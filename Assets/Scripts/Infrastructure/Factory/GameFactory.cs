using System.Collections.Generic;
using CameraLogic;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.StateMachine;
using Logic.Player;
using StaticData;
using UI.Elements;
using UI.Services.Windows;
using UnityEngine;

namespace Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public GameObject Player { get; private set; }

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    private GameObject PlayerGameObject { get; set; }

    private readonly IAssetProviderService _assets;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;
    private readonly IWindowService _windowService;
    private readonly IGameStateMachine _stateMachine;

    public GameFactory(
      IAssetProviderService assets, 
      IStaticDataService staticData,
      IPersistentProgressService progressService, 
      IWindowService windowService,
      IGameStateMachine stateMachine)
    {
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
      _windowService = windowService;
      _stateMachine = stateMachine;
    }

    public GameObject CreatePlayerObject(Vector3 at)
    {
      PlayerGameObject = InstantiateRegistred(AssetPath.HeroPath, at);
      Player = PlayerGameObject;
      return PlayerGameObject;
    }

    public GameObject CreateEnemyObject(Vector3 at)
    {
      return _assets.Instantiate(AssetPath.EnemyPath, at);
    }
    public CinemachineSwitcher CreateCinemachine()
    {
      return _assets.Instantiate(AssetPath.CinemachinePath).GetComponent<CinemachineSwitcher>();
    }

    public GameObject CreateHud()
    {
      GameObject hud = InstantiateRegistred(AssetPath.HudPath);
      //hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);

      foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>()) 
        openWindowButton.Construct(_windowService);

      return hud;
    }

    public GameObject CreateItem(Vector3 at)
    {
      return _assets.Instantiate(AssetPath.KeyPath, at);
    }


    public void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter) 
        ProgressWriters.Add(progressWriter);

      ProgressReaders.Add(progressReader);
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
      
      _assets.CleanUp();
    }
    

    private GameObject InstantiateRegistred(string prefab, Vector3 position)
    {
      GameObject playerGameObject = _assets.Instantiate(prefab, position, Quaternion.identity);
      RegisterProgressWatchers(playerGameObject);
      return playerGameObject;
    }

    private GameObject InstantiateRegistred(string prefab)
    {
      GameObject heroGameObject = _assets.Instantiate(prefab);
      RegisterProgressWatchers(heroGameObject);
      return heroGameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>()) 
        Register(progressReader);
    }
  }


  public static class AssetPath
  {
    public const string HeroPath = "Prefabs/Player/Player";
    public const string EnemyPath = "Prefabs/Enemy/Skeleton";
    public const string HudPath = "Prefabs/UI/Hud";
    public const string CinemachinePath = "Prefabs/Camera/CM StateDrivenCamera";
    public const string KeyPath = "Prefabs/Items/Key";
  }
}