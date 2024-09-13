using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnExpire;

        [SerializeField] MeshRenderer _meshRenderer;
        [SerializeField] ParticleSystem _explodeParticle;
        [Space]
        [SerializeField] float _sphereRadius = 0.25f;
        [SerializeField] int _maxRicoshetsCount = 1;

        [SerializeField] float _autoExplodeDelay = 5f;

        private Coroutine _autoExplodeDelayCoroutine;

        private Vector3 _velocity;
        private Vector3 _gravity;

        private int _currentRicoshetsCount;

        private bool _isExploded;

        public void SetupVelocity(Vector3 velocity, Vector3 gravity)
        {
            _velocity = velocity;
            _gravity = gravity;
        }

        private void OnEnable()
        {
            ResetBullet();
            _autoExplodeDelayCoroutine = StartCoroutine(AutoExplode());
        }

        private void FixedUpdate()
        {
            if (_isExploded)
                return;

            CheckForCollision();
            ApplyVelocity();
        }

        private void ResetBullet()
        {
            _meshRenderer.enabled = true;
            _isExploded = false;
            _currentRicoshetsCount = 0;

            _velocity = Vector3.zero;
            _gravity = Vector3.zero;
        }

        private void CheckForCollision()
        {
            Ray ray = new Ray(transform.position, _velocity.normalized);
            float distance = _velocity.magnitude * Time.fixedDeltaTime;

            if (Physics.SphereCast(ray, _sphereRadius, out RaycastHit hit, distance))
            {
                LevelManager.Instance.DecalsPooler.SpawnDecal(hit);
                HandleCollision(hit);
            }
        }

        private void HandleCollision(RaycastHit hit)
        {
            if (_currentRicoshetsCount >= _maxRicoshetsCount)
                Explode();
            else
            {
                _velocity = Vector3.Reflect(_velocity, hit.normal);
                _currentRicoshetsCount++;
            }
        }

        private void ApplyVelocity()
        {
            _velocity += _gravity * Time.fixedDeltaTime;
            transform.position += _velocity * Time.fixedDeltaTime;
        }

        private void Explode()
        {
            if (_isExploded)
                return;

            if (_autoExplodeDelayCoroutine != null)
                StopCoroutine(_autoExplodeDelayCoroutine);

            _isExploded = true;

            _meshRenderer.enabled = false;
            _explodeParticle.Play();

            StartCoroutine(DisableDelay());
        }

        private IEnumerator AutoExplode()
        {
            yield return new WaitForSeconds(_autoExplodeDelay);
            Explode();
        }

        private IEnumerator DisableDelay()
        {
            yield return new WaitForSeconds(3f);

            OnExpire?.Invoke(this);
            gameObject.SetActive(false);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _sphereRadius);
        }
    }
}