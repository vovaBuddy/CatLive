using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetsScannInfo
{
    [Serializable]
    public class StarGiver
    {
        public List<string> pref_cat_names;
        public int cnt_opened;
        public int need_open;

        public StarGiver()
        {
            pref_cat_names = new List<string>();
            cnt_opened = 0;
            need_open = 5;
        }
    }
}
