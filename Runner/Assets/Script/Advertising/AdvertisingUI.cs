using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdvertisingUI : MonoBehaviour
{
    private InterstitialAd _interstitialAd;
    private int _num = 0;

    private string _id = "ca-app-pub-3940256099942544/1033173712";

    private void Start()
    {
        DeathingState._Enter += Check;
    }

    private void OnEnable()
    {
        _interstitialAd = new InterstitialAd(_id);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);
    }

    private void Check()
    {
        _num++;
        Debug.Log(_num);
        if(_num % 4 == 0)
        {
            ShowAd();
        }
    }

    private void ShowAd()
    {
        if (_interstitialAd.IsLoaded())
            _interstitialAd.Show();
    }
    private void OnDestroy()
    {
        DeathingState._Enter -= Check;
        Debug.Log("Yes");
    }

}
