using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yaga;
using Yaga.MessageBus;

namespace PhotoScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class ScanScreenView : ExtendedBehaviour
    {
        public Text header_make;
        public Text header_scan;

        public GameObject scan_screen;

        public GameObject btn_home;
        public GameObject btn_photo;
        public GameObject btn_switch;

        public GameObject btn_ok;
        public GameObject btn_no;

        // Use this for initialization
        override public void ExtendedStart()
        {
            header_make.text = TextManager.getText("header_make");
            header_scan.text = TextManager.getText("header_scan");
            scan_screen.SetActive(true);
            ShowScanUI(empty_msg);
            CloseApplyUI(empty_msg);
        }

        [Subscribe(UIMessageType.SHOW_SCAN_UI)]
        public void ShowScanUI(Message msg)
        {
            if (DataController.instance.tasks_storage.content.ContainsKey("opened_pets") &&
                (int)DataController.instance.tasks_storage.content["opened_pets"] >= 1)
            {
                btn_home.SetActive(true);                
            }
            else
            {
                btn_home.SetActive(false);

                //MessageBus.Instance.SendMessage(
                //    new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                //        new TutorMaskController.TutorMaskParametr(
                //            btn_photo.transform.position, false, true)));

                //MessageBus.Instance.SendMessage(
                //    new Message(TutorMaskController.Messages.SHOW_TUTOR_MASK,
                //        new TutorMaskController.TutorMaskParametr(
                //            btn_ok.transform.position, false, true)));
            }
            btn_photo.SetActive(true);
            btn_switch.SetActive(true);
            header_scan.gameObject.SetActive(false);
            header_make.gameObject.SetActive(true);
        }

        [Subscribe(UIMessageType.CLOSE_SCAN_UI)]
        public void CloseScanUI(Message msg)
        {
            btn_home.SetActive(false);
            btn_photo.SetActive(false);
            btn_switch.SetActive(false);
        }

        [Subscribe(UIMessageType.SHOW_APPLY_PHOTO_UI)]
        public void ShowApplyUI(Message msg)
        {
            btn_ok.SetActive(true);
            btn_no.SetActive(true);

            header_scan.gameObject.SetActive(true);
            header_make.gameObject.SetActive(false);
        }

        [Subscribe(UIMessageType.CLOSE_APPLY_PHOTO_UI)]
        public void CloseApplyUI(Message msg)
        {
            btn_ok.SetActive(false);
            btn_no.SetActive(false);
        }

        [Subscribe(UIMessageType.CLOSE_SCAN_SCREEN)]
        public void Close(Message msg)
        {
            scan_screen.SetActive(false);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}