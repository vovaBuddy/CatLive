using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaga
{
    [AttributeUsage(AttributeTargets.Class)]
    class Extension : Attribute
    {
        public Extensions[] extensions;

        public Extension(params Extensions[] exts)
        {
            extensions = exts;
        }
    }
}
