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
        private HUD hud;

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
            hud = _assets.Instantiate(AssetPath.HudPath).GetComponent<HUD>();
            hud.SetupGUI(playerHealth);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(windowService);
        }
        
        public void CreateSettings()
        {
            WindowConfig config = staticData.ForWindow(WindowId.Settings);
            Object.Instantiate(config.Prefab, uiRoot);
        }

        public void CreateUIRoot()
        {
            uiRoot = _assets.Instantiate(UIRootPath).transform;
        }
    }
}