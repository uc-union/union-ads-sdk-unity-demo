using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public class AdRequest
    {
        public class Params
        {
            private readonly AndroidJavaObject mAndroidParams;
            public Params(AndroidJavaObject androidParams)
            {
                mAndroidParams = androidParams;
            }

            public AndroidJavaObject asAndroidObject()
            {
                return mAndroidParams;
            }
        }

        public class Builder
        {
            private AndroidJavaObject mAndroidBuilder;
            public Builder()
            {
                mAndroidBuilder = new AndroidJavaObject("com.ucweb.union.ads.AdRequest$Builder");
            }

            public Builder pub(string pub)
            {
                mAndroidBuilder.Call<AndroidJavaObject>("pub", pub);
                return this;
            }

            public Builder testDeviceId(string testDeviceId)
            {
                mAndroidBuilder.Call<AndroidJavaObject>("testDeviceId", testDeviceId);
                return this;
            }

            public Builder withParams(Params param)
            {
                mAndroidBuilder.Call<AndroidJavaObject>("withOption", param.asAndroidObject());
                return this;
            }

            public AdRequest build()
            {
                return new AdRequest(mAndroidBuilder.Call<AndroidJavaObject>("build"));
            }
        }
        private AndroidJavaObject mAndroidAdRequest;
        public AdRequest(AndroidJavaObject androidAdRequest)
        {
            mAndroidAdRequest = androidAdRequest;
        }
        public AndroidJavaObject asAndroidObject ()
        {
            return mAndroidAdRequest;
        }

    }
#endif
}
