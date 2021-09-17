using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    private bool cheat1Enabled = false;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

    public void onCheatEnabled()
    {
        if(cheat1Enabled)
            playerStats.infiniteUlt();
        else
            playerStats.infiniteUlt();
    }
    public void levelSelect()
    {
        playerStats.setAccess();
    }

    public void onCheat2()
    {
        playerStats.instaCash();
    }
}
