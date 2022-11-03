using Cinemachine;
using UnityEngine;

namespace CameraLogic
{
    public class CinemachineSwitcher : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera forwardCamera;

        const string cameraName = "NextCamera";
        private Animator _animator;
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void ChangeCamera(string nextCamera)
        {
            _animator.Play(nextCamera);
        }

        public void SetPlayerTarget(Transform player)
        {
            forwardCamera.m_Follow = player;
            // forwardCamera.m_LookAt = player;
        }
    }
}