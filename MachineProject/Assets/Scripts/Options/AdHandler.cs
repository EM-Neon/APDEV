using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class AdHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public float Besos = 0;//temp holder, will change this later
    public AdsManager adsManager;
    public Text besosLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        adsManager.OnAdDone += OnAdDone;
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if(stats != null)
        {
            Besos = stats.moneyAmount;
        }
        /*besosLabel = GetComponent<Text>();*/
    }
    public void OnAdDone(object sender, AdFinishEventArgs args)
    {
        if(args.PlacementID == AdsManager.SampleRewardedAd)
        {
            switch (args.AdResult)
            {
                case ShowResult.Failed: Debug.Log("Ad Fails to Show");break;
                case ShowResult.Skipped: Debug.Log("Ad Skipped"); break;
                case ShowResult.Finished: Debug.Log("Ad Finished"); Besos += 10; break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        besosLabel.text = $"Besos: {Besos}";
    }
}
