using UnityEngine;

namespace Infrastructure.Basic
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper bootstrapper;
    
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (!bootstrapper) 
                Instantiate(this.bootstrapper);
        }
    }
}
