using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaga.MessageBus;

public static class DoneAction
{
    public static void Action()
    {
        DialogController dialog = DialogController.GetController();

        List<DialogEntity> deList = new List<DialogEntity>();
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 1), DialogType.Main, DialogType.Black, DialogEntity.get_id(25, 1)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 2), DialogType.Black, DialogType.Main, DialogEntity.get_id(25, 2)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 3), DialogType.Black, DialogType.Main, DialogEntity.get_id(25, 3)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 4), DialogType.Main, DialogType.Black, DialogEntity.get_id(25, 4)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 5), DialogType.Main, DialogType.Black, DialogEntity.get_id(25, 5)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 6), DialogType.Black, DialogType.Main, DialogEntity.get_id(25, 6)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 7), DialogType.Main, DialogType.Black, DialogEntity.get_id(25, 7)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 8), DialogType.Black, DialogType.Main, DialogEntity.get_id(25, 8)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 9), DialogType.Main, DialogType.Black, DialogEntity.get_id(25, 9)));
        deList.Add(new DialogEntity(
            TextManager.getDialogsText(25, 10), DialogType.Black, DialogType.Main, DialogEntity.get_id(25, 10)));
        dialog.SetDialogs(deList);
        dialog.SetBtnAction(() =>
        {
            dialog.CloseDialog();

            MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.SHOW_MAIN_MENU);
            //MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.OPEN_TASK_LIST);
        });
        dialog.ShowDialog();
        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_MAIN_MENU);
        MessageBus.Instance.SendMessage(MainScene.MainMenuMessageType.CLOSE_TASK_LIST_ONLY);
    }
}
