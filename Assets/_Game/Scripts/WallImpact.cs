using UnityEngine;

namespace Game
{
    public class WallImpact : MonoBehaviour
    {
        public RenderTexture renderTexture; // ���� Render Texture
        public Material drawMaterial;       // �������� ��� ��������� ������
        public Camera renderCamera;         // ������ ��� ���������� ������

        // �����, ���������� ����� ��� ������������
        public void DrawImpact(Vector3 hitPoint, Vector3 hitNormal)
        {
            // ���������� ������ �� ������� ��������� � ���������� � �� �����
            renderCamera.transform.position = hitPoint + hitNormal * 0.01f;
            renderCamera.transform.rotation = Quaternion.LookRotation(-hitNormal);

            // ������ ���� �� Render Texture
            Graphics.Blit(null, renderTexture, drawMaterial);
        }
    }
}