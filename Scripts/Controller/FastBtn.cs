using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class FastBtn : MonoBehaviour, IPointerDownHandler
{
    Button.ButtonClickedEvent action;
    public void Start()
    {
        action = gameObject.GetComponent<Button>().onClick;
        gameObject.GetComponent<Button>().onClick = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        action.Invoke();
    }
}
