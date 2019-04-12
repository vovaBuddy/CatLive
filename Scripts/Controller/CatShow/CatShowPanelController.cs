using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;
using UnityEngine.SceneManagement;

namespace CatShow
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class CatShowPanelController : ExtendedBehaviour
    {

        // Use this for initialization
        override public void ExtendedStart()
        {

        }

        [Subscribe(CatShowMessageType.STARTED_CAT_SHOW_GAME)]
        public void StarCatShow(Message msg)
        {
            MessageBus.Restore();
            SceneManager.LoadScene("cat_show_game");
        }

        public void StartGame()
        {
            MessageBus.Instance.SendMessage(CatShowMessageType.START_CAT_SHOW_GAME);
        }

        public void Open()
        {
            MessageBus.Instance.SendMessage(CatShowMessageType.OPEN_CAT_SHOW);
            MainScene.ArrowController.Instance.arrow_play_show.SetActive(false);

            Message msg = new Message();
            msg.Type = CatShowMessageType.UPDATE_ENERGY;
            msg.parametrs = new UpdateInt(DataController.instance.catsPurse.Energy);
            MessageBus.Instance.SendMessage(msg);
        }

        public void Close()
        {
            MessageBus.Instance.SendMessage(CatShowMessageType.CLOSE_CAT_SHOW);
        }

        // Update is called once per frame
        override public void ExtendedUpdate()
        {

        }
    }
}