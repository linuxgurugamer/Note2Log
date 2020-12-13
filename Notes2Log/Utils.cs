using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Notes2Log_NS.RegisterToolbar;

namespace Notes2Log_NS
{
    public static class Utils
    {
        public static string SafeLoad(this ConfigNode node, string value, string oldvalue)
        {
            if (!node.HasValue(value))
            {
                Log.Info("SafeLoad string, node missing value: " + value + ", oldvalue: " + oldvalue);
                return oldvalue;
            }
            return node.GetValue(value);
        }

    }
}
