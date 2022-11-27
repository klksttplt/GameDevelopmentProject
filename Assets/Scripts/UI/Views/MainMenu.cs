using Infrastructure.Services;
using Infrastructure.StateMachine;
using UI.Services.Windows;
using UnityEngine;

namespace UI.Views
{
    public class MainMenu : BaseWindow
    {
        private IGameStateMachine stateMachine;
        
        protected override void OnAwake()
        {
            base.OnAwake();
            stateMachine = AllServices.Container.Single<IGameStateMachine>();

        }

        public void Play()
        {
            stateMachine.Enter<LoadLevelState, string>("Level 1");
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
