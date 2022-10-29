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
        

        // Implement movement here
        private void FixedUpdate()
        {
        
        }
    }
}
