using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Mediation_SDK.Library.inner;
using UnityEngine;

namespace Assets.Mediation_SDK.Library
{
    class ImageFilter
    {
        public static CoverImage filterImageBySize(AndroidJavaObject imgs, int w, int h)
        {
            try
            {
                AndroidJavaClass imageFilter = new AndroidJavaClass("com.ucweb.union.ads.ImageFilter");
                AndroidJavaObject img = imageFilter.CallStatic<AndroidJavaObject>("filter", imgs, w, h);
                if (img != null)
                {
                    return new CoverImage(img);
                }
            }catch(Exception e)
            {
                Debug.Log(e.Message);
            }
            return null;
        }
    }
}
