using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public class AndroidList
    {
        private AndroidJavaObject mAndroidList;
        public AndroidList(AndroidJavaObject list)
        {
            mAndroidList = list;
        }

        public int size()
        {
            return mAndroidList.Call<int>("size");
        }

        public AndroidJavaObject at(int index)
        {
            return mAndroidList.Call<AndroidJavaObject>("get", index);
        }

    }
#endif
}
