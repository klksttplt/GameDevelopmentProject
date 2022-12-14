using System;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Logic.LevelTransition
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private MMFeedbacks transferFeedbacks;
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
            if (player && player.hasKey)
            {
                Debug.Log(player + " "  + player.hasKey);
                transferFeedbacks?.PlayFeedbacks();
                stateMachine.Enter<LoadLevelState, string>(transferTo);
            }
        }
    }
}
