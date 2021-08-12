using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Button buy1, buy2, buy3;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        buy1.onClick.AddListener(delegate { playerStats.Buy(0, 100); });
        buy2.onClick.AddListener(delegate { playerStats.Buy(1, 50); });
        buy3.onClick.AddListener(delegate { playerStats.Buy(2, 50); });
        
    }
}
