using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BulletsPooler : MonoBehaviour
    {
        [SerializeField] Bullet _bulletPrefab;
        [SerializeField] int _initialPoolSize = 10;

        private Queue<Bullet> _bulletPool = new Queue<Bullet>();

        private void Awake()
        {
            for (int i = 0; i < _initialPoolSize; i++)
                AddBulletToPool();
        }

        public Bullet GetBullet()
        {
            if (_bulletPool.Count == 0)
                AddBulletToPool();

            Bullet bullet = _bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        private void AddBulletToPool()
        {
            Bullet bullet = Instantiate(_bulletPrefab, transform);
            bullet.gameObject.SetActive(false);
            _bulletPool.Enqueue(bullet);

            bullet.OnExpire += ReturnBullet;
        }

        private void ReturnBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }
    }
}
