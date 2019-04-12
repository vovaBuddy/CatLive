using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using Yaga.Helpers;
using UnityEngine.UI;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ShopView : ExtendedBehaviour
    {
        public GameObject shop;

        public Text header_text;
        public Text head_btn_text;
        public Text collar_btn_text;
        public Text skeen_btn_text;
        public Text eye_btn_text;
        public Text glasses_btn_text; 

        public List<GameObject> shop_items;
        public ScrollRect scrollRect;
        public GameObject content;

        public GameObject shop_item_prefab;

        [Subscribe(MainMenuMessageType.SELECT_SHOP_ITEM)]
        public void SelectItem(Message msg)
        {
            var param = CastHelper.Cast<ShopTypeParametr>(msg.parametrs);

            foreach(var item in shop_items)
            {
                item.SetActive(false);
            }

            shop_items[(int)param.type].SetActive(true);
            //scrollRect.content = shop_items[(int)param.type].gameObject
            //    .GetComponent<RectTransform>();

            var width_item = shop_item_prefab.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;

            var w = shop_items[(int)param.type].
                gameObject.transform.childCount * (width_item + 20);

            content.GetComponent<RectTransform>().sizeDelta = new Vector2(w, 100);

        }

        [Subscribe(MainMenuMessageType.OPEN_SHOP)]
        public void ShowMain(Message msg)
        {
            shop.SetActive(true);

            //foreach (var item in shop_items)
            //{
            //    item.SetActive(false);
            //}

            shop_items[0].SetActive(true);

            var width_item = shop_item_prefab.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;

            var w = shop_items[0].
                gameObject.transform.childCount * (width_item + 20);

            content.GetComponent<RectTransform>().sizeDelta = new Vector2(w, 100);
        }

        [Subscribe(MainMenuMessageType.CLOSE_SHOP)]
        public void CloseMain(Message msg)
        {
            for (int i = 0; i < shop_items.Count - 1; ++i)
            {
                shop_items[i].SetActive(false);
            }

            shop.SetActive(false);
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            header_text.text = TextManager.getText("sc_shop_header_text");
            head_btn_text.text = TextManager.getText("sc_shop_head_btn_text");
            collar_btn_text.text = TextManager.getText("sc_shop_collar_btn_text");
            skeen_btn_text.text = TextManager.getText("sc_shop_skeen_btn_text");
            eye_btn_text.text = TextManager.getText("sc_shop_eye_btn_text");
            glasses_btn_text.text = TextManager.getText("sc_shop_glasses_btn_text");

            shop.SetActive(false);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}
