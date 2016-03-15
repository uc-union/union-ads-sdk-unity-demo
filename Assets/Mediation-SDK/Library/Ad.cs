using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public interface Ad
    {
        void setListener(AdListener listener);
        void load(AdRequest request);
        void stopLoading();
    }
#endif
}
