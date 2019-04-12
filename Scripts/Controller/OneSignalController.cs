using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OneSignalPush.MiniJSON;

public class OneSignalController {


    private static string oneSignalDebugMessage;
    static string Id;
    static string Tocken;

    public static void Create()
    {
        // Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
        // OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);

        OneSignal.StartInit("30aeaac5-eace-4f2b-a39d-c28318eb6d73")
            .HandleNotificationOpened(HandleNotificationOpened)
            .EndInit();

        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;

        // Call syncHashedEmail anywhere in your app if you have the user's email.
        // This improves the effectiveness of OneSignal's "best-time" notification scheduling feature.
        // OneSignal.syncHashedEmail(userEmail);
        OneSignal.IdsAvailable((id, tocken) => { Id = id; Tocken = tocken; });
    }


    // Gets called when the player opens the notification.
    private static void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
    }

    public static void someMethod(int day)
    {
        OneSignal.ClearOneSignalNotifications();

        // Just an example userId, use your own or get it the devices by calling OneSignal.GetIdsAvailabl

        var notification = new Dictionary<string, object>();

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            notification["contents"] = new Dictionary<string, string>() { { TextManager.getText("push_locale_text"),
                 TextManager.getText("push_" + day.ToString() + "_text")}, { "en", "Cats prepared a gift! Come and get it!" } };
        }
        else
        {
            notification["contents"] = new Dictionary<string, string>() { { TextManager.getText("push_locale_text"),
                 TextManager.getText("push_" + day.ToString() + "_text")} };
        }

        notification["include_player_ids"] = new List<string>() { Id };


        //Test!!!!
        var additional_hours = 24 - System.DateTime.Now.Hour + 17;
        //var additional_hours = 24 - System.DateTime.Now.Hour + 8;

        // Example of scheduling a notification in the future.
        notification["send_after"] = System.DateTime.Now.ToUniversalTime().AddHours(additional_hours).ToString("U");

        //notification["send_after"] = System.DateTime.Now.ToUniversalTime().AddMinutes(5).ToString("U");

        OneSignal.PostNotification(notification, (responseSuccess) =>
        {
            oneSignalDebugMessage = "Notification posted successful! Delayed by about 30 secounds to give you time to press the home button to see a notification vs an in-app alert.\n" + Json.Serialize(responseSuccess);
            Debug.Log(oneSignalDebugMessage);
        }, (responseFailure) =>
        {
            oneSignalDebugMessage = "Notification failed to post:\n" + Json.Serialize(responseFailure);
            Debug.Log(oneSignalDebugMessage);
        });
    }
}
