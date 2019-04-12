using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga;
using Yaga.MessageBus;
using PetsScannInfo;
using Yaga.Storage;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ScanMenuController : ExtendedBehaviour
    {
        private CatsScannInfo pets_info;
        private PetsScannedStorage pets_scanned;

        public void CloseScan()
        {
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_SCAN_MENU);
            MessageBus.Instance.SendMessage(MainMenuMessageType.SHOW_MAIN_MENU);
        }

        [Subscribe(MainMenuMessageType.SHOW_SCAN_MENU)]
        public void OpenScanMenu(Message msg)
        {
            Message m = new Message();
            m.Type = MainMenuMessageType.SHOW_SCANNED_PETS;

            List<string> names = new List<string>();

            foreach(KeyValuePair<string, bool> pair in pets_scanned.storage.content.opened_pets)
            {
                if(pair.Value)
                {
                    names.Add(pair.Key);
                }
            }

            Analytics.CustomEvent("scanned_cats", new Dictionary<string, object>
            {
                { "count", names.Count}
            });

            var parametrs = new ScanMenuMessageParametrs();
            parametrs.names = names;
            parametrs.max_cats = pets_scanned.storage.content.opened_pets.Count;
            parametrs.star_cnt = names.Count / 5;
            parametrs.max_star_cnt = parametrs.max_cats / 5;

            m.parametrs = parametrs;

            MessageBus.Instance.SendMessage(m);
        }

        // Use this for initialization
        override public void ExtendedStart()
        {
            pets_info = new CatsScannInfo();
            pets_scanned = new PetsScannedStorage(pets_info.file_names);

        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}