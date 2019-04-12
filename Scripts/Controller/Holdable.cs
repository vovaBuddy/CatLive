using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;
using Yaga;

namespace MainScene
{
    [Extension(Extensions.SUBSCRIBE_MESSAGE)]
    class Holdable : ExtendedBehaviour
    {
        private float holdTime = 0.7f; //or whatever
        private float indicator_time = 0.2f;
        private float acumTime = 0;
        private float chopTime = 0.0f;
        private bool start_indicator = false;

        bool stop = false;

        [Subscribe(MainScene.MainMenuMessageType.OPEN_CUSTOMIZER,
            MainScene.MainMenuMessageType.OPEN_CUSTOMIZER_WITH_CLOSE,
            MainScene.MainMenuMessageType.OPEN_MINI_GAMES,
            MainScene.MainMenuMessageType.OPEN_TASK_LIST,
            MainScene.MainMenuMessageType.OPEN_SHOP,
            MainScene.MainMenuMessageType.OPEN_CAT_SHOW,
            MainScene.MainMenuMessageType.SHOW_SCAN_MENU)]
        public void OpenMenu(Message msg)
        {
            stop = true;
        }

        [Subscribe(MainScene.MainMenuMessageType.CLOSE_CUSTOMIZER,
            MainScene.MainMenuMessageType.CLOSE_MINI_GAMES,
            MainScene.MainMenuMessageType.CLOSE_TASK_LIST,
            MainScene.MainMenuMessageType.CLOSE_SHOP,
            MainScene.MainMenuMessageType.SHOW_MAIN_MENU,
            MainScene.MainMenuMessageType.CLOSE_SCAN_MENU)]
        public void CloseMenu(Message msg)
        {
            stop = false;
        }

        override public void ExtendedUpdate()
        {
            if (stop)
                return;

            if (!CatsMoveController.GetController().DoesCatReachDestination(Cats.Main) ||
                !CatsMoveController.GetController().DoesCatReachDestination(Cats.Black) ||
                !CatsMoveController.GetController().DoesCatReachDestination(Cats.Jakky) ||
                !CameraMoveController.GetController().DoesReachDestination() ||
                DialogController.GetController().DialogWindow.activeSelf)
                return;

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).tapCount == 2)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), -Vector2.up);

                    int max_order = -100000;
                    OnHoldAction hold_action = null;

                    foreach(var hit in hits)
                    {
                        if(hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder > max_order)
                        {
                            max_order = hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                            hold_action = hit.collider.gameObject.GetComponent<OnHoldAction>();
                        }
                    }

                    if(hold_action != null)
                    {
                        hold_action.HoldAction();
                    }
                }
                else if(Input.GetTouch(0).tapCount == 1)
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit _hit;
                    Physics.Raycast(ray, out _hit);

                    if (_hit.collider != null)
                    {
                        _hit.collider.gameObject.GetComponent<OnTapAction3d>().OnTapAction();
                    }

                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), -Vector2.up);

                    int max_order = -100000;

                    foreach (var hit in hits)
                    {
                        if (hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder > max_order)
                        {
                            max_order = hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                        }
                    }

                }
            }


            //unity editor
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit _hit;
                    Physics.Raycast(ray, out _hit);

                    if(_hit.collider != null)
                    {
                        var go = _hit.collider.gameObject.GetComponent<OnTapAction3d>();

                        if (go != null)
                            go.OnTapAction();
                    }



                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);

                    int max_order = -100000;
                    
                    if (chopTime < holdTime)
                    {
                        chopTime += Time.deltaTime;
                    }
                    else
                    {
                        chopTime = 0;

                        hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);

                        max_order = -100000;
                        OnHoldAction hold_action = null;

                        if(hits.Length > 0)
                        {
                            hold_action = hits[0].collider.gameObject.GetComponent<OnHoldAction>();
                        }

                        if (hold_action == null)
                        {
                            foreach (var hit in hits)
                            {
                                if (hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder > max_order)
                                {
                                    max_order = hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                                    hold_action = hit.collider.gameObject.GetComponent<OnHoldAction>();
                                }
                            }
                        }

                        if (hold_action != null)
                        {
                            hold_action.HoldAction();
                        }
                    }
                }
            }
        }
    }    
}
