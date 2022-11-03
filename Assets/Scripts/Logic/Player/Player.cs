using System;
using Cinemachine;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Logic.Player
{
    public class Player : MonoBehaviour
    {
        // Fields: Editor 

        // temporary 
        [SerializeField] 
        private float speed; 
        [SerializeField] 
        private float jumpForce;
        
        // Fields: Internal State

        private Rigidbody2D rb;
        private bool jump;
        
        // Services
        
        private IInputService inputService;
        
        // Lifecycle
        
        private void Awake()
        {
            inputService = AllServices.Container.Single<IInputService>();
            rb = GetComponentInChildren<Rigidbody2D>();
        }

        private void Update()
        {
            if (!jump) 
                jump = inputService.IsJumpButtonDown();
        }

        private void FixedUpdate()
        {
            Move();
        }
        
        // Methods: Internal State
        
        private void Move()
        {
            rb.velocity = new Vector2(inputService.Axis.X * speed, rb.velocity.y);

            /*need to add grounded check as well*/
            if (jump)
            {
                Debug.Log("Jump bastard");
                rb.AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }
        }
    }
}
