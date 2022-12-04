using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic.Damage;
using StaticData;
using StaticData.Windows;
using UI.Elements;
using UI.Services.Windows;
using UI.Views;
using UnityEngine;

namespace UI.Services.Factory
{
    class UIFactory : IUIFactory
    {
        private const string UIRootPath = "Prefabs/UI/UIRoot";
        private readonly IStaticDataService staticData;
        private readonly IAssetProviderService _assets;
        private Transform uiRoot;
        private readonly IPersistentProgressService progressService;
        private IWindowService windowService;

        public HUD Hud { get; private set; }

        public UIFactory(
            IStaticDataService staticData, 
            IPersistentProgressService progressService, 
            IAssetProviderService assets)
        {
            this.staticData = staticData;
            this.progressService = progressService;
            _assets = assets;
        }

        public void CreateHud(Health playerHealth)
        {
            windowService = AllServices.Container.Single<IWindowService>();
            Hud = _assets.Instantiate(AssetPath.HudPath).GetComponent<HUD>();
            Hud.SetupGUI(playerHealth);

            foreach (OpenWindowButton openWindowButton in Hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(windowService);
        }



        public void CreateSettings()
        {
            WindowConfig config = staticData.ForWindow(WindowId.Settings);
            Object.Instantiate(config.Prefab, uiRoot);
        }

        public void CreateMenu()
        {
            _assets.Instantiate(AssetPath.MenuPath, uiRoot);
        }

        public void CreateUIRoot()
        {
            uiRoot = _assets.Instantiate(UIRootPath).transform;
        }
    }
}