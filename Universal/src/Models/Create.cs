using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wangkanai.Universal
{
    public class Create
    {
        private Configuration _config { get; set; }
        private ConfigOption _objects { get; set; }
        public Create(Configuration config)
        {
            _config = config;
        }
        public Create(Configuration config, ConfigOption configObject)
            : this(config)
        {
            _objects = configObject;
        }
        public override string ToString()
        {
            return string.Format("ga('create', '{0}', '{1}'{2});",
                _config.Account,
                _config.Property,
                _objects != null ? ", " + _objects.ToString() : "");
        }
    }
}
