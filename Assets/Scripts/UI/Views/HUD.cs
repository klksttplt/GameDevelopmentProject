using System;
using Logic.Damage;
using Logic.Stats;
using UI.Icons;
using UnityEngine;

namespace UI.Views
{
    public class HUD : MonoBehaviour
    {
        // Fields: Editor

        [SerializeField, Header("Health")]
        private RectTransform healthContainer;
        [SerializeField] private StatDef healthDef;        
        [SerializeField] private Icon healthIcon;

        // Public API
        
        public void SetupGUI(Health playerHealth)
        {
            playerHealth.OnHealthChange.AddListener(UpdateHealthPanel);
            UpdateHealthPanel(playerHealth.MaxValue);
        }

        // Methods: Internal State

        private void UpdateHealthPanel(float currentHealth)
        {
            foreach (Transform child in healthContainer.transform) 
                Destroy(child.gameObject);
            
            for (var i = 0; i < currentHealth; i++)
                Instantiate(healthIcon, healthContainer).Setup(healthDef.SpriteIcon);
        }
    }
}
