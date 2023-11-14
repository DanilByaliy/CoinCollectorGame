using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoadRewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string androidAdUnitId;
    public string iosAdUnitId;
    // public bool isTestingMode = true;
    string adUnitId;
    Bank bank;

    private void Awake() {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif

        bank = FindObjectOfType<Bank>();
    }

    public void LoadAd()
    {
        print("Loading rewarded");
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(adUnitId))
        {
            print("rewarded loaded");
            ShowAd();
        }
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("rewarded NOT loaded");
        // throw new System.NotImplementedException();
    }

    public void ShowAd()
    {
        print("Showing rewarded");
        Advertisement.Show(adUnitId, this);
    }



    public void OnUnityAdsShowClick(string placementId) {}

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        print("rewarded show complete");
        bank.Deposite(200);
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("rewarded show faled");
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("rewarded show start");
        // throw new System.NotImplementedException();
    }
}

