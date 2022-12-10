using Infrastructure.Services;
using Logic.Common;
using UI.Services.Factory;
using UnityEngine;

namespace Logic.Player
{
    public class Player : ContextComponent
    {
        [HideInInspector]
        public bool hasKey;

        public override void Awake()
        {
            base.Awake();
            Health.OnDied.AddListener(() =>
            {
                Animable.Die();
                Destroy(gameObject, 5f);
            });
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag($"key"))
            {
                Debug.Log("GOT KEY! GO TO THE PORTAL");
                AllServices.Container.Single<IUIFactory>().Hud.UpdateKey(true);
                Destroy(col.gameObject);
                hasKey = true;
            }

            if (col.CompareTag($"soul"))
            {
                PlayerPrefs.SetInt("Souls", PlayerPrefs.GetInt("Souls")+1);
                Destroy(col.gameObject);

            }
        }
    }
}
