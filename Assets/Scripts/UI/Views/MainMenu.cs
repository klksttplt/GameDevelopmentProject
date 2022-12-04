using Infrastructure.Services;
using Infrastructure.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
        
        private IGameStateMachine stateMachine;
        
        private void Awake()
        {
            stateMachine = AllServices.Container.Single<IGameStateMachine>();
            
            playButton.onClick.AddListener(Play);
            exitButton.onClick.AddListener(Exit);
        }

        private void Play()
        {
            stateMachine.Enter<LoadLevelState, string>("Level 1");
        }

        private void Exit()
        {
            Debug.Log("Application.Quit()");
            Application.Quit();
        }
    }
}
