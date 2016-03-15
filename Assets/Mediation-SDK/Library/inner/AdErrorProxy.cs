using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library.inner
{
#if UNITY_ANDROID
    public class AdErrorProxy : AdError
    {
        private AndroidJavaObject mAndroidAdError;
        public AdErrorProxy(AndroidJavaObject adError)
        {
            mAndroidAdError = adError;
        }

        public override int getErrorCode()
        {
           return mAndroidAdError.Call<int>("getErrorCode");
        }

        public override string getErrorMsg()
        {
            return mAndroidAdError.Call<string>("getErrorMessage");
        }
    }
#endif
}
