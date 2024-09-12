using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _defaultVC;

        [SerializeField] float _cameraShakeDuraction = 0.2f;

        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

        private Tween _shakeDissableDelayTween;

        private void Awake()
        {
            _cinemachineBasicMultiChannelPerlin = _defaultVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        
        public void Shake(float intencity = 1f)
        {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intencity;

            _shakeDissableDelayTween.KillIfActiveAndPlaying();
            _shakeDissableDelayTween = DOVirtual.DelayedCall(_cameraShakeDuraction, () => _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0);
        }
    }
}

