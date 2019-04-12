using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager.Pause
{
    public class PauseController : MonoBehaviour
    {
        public GameObject panel;
        float prev_speed;

        public void OpenPanel()
        {
            prev_speed = Time.timeScale;
            Time.timeScale = 0.0f;
            panel.SetActive(true);
        }

        public void ClosePanel()
        {
            Time.timeScale = prev_speed;
            panel.SetActive(false);
        }
    }
}