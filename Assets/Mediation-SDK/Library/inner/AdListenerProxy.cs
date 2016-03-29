using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library.inner
{
#if UNITY_ANDROID
    public class AdListenerProxy : AndroidJavaProxy
    {
        private AdListener mListener;
        private Ad mAd;
        public AdListenerProxy(Ad ad, AdListener listener):base("com.ucweb.union.ads.AdListener")
        {
            mAd = ad;
            mListener = listener;
        }

        public void onAdLoaded(AndroidJavaObject ad)
        {
            mListener.onLoaded(mAd);
        }

        public void onAdClosed(AndroidJavaObject ad)
        {
            mListener.onClosed(mAd);
        }

        public void onAdShowed(AndroidJavaObject ad)
        {
            mListener.onOpened(mAd);
        }

        public void onAdClicked(AndroidJavaObject ad)
        {
            mListener.onClicked(mAd);
        }

        public void onAdError(AndroidJavaObject ad, AndroidJavaObject error)
        {
            mListener.onError(mAd, new AdErrorProxy(error));
        }


    }
#endif
}
