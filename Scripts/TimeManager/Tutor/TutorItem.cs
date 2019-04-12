using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.Tutor
{
    public enum TutorType
    {
        INFO,
        GAMEPLAY,
    }

    public enum TutorAim
    {
        CUSTOMER,
        PRODUCT_PROVIDER,
        PRODUCTION_FIELD,
        PRODUCTION_UNIT,
    }

    [System.Serializable]
    public class TutorItem
    {
        public TutorType type;
        public bool aim_ready;
        public TutorAim aim;
        public List<Product.ProductType> product;
        public int position_index;
        public string text_id;
        public int clicks;
        public string name;

        public TutorItem(TutorType t, bool re, TutorAim a, List<Product.ProductType> pr, int index, string t_id, int cnt, string n)
        {
            type = t;
            aim_ready = re;
            aim = a;
            product = pr;
            position_index = index;
            text_id = t_id;
            clicks = cnt;
            name = n;
        }
    }
}