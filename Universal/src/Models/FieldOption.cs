using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wangkanai.Universal
{
    public class FieldOption : Field
    {
        public override string ToString()
        {
            return "{" + base.ToString() + "}";
        }
    }
}
