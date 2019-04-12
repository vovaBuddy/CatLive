using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Helper
{
    public static class DeviceNameHelper
    {
        static string value;

        public static void Init()
        {
            value = SystemInfo.deviceUniqueIdentifier;

#if UNITY_EDITOR
            value = "UNITY_EDITOR";
#endif
        }

        public static string GetDeviceName()
        {
            return value;
        }
    }
}
