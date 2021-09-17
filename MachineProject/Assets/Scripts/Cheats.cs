using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    public void onCheatEnabled()
    {
        playerStats.infiniteUlt();
    }
    public void levelSelect()
    {
        playerStats.setAccess();
    }
}
