using Assets.Mediation_SDK.Library.inner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public class Banner : AdBase, IDisposable
    {
        private AndroidJavaObject mAndroidAdView;
        private Rect mRect;
        private bool mHasAttachToParent = false;
        public Banner()
        {
            mAndroidAdView = new AndroidJavaObject("com.ucweb.union.ads.BannerAdView", mActivity);
            mAndroidAd = mAndroidAdView;
        }

        public void show()
        {
            Debug.Log("Banner::show");
            mActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                showInRect(mRect.xMin, mRect.yMin, mRect.width, mRect.height);
            }));
        }


        public void setShowRect(Rect rect)
        {
            mRect = rect;
        }

        private void showInRect(double x,
                           double y,
                           double width,
                           double height)
        {
            Debug.Log("Banner::showInRect[x:" + x + "][y:" + y + "][w:" + width + "][h:" + height +"]");

            AndroidJavaClass R = new AndroidJavaClass("android.R$id");
            AndroidJavaObject view = mActivity.Call<AndroidJavaObject>("findViewById", R.GetStatic<int>("content"));
            AndroidJavaObject layoutParams = null;
            if (!mHasAttachToParent)
            {
                Debug.Log("Banner::First attach to Parent!");
                layoutParams = new AndroidJavaObject("android.view.ViewGroup$MarginLayoutParams", (int)width, (int)height);
                view.Call("addView", mAndroidAdView, layoutParams);
                mHasAttachToParent = true;
            }
            // Can not use else here!!!!TODO: find out the reason and fix it.
           // else {
                Debug.Log("Banner::AttachToParent");
                layoutParams = mAndroidAdView.Call<AndroidJavaObject>("getLayoutParams");
          //  }
            layoutParams.Call("setMargins", (int)x, (int)y, 0, 0);
        }

        private void removeAdViewFromParent()
        {
            if (!mHasAttachToParent)
            {
                return;
            }
            AndroidJavaObject parent = mAndroidAdView.Call<AndroidJavaObject>("getParent");
            parent.Call("removeView", mAndroidAdView);
            mHasAttachToParent = false;
        }

        public void Dispose()
        {
            Debug.Log("Banner::Dispose");
            mActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                removeAdViewFromParent();
            }));
        }
    }
#endif
}
