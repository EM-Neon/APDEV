using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float moneyAmount = 1000;
    [SerializeField] private int armor = 1;
    [SerializeField] private float incrementMultiplier = 1.5f;
    public float[] holdStatPrice = new float[3];
    public float[] holdLevel = new float[3];
    public int playerScore = 0;
    public ButtonManager manager;

    public void Buy(int item, float moneyRequired)
    {
        if(moneyAmount >= moneyRequired)
        {
            Debug.Log("Buying");

            moneyAmount -= moneyRequired;

            manager.prices[item] = (int)(manager.prices[item] * incrementMultiplier);
            manager.texts[item].text = manager.prices[item] + " Besos";
            manager.level[item] += 1;
            manager.besosText.text = $"Besos: {moneyAmount}";
            switch (item)
            {
                case 0:
                    Debug.Log("Item 1");
                    manager.levelText[item].text = $"Health\nLv.{manager.level[item]}";
                    break;
                case 1:
                    Debug.Log("Item 2");
                    manager.levelText[item].text = $"Duration Lv.{manager.level[item]}";
                    break;
                case 2:
                    Debug.Log("Item 3");
                    manager.levelText[item].text = $"Cooldown Lv.{manager.level[item]}";
                    break;
            }

            return;
        }
        Debug.Log("Not enough money");
        return;
    }
}