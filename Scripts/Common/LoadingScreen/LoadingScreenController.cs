using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yaga;
using Yaga.MessageBus;

namespace Common.LoadingScreen
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    public class LoadingScreenController : ExtendedBehaviour
    {
        LoadingScreenBinder binder;
        string scene_name;
        public bool show_ads;

        // Use this for initialization
        public override void ExtendedStart()
        {
            binder = gameObject.transform.parent.Find("LoadingScreenBinder").GetComponent<LoadingScreenBinder>();

            //if (Application.systemLanguage == SystemLanguage.Russian)
            //{
            //    binder.logo_close_scene.sprite = Resources.Load<Sprite>("logo/Russian");
            //    binder.logo_open_scene.sprite = Resources.Load<Sprite>("logo/Russian");
            //}
            //else
            //{
            //    binder.logo_close_scene.sprite = Resources.Load<Sprite>("logo/English");
            //    binder.logo_open_scene.sprite = Resources.Load<Sprite>("logo/English");
            //}
        }



        [Subscribe(API.LoadingScreenAPI.OPEN_CLOSE_ANIM)]
        public void ShowCloseAnim(Message msg)
        {
            binder.panel.SetActive(true);
        }

        [Subscribe(API.LoadingScreenAPI.OPEN_OPEN_ANIM)]
        public void ShowOpenAnim(Message msg)
        {
            binder.panel.SetActive(true);

            var param = Yaga.Helpers.CastHelper.Cast<API.SceneNameParametr>(msg.parametrs);
            scene_name = param.name;
            show_ads = param.show_ads;
        }

        [Subscribe(API.LoadingScreenAPI.OPEN_ANIM_ON_START)]
        public void Show(Message msg)
        {
            binder.panel.SetActive(true);
        }

        [Subscribe(API.LoadingScreenAPI.OPEN_ANIM_ON_FINISH)]
        public void LoadScene(Message msg)
        {
            LoadScene();
        }

        void LoadScene()
        {
            if(show_ads)
            {
                //yield return new WaitForSeconds(2);

                //AppodealController.instance.ShowInterstitial();

                show_ads = false;
            }

            MessageBus.Restore();
            SceneManager.LoadScene(scene_name);
        }
    }
}