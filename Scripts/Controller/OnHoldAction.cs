using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yaga.MessageBus;
namespace MainScene
{
    public enum HoldActionsType
    {
        OPEN_KITCHEN_CUSTOM = 0,
        OPEN_SOFA_CUSTOM = 1,
        OPEN_BAD_CUSTOM = 2,

        OPEN_FLOOR_HOME_CUSTOM = 3,
        OPEN_WALL_HOME_CUSTOM = 4,
        OPEN_FLOOR_KITCHEN_CUSTOM = 5,
        OPEN_WALL_KITCHEN_CUSTOM = 6,

        OPEN_GARDEN_CUSTOM = 7,
        OPEN_BENCH_CUSTOM = 8

    }    

    public class OnHoldAction : MonoBehaviour
    {
        public HoldActionsType hold_type;

        public void HoldAction()
        {

            Message msg = new Message();
            
            msg.Type = MainMenuMessageType.OPEN_CUSTOMIZER_WITH_CLOSE;

            switch (hold_type)
            {


                case HoldActionsType.OPEN_KITCHEN_CUSTOM:
                    if (!Task.TaskController.GetController().GetTaskInfo(4).data.started)
                        return;
                    msg.parametrs = new Yaga.CommonMessageParametr("Kitchen_set");
                    break;
                    
                case HoldActionsType.OPEN_SOFA_CUSTOM:
                    msg.parametrs = new Yaga.CommonMessageParametr("Sofa");
                    break;

                case HoldActionsType.OPEN_BAD_CUSTOM:
                    msg.parametrs = new Yaga.CommonMessageParametr("Bed");
                    break;

                case HoldActionsType.OPEN_FLOOR_HOME_CUSTOM:
                    if (!Task.TaskController.GetController().GetTaskInfo(3).data.done)
                        return;
                    msg.parametrs = new Yaga.CommonMessageParametr("Floor_Home");
                    break;

                case HoldActionsType.OPEN_WALL_HOME_CUSTOM:
                    if (!Task.TaskController.GetController().GetTaskInfo(4).data.done)
                        return;
                    msg.parametrs = new Yaga.CommonMessageParametr("Wall_Home");
                    break;

                case HoldActionsType.OPEN_FLOOR_KITCHEN_CUSTOM:
                    if (!Task.TaskController.GetController().GetTaskInfo(3).data.done)
                        return;
                    msg.parametrs = new Yaga.CommonMessageParametr("Floor_Kitchen");
                    break;

                case HoldActionsType.OPEN_WALL_KITCHEN_CUSTOM:
                    if (!Task.TaskController.GetController().GetTaskInfo(4).data.done)
                        return;
                    msg.parametrs = new Yaga.CommonMessageParametr("Wall_Kitchen");
                    break;

                case HoldActionsType.OPEN_GARDEN_CUSTOM:
                    msg.parametrs = new Yaga.CommonMessageParametr("garden");
                    break;

                case HoldActionsType.OPEN_BENCH_CUSTOM:
                    msg.parametrs = new Yaga.CommonMessageParametr("bench");
                    break;
            }

            MessageBus.Instance.SendMessage(msg);
            MessageBus.Instance.SendMessage(MainMenuMessageType.CLOSE_MAIN_FOOTER);
        }
    }
}
