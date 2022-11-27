using Logic.Common;
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
                Destroy(col.gameObject);
                hasKey = true;
            }
        }
    }
}
