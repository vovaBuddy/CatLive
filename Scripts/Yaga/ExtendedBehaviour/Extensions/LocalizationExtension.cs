using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Yaga.Localization
{
    class UnityTextLocalizationExtension : IExtension
    {
        object obj;

        private UnityTextLocalizationExtension() { }
        public UnityTextLocalizationExtension(object obj)
        {
            this.obj = obj;
        }

        public void Start()
        {
            var props = obj.GetType().GetFields().Where(
                prop => Attribute.IsDefined(prop, typeof(Localize)));

            foreach (System.Reflection.FieldInfo p in props)
            {
                Text text = p.GetValue(obj) as Text;
                //text.text = TextManager.GetLocalizedString(p.Name);
            }
        }

        public void Update()
        {

        }
    }
}
