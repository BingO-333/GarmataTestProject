using System;
using UnityEngine;

namespace Game
{
    public class GarmataShooter : MonoBehaviour
    {
        public event Action OnShoot;
        public event Action OnPowerChanged;

        public Vector3 BulletsVelocity => _spawnPoint.forward * CurrentShootPower;
        public Vector3 BulletsGravity => Physics.gravity * _gravityMultiplier;
        public Vector3 BulletSpawnPos => _spawnPoint.position;

        public float CurrentShootPower { get; private set; }

        [SerializeField] Bullet _bulletPrefab;
        [Space]
        [SerializeField] Transform _spawnPoint;

        [SerializeField] float _minShootPower = 10f;
        [SerializeField] float _maxShootPower = 150f;

        [SerializeField] float _changePowerSpeed = 30f;

        [SerializeField] float _gravityMultiplier = 2f;

        [SerializeField] float _cooldown = 0.25f;

        private float _lastTimeShoot;

        private void Awake()
        {
            CurrentShootPower = Mathf.Lerp(_minShootPower, _maxShootPower, 0.5f);
            OnPowerChanged?.Invoke();
        }

        private void Update()
        {
            HandleInputs();
        }

        private void SpawnBullet()
        {
            Bullet spawnedBullet = LevelManager.Instance.BulletPool.GetBullet();

            spawnedBullet.transform.position = BulletSpawnPos;
            spawnedBullet.transform.rotation = _spawnPoint.rotation;

            spawnedBullet.SetupVelocity(BulletsVelocity, BulletsGravity);

            if (LevelManager.Instance != null)
                LevelManager.Instance.CameraController.Shake();

            OnShoot?.Invoke();
        }

        private void HandleInputs()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _lastTimeShoot + _cooldown < Time.time)
            {
                SpawnBullet();
                _lastTimeShoot = Time.time;
            }

            if (Input.GetKey(KeyCode.W))
            {
                CurrentShootPower = Mathf.Clamp(CurrentShootPower + Time.deltaTime * _changePowerSpeed, _minShootPower, _maxShootPower);
                OnPowerChanged?.Invoke();
            }

            if (Input.GetKey(KeyCode.S))
            {
                CurrentShootPower = Mathf.Clamp(CurrentShootPower - Time.deltaTime * _changePowerSpeed, _minShootPower, _maxShootPower);
                OnPowerChanged?.Invoke();
            }
        }
    }
}