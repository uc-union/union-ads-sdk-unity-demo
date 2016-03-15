using Assets.Mediation_SDK.Library.inner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public class AdBase : Ad
    {
        protected AndroidJavaObject mActivity;
        protected AndroidJavaObject mAndroidAd;

        public AdBase()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            mActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public virtual void load(AdRequest adRequest)
        {
            mActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                mAndroidAd.Call("loadAd", adRequest.asAndroidObject());
            }));
        }

        public virtual void setListener(AdListener listener)
        {
            mActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                mAndroidAd.Call("setAdListener", new AdListenerProxy(this, listener));
            }));
        }

        public virtual void stopLoading()
        {
            mActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                mAndroidAd.Call("stopLoading");
            }));
        }
    }
#endif
}
