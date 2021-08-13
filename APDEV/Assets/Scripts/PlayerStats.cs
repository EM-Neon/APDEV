using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float moneyAmount = 1000;
    [SerializeField] private int armor = 1;
    [SerializeField] private float incrementMultiplier = 1.5f;
    public float[] holdStatPrice = new float[3];
    public ButtonManager manager;

    public void Buy(int item, float moneyRequired)
    {
        if(moneyAmount >= moneyRequired)
        {
            Debug.Log("Buying");

            moneyAmount -= moneyRequired;

            manager.prices[item] = (int)(manager.prices[item] * incrementMultiplier);
            manager.texts[item].text = manager.prices[item] + " Besos";

            switch (item)
            {
                case 0:
                    Debug.Log("Item 1");
                    break;
                case 1:
                    Debug.Log("Item 2");
                    break;
                case 2:
                    Debug.Log("Item 3");
                    break;
            }

            return;
        }
        Debug.Log("Not enough money");
        return;
    }
}