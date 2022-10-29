using CodeBase.Infrastructure;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Services.Input;
using StaticData;
using UI.Services.Factory;
using UI.Services.Windows;
using UnityEngine;

namespace Infrastructure.StateMachine
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      
      RegisterServices();
    }

    public void Enter()
    {
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadProgressState>();
    
    private void RegisterServices()
    {
      RegisterStaticData();

      _services.RegisterSingle<IGameStateMachine>(_stateMachine);
      _services.RegisterSingle<IInputService>(RegisterInputService());
      _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
      _services.RegisterSingle<IAssetProviderService>(new AssetProviderService());
      
      _services.RegisterSingle<IUIFactory>(new UIFactory(
        _services.Single<IStaticDataService>(),
        _services.Single<IPersistentProgressService>(),
        _services.Single<IAssetProviderService>()));
      
      _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
      
      _services.RegisterSingle<IGameFactory>(new GameFactory(
        _services.Single<IAssetProviderService>(),
        _services.Single<IStaticDataService>(),
        _services.Single<IPersistentProgressService>(),
        _services.Single<IWindowService>(),
        _services.Single<IGameStateMachine>()));

      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
        _services.Single<IPersistentProgressService>(), 
        _services.Single<IGameFactory>()));
    }
    
    

    private void RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.LoadData();
      _services.RegisterSingle<IStaticDataService>(staticData);
    }

    private static IInputService RegisterInputService()
    {
#if UNITY_EDITOR
      return new StandaloneInputService();
#elif UNITY_ANDROID
         return new MobileInputService();
#elif UNITY_IOS 
         return new MobileInputService();
#elif UNITY_STANDALONE_OSX
         return new StandaloneInputService();
#elif UNITY_STANDALONE_WIN
         return new StandaloneInputService();
#endif
    }
  }
}