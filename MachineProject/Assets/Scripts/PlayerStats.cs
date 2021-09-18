using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private static GameObject playerInstance;
    [SerializeField] public float moneyAmount = 1000;
    [SerializeField] private float incrementMultiplier = 1.5f;
    public float[] holdStatPrice = new float[3];
    public float[] holdLevel = new float[3];
    public int playerScore = 0;
    public int totalScore = 0;
    public float ultimateCount = 0;
    public bool isUlting = false;
    public bool canUlt = false;
    public bool levelEnd = false;
    public bool[] levelUnlocked;
    public bool unlimitedUlti = false;
    public bool shopUnlocked = false;
    public ButtonManager manager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(playerInstance == null)
        {
            playerInstance = gameObject;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }

    public void Buy(int item, float moneyRequired, GameObject panel)
    {
        if(moneyAmount >= moneyRequired)
        {
            //clamping, level does not go beyond 5
            if(manager.level[item] + 1 <= 5)
            {
                moneyAmount -= moneyRequired;

                manager.prices[item] = (int)(manager.prices[item] * incrementMultiplier);
                manager.texts[item].text = manager.prices[item] + " Besos";
                manager.level[item] += 1;
                manager.besosText.text = $"Besos: {moneyAmount}";
            }
            else
            {
                panel.SetActive(true);
            }

            switch (item)
            {
                case 0:
                    Debug.Log("Item 1");
                    manager.levelText[item].text = $"Pressure Duration Lv.{manager.level[item]}"; //decreases pressure decrease
                    break;
                case 1:
                    Debug.Log("Item 2");
                    manager.levelText[item].text = $"Minimum Pressure Lv.{manager.level[item]}"; //increases minimum pressure
                    break;
                case 2:
                    Debug.Log("Item 3");
                    manager.levelText[item].text = $"Recharge Pressure Lv.{manager.level[item]}"; // increases reload speed
                    break;
            }

            return;
        }
        Debug.Log("Not enough money");
        return;
    }

    public void AddBesos()
    {
        moneyAmount += 15;
    }

    public void ResetPoints()
    {
        playerScore = 0;
        moneyAmount = 0;
        for(int i = 0; i < 3; i++)
        {
            holdLevel[i] = 1;
            holdStatPrice[i] = 50;
        }
    }

    public void resetScore()
    {
        playerScore = 0;
        shopUnlocked = true;
    }

    public void setAccess()
    {
        for(int i=0; i < levelUnlocked.Length; i++)
        {
            levelUnlocked[i] = true;
        }
    }

    public void resetAccess()
    {
        levelUnlocked[1] = false;
        levelUnlocked[2] = false;
    }

    public void instaCash()
    {
        moneyAmount += 100;
    }

    public void infiniteUlt()
    {
        if (!unlimitedUlti)
        {
            unlimitedUlti = true;
            ultimateCount = 20;
        }
        else
        {
            unlimitedUlti = false;
            ultimateCount = 0;
        }
    }

    
}