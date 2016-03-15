using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public class Interstitial : AdBase, IDisposable
    {
        private AndroidJavaObject mAndroidInterstitialAd;

        public Interstitial()
        {
            mAndroidInterstitialAd = new AndroidJavaObject("com.ucweb.union.ads.InterstitialAd", mActivity);
            mAndroidAd = mAndroidInterstitialAd;
        }
        public void Dispose()
        {
            
        }
        public void show()
        {
            mActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                mAndroidAd.Call("show");
            }));
        }
    }
#endif
}
