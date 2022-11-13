using System;
using UnityEngine;
using UnityEngine.Events;

namespace Logic.Combat.Player
{
    public class Blade : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent<Collider2D> OnHit = new UnityEvent<Collider2D>();

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnHit.Invoke(col);
        }
    }
}