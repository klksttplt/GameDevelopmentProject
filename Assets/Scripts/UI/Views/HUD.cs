using System;
using GameUtils;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using Logic.Damage;
using Logic.Stats;
using MoreMountains.Feedbacks;
using TMPro;
using UI.Icons;
using UI.Services.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class HUD : MonoBehaviour
    {
        // Fields: Editor

        [SerializeField] private Icon Icon;

        [SerializeField, Header("Health")]
        private RectTransform healthContainer;
        [SerializeField] private StatDef healthDef;

        [SerializeField, Header("Souls")]
        private Icon soulIcon;
        [SerializeField] private TMP_Text soulText;
        [SerializeField] private StatDef soulDef;
        
        [SerializeField, Header("Key")] 
        private RectTransform keyHolder;
        [SerializeField] private Icon keyIcon;
        [SerializeField] private Sprite keySprite;

        [SerializeField, Header("Pause")] 
        private GameObject pauseObject;
        [SerializeField] private Button pauseButton;
        [SerializeField] private MMFeedbacks pauseFeedbacks;
            
        [SerializeField, Header("Pause")] 
        private GameObject loseObject;
        [SerializeField] private Button restartButton; 
        
        // Fields: Internal State

        private IGameStateMachine stateMachine;
        private IUIFactory uiFactory;
        private float refreshTime = .5f;
        
        // Public API
        
        public void SetupGUI(Health playerHealth)
        {
            this.UpdatePeriodically(refreshTime, Refresh);
            
            playerHealth.OnHealthChange.AddListener(UpdateHealthPanel);
            UpdateHealthPanel(playerHealth.MaxValue);
            
            soulIcon.Setup(soulDef.SpriteIcon);
            
            keyIcon.Setup(keySprite);
            UpdateKey(false);
            
            pauseButton.onClick.AddListener(Pause);
            restartButton.onClick.AddListener(Restart);
        }
        
        public void UpdateKey(bool inStock)
        {
            keyHolder.gameObject.SetActive(inStock);
        }

        public void EnableLoseScreen()
        {
            loseObject.SetActive(true);
        }
        
        // Methods: Lifecycle

        private void Awake()
        {
            stateMachine = AllServices.Container.Single<IGameStateMachine>();
            uiFactory = AllServices.Container.Single<IUIFactory>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
                Pause();
        }
        
        private void OnDestroy()
        {
            pauseButton.onClick.RemoveListener(Pause);
            restartButton.onClick.RemoveListener(Restart);
        }

        // Methods: Internal State

        private void Refresh()
        {
            soulText.text = $"x{PlayerPrefs.GetInt("Souls")}";
        }
        
        private void UpdateHealthPanel(float currentHealth)
        {
            foreach (Transform child in healthContainer.transform) 
                Destroy(child.gameObject);
            
            for (var i = 0; i < currentHealth; i++)
                Instantiate(Icon, healthContainer).Setup(healthDef.SpriteIcon);
        }

        private void Pause()
        {
            if (loseObject.activeSelf)
                return;
            
            pauseFeedbacks.PlayFeedbacks();
            pauseObject.SetActive(!pauseObject.activeSelf);
            Time.timeScale = pauseObject.activeSelf ? 0 : 1;
        }

        private void Restart()
        {
            stateMachine.Enter<LoadLevelState, string>("MainMenu");
        }

        
    }
}
