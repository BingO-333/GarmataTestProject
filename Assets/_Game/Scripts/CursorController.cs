using UnityEngine;

namespace Game
{
    public class CursorController : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}