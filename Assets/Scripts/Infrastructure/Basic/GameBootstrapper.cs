using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure.Basic
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game game;

        private void Awake()
        {
            game = new Game(this);
            game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
