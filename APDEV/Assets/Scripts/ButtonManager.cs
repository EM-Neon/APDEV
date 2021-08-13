using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Button[] buyButtons = new Button[3];
    public float[] prices = new float[3];
    public Text[] texts = new Text[3];

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        for (int i = 0; i < buyButtons.Length; i++)
        {
            Debug.Log(i);
            prices[i] = playerStats.holdStatPrice[i];
            var i2 = i;
            buyButtons[i].onClick.AddListener(delegate { playerStats.Buy(i2, prices[i2]); });
            texts[i].text = prices[i] + " Besos";
        }

        /*buy1.onClick.AddListener(delegate { playerStats.Buy(0, prices[0]); });
        buy2.onClick.AddListener(delegate { playerStats.Buy(1, prices[1]); });
        buy3.onClick.AddListener(delegate { playerStats.Buy(2, prices[2]); });*/

        playerStats.manager = this;
    }

    private void OnDestroy()
    {
        for(int i = 0; i < prices.Length; i++)
        {
            playerStats.holdStatPrice[i] = prices[i];
        }
    }
}
