using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaga.Helpers
{
    public static class CastHelper
    {
        public static T Cast<T>(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
