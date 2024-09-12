using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryView : MonoBehaviour
    {
        [SerializeField] GarmataShooter _garmataShooter;

        [SerializeField] int _lenght = 25;

        private LineRenderer _trajectoryLine;

        private void Awake()
        {
            _trajectoryLine = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (_garmataShooter == null || _trajectoryLine == null)
                return;

            UpdateTrajectory();
        }

        private void UpdateTrajectory()
        {
            List<Vector3> points = new List<Vector3>();

            Vector3 simulatedPosition = _garmataShooter.BulletSpawnPos;
            Vector3 simulatedVelocity = _garmataShooter.BulletsVelocity;
            Vector3 simulatedGravity = _garmataShooter.BulletsGravity;

            for (int i = 0; i < _lenght; i++)
            {
                points.Add(simulatedPosition);
                simulatedVelocity += simulatedGravity * Time.fixedDeltaTime;
                simulatedPosition += simulatedVelocity * Time.fixedDeltaTime;
            }

            _trajectoryLine.positionCount = points.Count;
            _trajectoryLine.SetPositions(points.ToArray());
        }
    }
}