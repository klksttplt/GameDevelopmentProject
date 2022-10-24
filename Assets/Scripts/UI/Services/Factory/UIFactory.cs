using Infrastructure.AssetManagement;
using Infrastructure.Services;
using StaticData;
using StaticData.Windows;
using UI.Services.Windows;
using UnityEngine;

namespace UI.Services.Factory
{
    class UIFactory : IUIFactory
    {
        private const string UIRootPath = "Prefabs/UI/UIRoot";
        private readonly IStaticDataService _staticData;
        private IAssetProviderService _assets;
        private Transform _uiRoot;
        private readonly IPersistentProgressService _progressService;


        public UIFactory(
            IStaticDataService staticData, 
            IPersistentProgressService progressService, 
            IAssetProviderService assets)
        {
            _staticData = staticData;
            _progressService = progressService;
            _assets = assets;
        }

        public void CreateSettings()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Settings);
            Object.Instantiate(config.Prefab, _uiRoot);
        }

        public void CreateUIRoot()
        {
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
        }
    }
}