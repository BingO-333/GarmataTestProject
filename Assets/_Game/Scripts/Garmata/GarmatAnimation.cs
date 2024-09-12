using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class GarmatAnimation : MonoBehaviour
    {
        [SerializeField] GarmataShooter _garmateShooter;

        private Tweener _scaleTweener;
        private Tweener _moveTweener;

        private void Awake()
        {
            if (_garmateShooter != null)
                _garmateShooter.OnShoot += PlayAnimation;
        }

        private void PlayAnimation()
        {
            _scaleTweener.KillIfActiveAndPlaying();
            _moveTweener.KillIfActiveAndPlaying();

            _scaleTweener = transform.DOScale(Vector3.one * 1.1f, 0.1f)
                .ChangeStartValue(Vector3.one)
                .SetLoops(2, LoopType.Yoyo);

            _moveTweener = transform.DOLocalMove(new Vector3(0, 0, -0.5f), 0.1f)
                .ChangeStartValue(Vector3.zero)
                .SetLoops(2, LoopType.Yoyo);
        }
    }
}