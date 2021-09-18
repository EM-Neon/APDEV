using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text text;
    public PlayerStats playerStats;
    private void Awake()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        text.text = playerStats.playerScore.ToString();
    }

}
