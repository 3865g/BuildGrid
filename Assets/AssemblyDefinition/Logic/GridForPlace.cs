using UnityEngine;

namespace AssemblyDefinition.Logic
{
    public class GridForPlace : MonoBehaviour
    {
        [SerializeField] private float gridSize = 1f; 
        [SerializeField] private int gridWidth = 10; 
        [SerializeField] private int gridHeight = 10; 

        // получаем вектор который округлен до ближайшей клетки сетки
        public Vector3 GetNearestPointOnGrid(Vector3 position)
        {
            
            position -= transform.position;
            int xCount = Mathf.RoundToInt(position.x / gridSize);
            int yCount = Mathf.RoundToInt(position.y / gridSize);
            Vector3 result = new Vector3(
                (float)xCount * gridSize,
                (float)yCount * gridSize,
                0
            );
            result += transform.position;
            return result;
        }

        // рисуем сетку для дебага
        private void OnDrawGizmos()
        {
            if (gridSize <= 0 || gridWidth <= 0 || gridHeight <= 0) return;
            
            Gizmos.color = Color.green;
            
            Vector3 offset = new Vector3(
                -gridWidth * gridSize / 2f,
                -gridHeight * gridSize / 2f,
                0
            );

            // Отрисовка вертикальных линий
            for (int x = 0; x <= gridWidth; x++)
            {
                Vector3 start = transform.position + offset + new Vector3(x * gridSize, 0, 0);
                Vector3 end = start + new Vector3(0, gridHeight * gridSize, 0);
                Gizmos.DrawLine(start, end);
            }

            // Отрисовка горизонтальных линий
            for (int y = 0; y <= gridHeight; y++)
            {
                Vector3 start = transform.position + offset + new Vector3(0, y * gridSize, 0);
                Vector3 end = start + new Vector3(gridWidth * gridSize, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}
