using System.Collections.Generic;
using CameraLogic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject Player { get;  }
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreatePlayerObject(Vector3 at);
        GameObject CreateEnemyObject(Vector3 at);

        CinemachineSwitcher CreateCinemachine();
        // GameObject CreateHud();
        GameObject CreateItem(Vector3 at);
        
        void Cleanup();
    }
}