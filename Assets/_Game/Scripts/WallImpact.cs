using UnityEngine;

namespace Game
{
    public class WallImpact : MonoBehaviour
    {
        public RenderTexture renderTexture; // Ваша Render Texture
        public Material drawMaterial;       // Материал для рисования следов
        public Camera renderCamera;         // Камера для рендеринга следов

        // Метод, вызываемый пулей при столкновении
        public void DrawImpact(Vector3 hitPoint, Vector3 hitNormal)
        {
            // Перемещаем камеру на позицию попадания и направляем её на стену
            renderCamera.transform.position = hitPoint + hitNormal * 0.01f;
            renderCamera.transform.rotation = Quaternion.LookRotation(-hitNormal);

            // Рисуем след на Render Texture
            Graphics.Blit(null, renderTexture, drawMaterial);
        }
    }
}