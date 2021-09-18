using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandleShop : MonoBehaviour
{
    public GameObject shop;
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.shopUnlocked)
        {
            shop.SetActive(true);
        }
    }
}
