using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Button[] buyButtons = new Button[3];
    public float[] prices = new float[3];
    public float[] level = new float[3];
    public Text[] texts = new Text[3];
    public Text[] levelText = new Text[3];
    public Text besosText;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        for (int i = 0; i < buyButtons.Length; i++)
        {
            Debug.Log(i);
            prices[i] = playerStats.holdStatPrice[i];
            level[i] = playerStats.holdLevel[i];
            besosText.text = $"Besos: {playerStats.moneyAmount}";
            var i2 = i;
            buyButtons[i].onClick.AddListener(delegate { playerStats.Buy(i2, prices[i2], panel); });
            texts[i].text = prices[i] + " Besos";
            switch (i)
            {
                case 0: levelText[i].text = "Pressure Duration Lv." + level[i]; break;
                case 1: levelText[i].text = "Minimum Pressure Lv." + level[i]; break;
                case 2: levelText[i].text = "Recharge Pressure Lv." + level[i]; break;
            }
        }
        playerStats.manager = this;
    }

    private void OnDestroy()
    {
        for(int i = 0; i < prices.Length; i++)
        {
            playerStats.holdStatPrice[i] = prices[i];
            playerStats.holdLevel[i] = level[i];
        }
    }
}
