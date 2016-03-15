using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public interface NativeAdRequestParamBuilder
    {
        NativeAdRequestParamBuilder requestExtraResource(bool flag);

        NativeAdRequestParamBuilder requestCoverImageSize(int width, int height);

        AdRequest.Params build();
    }
#endif
}
