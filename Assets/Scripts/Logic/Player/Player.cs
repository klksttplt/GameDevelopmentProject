using System;
using Logic.Common;
using UnityEngine;

namespace Logic.Player
{
    public class Player :  ContextComponent
    {
        [HideInInspector]
        public bool hasKey;

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
