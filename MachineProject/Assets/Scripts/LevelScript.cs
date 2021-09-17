using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    public Button[] levelButton;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if (playerStats.levelUnlocked[1] == false)
        {
            levelButton[1].interactable = false;
        }
        if (playerStats.levelUnlocked[2] == false)
        {
            levelButton[2].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.levelUnlocked[1])
        {
            levelButton[1].interactable = true;
        }
        if (playerStats.levelUnlocked[2])
        {
            levelButton[2].interactable = true;
        }
    }
}
