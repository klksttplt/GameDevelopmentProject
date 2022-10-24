using System;
using UI.Services.Windows;

namespace StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public BaseWindow Prefab;
    }
}