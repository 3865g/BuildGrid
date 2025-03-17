using System;
using AssemblyDefinition.StaticData;
using UnityEngine;

namespace AssemblyDefinition.Buildings
{
    public class Building : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;   
        public BoxCollider2D boxCollider; 

        private BuildingStaticData _buildingData;
        private string _id; 

        public string Id => _id;

        // инициализируем данные
        public void Construct(BuildingStaticData buildingData, string id)
        {
            _buildingData = buildingData;
            _id = id ?? Guid.NewGuid().ToString();
            InitializeBuilding();
        }

        // создаем префаб согласно данным из статик даты
        public void InitializeBuilding()
        {
            Vector2 size = _buildingData.buildingsize;
            
            UpdateCollider(size);
            UpdateSprite(size);
        }

        private void UpdateCollider(Vector2 size)
        {
            boxCollider.size =  new Vector2(size.x - .1f, size.y - .1f);
            boxCollider.offset = Vector2.zero;
        }

        private void UpdateSprite(Vector2 size)
        {
            if (!spriteRenderer)
            {
                return;
            }

            // Определяем, какая сторона больше и растягиваем спрайт по большей стороне
            float maxSide = Mathf.Max(size.x, size.y);
            spriteRenderer.transform.localScale = new Vector2(maxSide, maxSide);
            
            spriteRenderer.transform.localPosition = Vector2.zero;
            if (_buildingData.buildingIcon)
            {
                spriteRenderer.sprite = _buildingData.buildingIcon;
            }
            else
            {
                Debug.LogWarning("Иконка не назначена в статик дате");
            }
        }

        // функция для смены цвета в момент выбора позиции
        public void UpdateColor(Color color)
        {
            spriteRenderer.color = color;
        }
        
    }
}