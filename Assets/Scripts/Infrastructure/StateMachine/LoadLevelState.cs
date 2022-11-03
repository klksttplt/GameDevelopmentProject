using CameraLogic;
using CodeBase.Infrastructure;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using StaticData;
using UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.StateMachine
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly IUIFactory _uiFactory;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
      IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory)
    {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticData = staticData;
      _uiFactory = uiFactory;
    }

    public void Enter(string sceneName)
    {
      _gameFactory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }
    
    public void Exit()
    {
    }

    private  void OnLoaded()
    {
      InitUIRoot();
      InitGameWorld();
      InformProgressReaders();
      
      _gameStateMachine.Enter<GameLoopState>();
    }

    private void InitUIRoot() => 
      _uiFactory.CreateUIRoot();

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders) 
        progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitGameWorld()
    {
      LevelStaticData levelData = LevelStaticData();
      
      GameObject player = _gameFactory.CreatePlayerObject(levelData.InitialHeroPosition);
      InitHud(player);
      CameraFollow(player);
    }

    private LevelStaticData LevelStaticData() => 
      _staticData.ForLevel(SceneManager.GetActiveScene().name);
    
    private void InitHud(GameObject hero) => 
      _gameFactory.CreateHud();

    private void CameraFollow(GameObject hero)
    {
      var cinemachine = _gameFactory.CreateCinemachine();
      cinemachine.SetPlayerTarget(hero.transform.transform);
    }
    
  }
}