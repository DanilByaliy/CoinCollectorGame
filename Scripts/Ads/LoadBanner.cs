using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoadBanner : MonoBehaviour
{
    public string androidAdUnityId;
    public string iosAdUnityId;
    // public bool isTestingMode = true;
    string adUnityId;

    BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private void Start() {
#if UNITY_IOS
        adUnityId = iosAdUnityId;
#elif UNITY_ANDROID
        adUnityId = androidAdUnityId;
#endif

        Advertisement.Banner.SetPosition(bannerPosition);
        LoadBanner1();
    }

    public void LoadBanner1()
    {
        BannerLoadOptions options = new BannerLoadOptions {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerLoadError
        };

        Advertisement.Banner.Load(adUnityId, options);
    }

    void OnBannerLoaded()
    {
        print("Banner Loaded");
        ShowBannerAd();
    }

    void OnBannerLoadError(string error)
    {
        print("Banner LoadError: " + error);
    }

    public void ShowBannerAd()
    {
        Debug.Log(1);
        BannerOptions options = new BannerOptions {
            showCallback = OnBannerShow,
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden
        };
        Debug.Log(2);
        Advertisement.Banner.Show(adUnityId, options);
        Debug.Log(3);
    }

    void OnBannerShow() {}
    void OnBannerClicked() {}
    void OnBannerHidden() {}

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    // void Start() {
    //     ShowBannerAd();
    // }
}
