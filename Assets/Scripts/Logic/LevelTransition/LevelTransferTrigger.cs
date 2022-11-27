using System;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Logic.LevelTransition
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        public string transferTo;
        private IGameStateMachine stateMachine;

        private bool triggered;
        private void Awake()
        {
            stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (triggered)
                return;

            var player = other.GetComponent<Player.Player>();
            Debug.Log(player + " "  + player.hasKey);
            if (player && player.hasKey) 
                stateMachine.Enter<LoadLevelState, string>(transferTo);
        }
    }
}
