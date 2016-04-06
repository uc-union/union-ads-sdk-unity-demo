using System;
using Assets.Mediation_SDK.Library;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace Assets.Mediation_SDK.Demo
{
#if UNITY_ANDROID
    public class Demo : MonoBehaviour
    {
        private static int BTN_WIDHT = 200;
        private static int BTN_HEIGHT = 100;
        private static int TEXT_SIZE = 25;

        private Banner mBanner;
        private Interstitial mInterstitial;
        private NativeAd mNativeAd;

        private bool showNativeAd = false;
        private MainThreadLooper mMainThreadLooper;

        void Start()
        {
            mMainThreadLooper = new MainThreadLooper();
            AdSDK.start();
        }

        void OnGUI()
        {
            GUI.skin.button.fontSize = TEXT_SIZE;
            GUI.skin.textArea.fontSize = TEXT_SIZE;

            GUILayout.BeginScrollView(new Vector2(0, 0));
            {
                if (GUILayout.Button("Show Banner", GUILayout.Width(BTN_WIDHT), GUILayout.Height(BTN_HEIGHT)))
                {
                    if (mBanner == null)
                    {
                        mBanner = new Banner();
                        mBanner.setShowPos(200,0);
                        mBanner.setListener(new BannerAdListener(mBanner));
                    }
                    AdRequest adRequest = new AdRequest.Builder()//
                        .pub("ssr@debugbanner")//
                        .build();
                    mBanner.load(adRequest);
                    Debug.Log("Demo::start load banner!");
                }

                if (GUILayout.Button("Show Interstitial", GUILayout.Width(BTN_WIDHT), GUILayout.Height(BTN_HEIGHT)))
                {
                    if (mInterstitial == null)
                    {
                        mInterstitial = new Interstitial();
                        mInterstitial.setListener(new InterstitialAdListener(mInterstitial));
                    }
                    AdRequest adRequest = new AdRequest.Builder()//
                        .pub("ssr@debuginterstitial")//
                        .build();
                    mInterstitial.load(adRequest);
                    Debug.Log("Demo::start load interstitial!");
                }

                if (GUILayout.Button("Show Native", GUILayout.Width(BTN_WIDHT), GUILayout.Height(BTN_HEIGHT)))
                {
                    //StopAllCoroutines and StartCoroutine should run on Main Thread
                    mMainThreadLooper.Post(() => {
                    if (mNativeAd == null)
                        {
                            mNativeAd = new NativeAd();
                            mNativeAd.setListener(new NativeAdListener(delegate (Ad ad)
                            {
                                mMainThreadLooper.Post(() => {
                                showNativeAd = true;
                                    Debug.Log("Demo::onNativeAdLoaded_[icon_url:" + mNativeAd.getIconUrl() + "][cover_url:" + mNativeAd.getCoverUrl() + "]");
                                    if (mNativeAd.getIconUrl() != null && mNativeAd.getIconUrl().Length > 0)
                                    {
                                        StartCoroutine(LoadImage(mNativeAd.getIconUrl(), delegate (Texture2D texture) {
                                            mIconBg = texture;
                                        }));
                                    }
                                    if (mNativeAd.getCoverUrl() != null && mNativeAd.getCoverUrl().Length > 0)
                                    {
                                        StartCoroutine(LoadImage(mNativeAd.getCoverUrl(), delegate (Texture2D texture) {
                                            mCoverBg = texture;
                                        }));
                                    }
                                });
                            }));
                        }
                        showNativeAd = false;
                        mIconBg = null;
                        mCoverBg = null;

                        StopAllCoroutines();

                        AdRequest adRequest = new AdRequest.Builder()//
                            .pub("ssr@debugnative")//
                                                               //.withParams(AdRequestOption.newNativeBuilder().requestCoverImageSize(480, 320).requestExtraResource(false).build())//
                            .build();
                        mNativeAd.load(adRequest);
                        Debug.Log("Demo::start load native ad!");
                    });
                }

                if (GUILayout.Button("Exit", GUILayout.Width(BTN_WIDHT), GUILayout.Height(BTN_HEIGHT)))
                {
                    Application.Quit();
                }

                if (showNativeAd)
                {
                    if (GUILayout.Button("Title:" + mNativeAd.getTitle(), GUILayout.Width(BTN_WIDHT), GUILayout.Height(BTN_HEIGHT)))
                    {
                        mNativeAd.manualClick();
                    }
                    GUILayout.TextArea("Description:" + mNativeAd.getDescription());
                    GUILayout.Box(mIconBg, GUILayout.Width(100), GUILayout.Height(100));
                    GUILayout.Box(mCoverBg, GUILayout.Width(600), GUILayout.Height(400));
                }
            }
            GUILayout.EndScrollView();
        }
        private Texture2D mIconBg;
        private Texture2D mCoverBg;

        public delegate void NativeAdOnImageLoadedCallback(Texture2D texture);
        private static IEnumerator LoadImage(string url, NativeAdOnImageLoadedCallback callback)
        {
            WWW www = new WWW(url);
            yield return www;
            callback(www.texture);
        }

        void Update()
        {
            mMainThreadLooper.Run();
        }

        void OnDestroy()
        {
            Debug.Log("OnDestroy!");
            // Dispose of ads when the scene is destroyed
            if (mBanner != null)
            {
                mBanner.Dispose();
            }
            if (mInterstitial != null)
            {
                mInterstitial.Dispose();
            }
            if (mNativeAd != null)
            {
                mNativeAd.Dispose();
            }

        }
        private class InterstitialAdListener : BaseAdListener
        {
            private Interstitial mInterstitial;
            public InterstitialAdListener(Interstitial interstitial) : base("InterstitialAdListener")
            {
                mInterstitial = interstitial;
            }
            public override void onLoaded(Ad ad)
            {
                base.onLoaded(ad);
                mInterstitial.show();
            }
        }
        private class BannerAdListener : BaseAdListener
        {
            private Banner mBanner;
            public BannerAdListener(Banner banner) : base("BannerAdListener")
            {
                mBanner = banner;
            }
            public override void onLoaded(Ad ad)
            {
                base.onLoaded(ad);
                mBanner.show();
            }
        }
        public delegate void NativeAdOnLoadedCallback(Ad ad);
        private class NativeAdListener : BaseAdListener
        {
            private NativeAdOnLoadedCallback mLoadedCallback;

            public NativeAdListener(NativeAdOnLoadedCallback callback) : base("NativeAdListener")
            {
                mLoadedCallback = callback;
            }
            public override void onLoaded(Ad ad)
            {
                base.onLoaded(ad);
                mLoadedCallback(ad);
            }
        }
    }
#endif
}