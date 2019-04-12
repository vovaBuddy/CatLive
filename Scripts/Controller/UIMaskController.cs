using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMaskController : MonoBehaviour, IPointerDownHandler
{
    private string _tutor_event_name = string.Empty;
    public string tutor_event_name
    {
        get { return _tutor_event_name; }
        set { _tutor_event_name = value; }
    }



    public void OnPointerDown(PointerEventData evd)
    {
        Vector2 localCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gameObject.GetComponent<RectTransform>(), evd.position, evd.pressEventCamera, out localCursor);
        var img = gameObject.GetComponent<Image>();
        Texture2D txtr = img.mainTexture as Texture2D;
        var size = gameObject.GetComponent<RectTransform>().sizeDelta;

        int x = Mathf.FloorToInt((size.x / 2 - localCursor.x) / size.x * txtr.width);
        int y = Mathf.FloorToInt((size.y / 2 - localCursor.y) / size.y * txtr.height);

        if (txtr.GetPixel(x, y).a == 0)
        {
            UnityEngine.UI.Button btn = null;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(evd, results);

            foreach (var go in results)
            {
                var tmp = go.gameObject.GetComponent<UnityEngine.UI.Button>();
                if (tmp != null)
                {
                    btn = tmp;
                    break;
                }
            }

            if (btn != null)
            {
                btn.onClick.Invoke();
                Yaga.MessageBus.MessageBus.Instance.SendMessage(TutorMaskController.Messages.CLOSE_TUTOR_MASK);
            }
        }
    }
}
