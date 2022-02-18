using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wangkanai.Universal.Ecommerce
{
    /// <summary>
    /// Tracks information about each individual item in the user's shopping cart and associates the item with each transaction
    /// </summary>
    public class Item
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Item() { }
        /// <param name="name">[Required] Product name</param>
        /// <param name="sku">SKU/code</param>
        /// <param name="category">Category or variation</param>
        /// <param name="price">Unit price</param>
        /// <param name="quantity">Quantity</param>
        public Item(string name, string sku, string category, double price, int quantity):this()
        {
            Name = name;
            SKU = sku;
            Category = category;
            Price = price;
            Quantity = quantity;
        }
        public string JsScript(string id)
        {
            return string.Format("{{'id':'{0}','name':'{1}','sku':'{2}','category':'{3}','price':'{4}','quantity':'{5}'}}",
                id,
                Name,
                SKU,
                Category,
                Price,
                Quantity);
        }
    }
}
