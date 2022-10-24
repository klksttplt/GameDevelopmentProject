using CodeBase.Infrastructure;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure.Basic
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
        }
    }
}
