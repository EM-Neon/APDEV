using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript3 : MonoBehaviour
{
    [SerializeField] private Text text;

    public void Display(GameObject message)
    {
        if (this.CompareTag("Level1"))
        {
            text.text = "Level 1";
        }
        else if (this.CompareTag("Level2"))
        {
            text.text = "Level 2 Not Built";
        }
        else if (this.CompareTag("Level3"))
        {
            text.text = "Level 3 Not Built";
        }
        if (!message.activeSelf)
        {
            message.SetActive(true);
        }
    }

    public void Close(GameObject message)
    {
        if (message.activeSelf)
        {
            message.SetActive(false);
        }
    }
}
