using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

namespace Yaga
{
    public class Localizer
    {
        public static void Localize(object obj)
        {
            var fields = obj.GetType().GetFields().Where(
                    prop => Attribute.IsDefined(prop, typeof(Localize)));

            foreach(var field in fields)
            {
                Text text = field.GetValue(obj) as Text;
                string name = obj.GetType().Namespace.Replace(".","_") + "_" + field.Name + "_" + "text";

                string local;
                if(TextManager.getLocalString(name, out local))
                {
                    text.text = local;
                }
                else
                {
                    Debug.Log("no locales for " + name);
                    //generate_new_field in local files
                }
            }
        }
    }
}