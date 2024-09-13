using UnityEngine;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton

        public static LevelManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }

            Destroy(gameObject);
        }

        #endregion

        [field: SerializeField] public CameraController CameraController { get; private set; }
        [field: SerializeField] public BulletsPooler BulletPool { get; private set; }
        [field: SerializeField] public DecalsPooler DecalsPooler { get; private set; }
    }
}