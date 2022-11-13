using System;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Logic.LevelTransition
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        public string transferTo;
        private IGameStateMachine _stateMachine;

        private bool _triggered;
        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggered)
                return;

            var player = other.GetComponent<Player.Player>();
            Debug.Log(player + " "  + player.hasKey);
            if (player && player.hasKey) 
                _stateMachine.Enter<LoadLevelState, string>(transferTo);
        }
    }
}
