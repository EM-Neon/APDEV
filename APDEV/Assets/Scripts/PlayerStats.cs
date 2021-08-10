using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int moneyAmount = 1000;
    [SerializeField] private int ballSpeed = 1;
    [SerializeField] private int armor = 1;

    public void Buy(int item, int moneyRequired)
    {
        if(moneyAmount >= moneyRequired)
        {
            Debug.Log("Buying");
            switch (item)
            {
                case 0:
                    Debug.Log("Item 1");
                    break;
                case 1:
                    Debug.Log("Item 2");
                    break;
            }
            moneyAmount -= moneyRequired;
            return;
        }
        Debug.Log("Not enough money");
        return;
    }
}