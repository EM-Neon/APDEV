using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    public GameObject pauseObject;

    public void Pause()
    {
        Time.timeScale = 0;
        pauseObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseObject.SetActive(false);
    }

    private void OnApplicationFocus(bool focus)
    {
        Debug.Log($"Device Focus {focus.ToString()}");
        if (!focus)
        {
            Pause();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Pause();
        }
    }
}
