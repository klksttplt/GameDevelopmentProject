using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic.Common;
using UnityEngine;

namespace Logic.Enemy
{
    public class Enemy : ContextComponent
    {
        [SerializeField] 
        private bool spawnItemsAfterDeath;
        [SerializeField]
        private List<GameObject> itemsToSpawn;
        
        // Services

        private IGameFactory gameFactory;
        
        public override void Awake()
        {
            base.Awake();
            Health.OnDied.AddListener(() =>
            {
                if (spawnItemsAfterDeath)
                {
                    spawnItemsAfterDeath = false;
                    foreach (var item in itemsToSpawn)
                        gameFactory.CreateItem(transform.position);}
                Destroy(gameObject);
            });
            gameFactory = AllServices.Container.Single<IGameFactory>();
        }
    }
}
