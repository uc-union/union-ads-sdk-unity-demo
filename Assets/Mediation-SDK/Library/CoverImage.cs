using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public class CoverImage
    {
        private AndroidJavaObject mAndroidImage;
        public CoverImage(AndroidJavaObject img)
        {
            mAndroidImage = img;
        }

        public int getWidth()
        {
            return mAndroidImage.Call<int>("getWidth");
        }

        public int getHeight()
        {
            return mAndroidImage.Call<int>("getHeight");
        }

        public String getUrl()
        {
            return mAndroidImage.Call<String>("getUrl");
        }

        public AndroidJavaObject raw()
        {
            return mAndroidImage;
        }
    }
#endif
}
