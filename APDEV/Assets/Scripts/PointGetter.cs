using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointGetter : MonoBehaviour
{
    [SerializeField] PlayerStats player;

    private void Start()
    {
        player = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = player.playerScore + " Points";
        gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = player.playerScore + " Besos";
    }

    public void ResetPoints()
    {
        player.playerScore = 0;
    }
}
