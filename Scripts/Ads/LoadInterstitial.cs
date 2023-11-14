using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoadInterstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string androidAdUnitId;
    public string iosAdUnitId;
    // public bool isTestingMode = true;
    string adUnitId;

    private void Awake() {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif
    }

    public void LoadAd()
    {
        print("Loading inter");
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        print("Units loaded");
        ShowAd();
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("units NOT loaded");
        // throw new System.NotImplementedException();
    }


    public void ShowAd()
    {
        print("Showing inter");
        Advertisement.Show(adUnitId, this);
    }



    public void OnUnityAdsShowClick(string placementId)
    {
        print("inter clicked");
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        print("inter show complete");
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("inter show faled");
        // throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("inter show start");
        // throw new System.NotImplementedException();
    }
}
