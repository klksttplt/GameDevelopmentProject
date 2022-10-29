using System;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Logic.Player
{
    public class Player : MonoBehaviour
    {
        // Services
        private IInputService inputService;
        
        private void Awake()
        {
            inputService = AllServices.Container.Single<IInputService>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
