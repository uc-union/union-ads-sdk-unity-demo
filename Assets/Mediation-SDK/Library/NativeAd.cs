using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public class NativeAd : AdBase, IDisposable
    {
        private AndroidJavaObject mAndroidNativeAd;
        private AndroidJavaObject mAndroidNativeAdInfo;

        public NativeAd()
        {
            mAndroidNativeAd = new AndroidJavaObject("com.ucweb.union.ads.NativeAd", mActivity);
            mAndroidAd = mAndroidNativeAd;
        }
        public void Dispose()
        {

        }

        public override void load(AdRequest adRequest)
        {
            mAndroidNativeAdInfo = null;
            base.load(adRequest);
        }

        public override void setListener(AdListener listener)
        {
            base.setListener(new NativeAdListener(listener, delegate(Ad ad) {
                mAndroidNativeAdInfo = mAndroidNativeAd.Call<AndroidJavaObject>("getNativeAdAssets");
            }));
        }


        public string getTitle()
        {
            if (mAndroidNativeAdInfo == null)
            {
                return null;
            }
            return mAndroidNativeAdInfo.Call<string>("getTitle");
        }

        public string getDescription()
        {
            if (mAndroidNativeAdInfo == null)
            {
                return null;
            }
            return mAndroidNativeAdInfo.Call<string>("getDescription");
        }

        public string getIconUrl()
        {
            if (mAndroidNativeAdInfo == null)
            {
                return null;
            }
            try
            {
                AndroidJavaObject icon = mAndroidNativeAdInfo.Call<AndroidJavaObject>("getIcon");
                if (icon == null)
                {
                    return "";
                }
                return icon.Call<string>("getUrl");
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
                return "";
            }
        }

        public List<CoverImage> getCovers()
        {
            if (mAndroidNativeAdInfo == null)
            {
                return null;
            }
            try
            {
                AndroidJavaObject covers = mAndroidNativeAdInfo.Call<AndroidJavaObject>("getCovers");
                if (covers == null)
                {
                    return null;
                }
                List<CoverImage> images = new List<CoverImage>();
                AndroidList list = new AndroidList(covers);
                for(int i = 0; i < list.size(); ++i)
                {
                    images.Add(new CoverImage(list.at(i)));
                }
                return images;

            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }
            return null;
        }

        public CoverImage getFilteredCover(int width, int height)
        {
            if (mAndroidNativeAdInfo == null)
            {
                return null;
            }
            try
            {
                AndroidJavaObject covers = mAndroidNativeAdInfo.Call<AndroidJavaObject>("getCovers");
                if (covers == null)
                {
                    return null;
                }
                return ImageFilter.filterImageBySize(covers, width, height);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
            }
            return null;
        }
        public delegate void AdOnLoadedCallback(Ad ad);
        private class NativeAdListener : AdListener
        {
            private AdOnLoadedCallback mCallback;
            private AdListener mNext;
            public NativeAdListener(AdListener next, AdOnLoadedCallback callback)
            {
                mNext = next;
                mCallback = callback;
            }

            public void onClicked(Ad ad)
            {
                mNext.onClicked(ad);
            }

            public void onClosed(Ad ad)
            {
                mNext.onClosed(ad);
            }

            public void onError(Ad ad, AdError error)
            {
                mNext.onError(ad, error);
            }

            public void onLoaded(Ad ad)
            {
                //mCallback must be the first
                mCallback(ad);
                mNext.onLoaded(ad);
            }

            public void onOpened(Ad ad)
            {
                mNext.onOpened(ad);
            }
        }
        
        public  void manualImpression()
        {
            sendIntentToBroadcastManager("com.ucweb.union.ads.native.impression");
        }

        public  void manualClick()
        {
            sendIntentToBroadcastManager("com.ucweb.union.ads.native.click");
        }

        private string getId()
        {
            return mAndroidNativeAdInfo.Call<String>("getNativeId");
        }

        private bool sendIntentToBroadcastManager(string intent)
        {
            if (intent != null)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                AndroidJavaObject clickIntent = new AndroidJavaObject("android.content.Intent", intent + ":" + getId());
                AndroidJavaClass localBroadcastManagerClass = new AndroidJavaClass("android.support.v4.content.LocalBroadcastManager");
                AndroidJavaObject localBroadcastManager =
                    localBroadcastManagerClass.CallStatic<AndroidJavaObject>("getInstance", currentActivity);
                return localBroadcastManager.Call<bool>("sendBroadcast", clickIntent);
            }
            return false;
        }

    }
#endif
}
