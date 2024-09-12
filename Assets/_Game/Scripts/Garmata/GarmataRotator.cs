using UnityEngine;

namespace Game
{
    public class GarmataRotator : MonoBehaviour
    {
        [SerializeField] Transform _horizontalPivot;
        [SerializeField] Transform _verticalPivot;
        [Space]
        [SerializeField] float _horizontalSpeed = 1f;
        [SerializeField] float _verticalSpeed = 1f;

        [SerializeField] float _verticalMaxAngle = 80f;

        private float _verticalAngle;

        private void Update()
        {
            HorizontalRotate(Input.GetAxis("Mouse X") * _horizontalSpeed * Time.deltaTime);
            VericalRotate(Input.GetAxis("Mouse Y") * _verticalSpeed * Time.deltaTime);
        }

        private void HorizontalRotate(float deltaX)
        {
            _horizontalPivot.Rotate(0, deltaX, 0);
        }

        private void VericalRotate(float deltaY)
        {
            _verticalAngle = Mathf.Clamp(_verticalAngle + deltaY, 0, _verticalMaxAngle);

            Vector3 localAngles = _verticalPivot.localEulerAngles;
            localAngles.x = -_verticalAngle;

            _verticalPivot.localEulerAngles = localAngles;
        }
    }
}