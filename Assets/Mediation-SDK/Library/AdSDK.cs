using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public class AdSDK
    {
        public static void start()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject mainActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject application = mainActivity.Call<AndroidJavaObject>("getApplicationContext");
            AndroidJavaClass mediationSdk = new AndroidJavaClass("com.ucweb.union.ads.UnionAdsSdk");
            mediationSdk.CallStatic("start", application);

        }
    }
#endif
}
