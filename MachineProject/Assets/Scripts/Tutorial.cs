using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> tutorialList;
    private int current = 0;

    private void Start()
    {
        tutorialList[0].SetActive(true);
    }

    public void onTutorialPressed()
    {
        tutorialList[current].SetActive(false);
        if(current+1 != tutorialList.Count)
        {
            tutorialList[current + 1].SetActive(true);
            current++;
        }
    }

    public void onBack()
    {
        tutorialList[current].SetActive(false);
        if (current - 1 >= 0)
        {
            tutorialList[current - 1].SetActive(true);
            current--;
        }
    }
}
