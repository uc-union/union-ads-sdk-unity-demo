using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Assets.Mediation_SDK.Library {
#if UNITY_ANDROID
    public interface AdListener
    {
        void onLoaded(Ad ad);

        void onClosed(Ad ad);

        void onOpened(Ad ad);

        void onClicked(Ad ad);

        void onError(Ad ad, AdError error);

    }
#endif
}