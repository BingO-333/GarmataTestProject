using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class DecalsPooler : MonoBehaviour
    {
        [SerializeField] Transform[] _decals;

        private int _decalIndex;

        private void Awake()
        {
            foreach (var decal in _decals)
                decal.gameObject.SetActive(false);
        }

        public void SpawnDecal(RaycastHit raycastHit)
        {
            Transform decal = GetDecal();

            decal.gameObject.SetActive(true);

            decal.transform.position = raycastHit.point + raycastHit.normal * 0.1f;
            decal.transform.LookAt(raycastHit.point - raycastHit.normal);

            Vector3 decalAngles = decal.transform.localEulerAngles;
            Vector3 decalScale = Vector3.one * Random.Range(0.7f, 1.1f);

            decalAngles.z = Random.Range(0, 360f);

            decal.transform.localEulerAngles = decalAngles;
            decal.transform.localScale = decalScale;
        }

        private Transform GetDecal()
        {
            Transform currentDecal = _decals[_decalIndex];
            _decalIndex++;

            if (_decalIndex >= _decals.Length)
                _decalIndex = 0;

            return currentDecal;
        }

        [Button]
        void GetDecalsInChildren()
        {
            _decals = GetComponentsInChildren<Transform>(true)
                .Where(t => t != transform)
                .ToArray();
        }
    }
}
