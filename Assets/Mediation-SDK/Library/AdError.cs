using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Mediation_SDK.Library
{
#if UNITY_ANDROID
    public abstract class AdError
    {
        public  const int ERROR_CODE_NETWORK_ERROR = 1000;
        public  const int ERROR_CODE_NO_FILL = 1001;
        public  const int ERROR_CODE_INTERNAL_ERROR = 1002;
        public const int ERROR_CODE_SERVER_ERROR = 1003;
        public const int ERROR_CODE_REMOTE_CLOSED = 1004;

        public abstract int getErrorCode();
        public abstract string getErrorMsg();
    }
#endif
}
