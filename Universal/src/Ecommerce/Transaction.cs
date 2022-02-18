using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using Wangkanai.Universal.Ecommerce;

namespace Wangkanai.Universal.Ecommerce
{
    /// <summary>
    /// Ecommerce tracking allows you to measure the number of transactions and revenue that your website generates
    /// </summary>
    public class Transaction
    {
        public string Id { get; set; }
        public string Affiliation { get; set; }
        public double Revenue { get { return CalculateRevenue(); } }
        private double Total { get { return Items.Sum(x => x.Quantity * x.Price); } } //sum items
        public double? Tax { get; set; } //fixed
        public double? Shipping { get; set; } //fixed
        public Currency? Currency { get; set; }
        public List<Item> Items { get; set; }

        public Transaction() { Items = new List<Item>(); }
        /// <param name="id">The transaction ID. (e.g. 1234)</param>
        /// <param name="affiliation">The store or affiliation from which this transaction occurred (e.g. Acme Clothing).</param>
        public Transaction(string id, string affiliation)
            : this()
        {
            Id = id;
            Affiliation = affiliation;
        }
        /// <param name="id">The transaction ID. (e.g. 1234)</param>
        /// <param name="affiliation">The store or affiliation from which this transaction occurred (e.g. Acme Clothing).</param>
        /// <param name="shipping">Specifies the total shipping cost of the transaction. (e.g. 5)</param>
        /// <param name="tax">Specifies the total tax of the transaction. (e.g. 1.29)</param>
        public Transaction(string id, string affiliation, double shipping, double tax)
            : this(id, affiliation)
        {
            Shipping = shipping;
            Tax = tax;
        }
        /// <param name="id">The transaction ID. (e.g. 1234)</param>
        /// <param name="affiliation">The store or affiliation from which this transaction occurred (e.g. Acme Clothing).</param>
        /// <param name="shipping">Specifies the total shipping cost of the transaction. (e.g. 5)</param>
        /// <param name="tax">Specifies the total tax of the transaction. (e.g. 1.29)</param>
        /// <param name="currency">currencies enumeration supported by Google Wangkanai.Universal ecommerce measurement</param>
        public Transaction(string id, string affiliation, double shipping, double tax, Currency currency)
            : this(id, affiliation, shipping, tax)
        {
            Currency = currency;
        }

        private double CalculateRevenue()
        {
            return Total + (Tax ?? 0.0) + (Shipping ?? 0.0);
        }

        private string JsEcommercePlugin()
        {
            return "ga('require', 'ecommerce', 'ecommerce.js');";
        }
        internal string JsTransactionScript()
        {
            return string.Format("ga('ecommerce:addTransaction',{0});", JsPayload());
        }
        internal string JsPayload()
        {
            string js = "{";
            js += FormatProperty("id", Id) ?? "";
            js += "," + FormatProperty("affiliation", Affiliation) ?? "";
            js += "," + FormatProperty("revenue", Revenue) ?? "";
            js += "," + FormatProperty("shipping", Shipping) ?? "";
            js += "," + FormatProperty("tax", Tax) ?? "";
            if (Currency != null) js += "," + FormatProperty("currency", Currency) ?? "";
            js += "}";
            return js;
        }
        private string FormatProperty(string name, object value)
        {
            return string.Format("'{0}':'{1}'", name, value);
        }
        internal List<string> JsItemsScript()
        {
            List<string> list = new List<string>();
            foreach (var item in Items.OrderBy(x => x.Name))
                list.Add(string.Format("ga('ecommerce:addItem',{0});", item.JsScript(Id)));
            return list;
        }
        private string JsSend()
        {
            return "ga('ecommerce:send');";
        }
        public override string ToString()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine(JsEcommercePlugin());
            script.AppendLine(JsTransactionScript());
            foreach (var itemscript in JsItemsScript())
                script.AppendLine(itemscript);
            script.AppendLine(JsSend());
            return script.ToString();
        }
    }
}
