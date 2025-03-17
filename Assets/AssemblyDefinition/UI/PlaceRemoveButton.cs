using AssemblyDefinition.Buildings;
using AssemblyDefinition.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyDefinition.UI
{
    public class PlaceRemoveButton : MonoBehaviour
    {
        public Button placeButton;
        public Button removeButton;
        public TextMeshProUGUI text;
        
        [SerializeField] private float fadeDuration = .25f; 
        [SerializeField] private float displayDuration = 3f;
        
        private BuildingStaticData _buildingData;
        private bool _isTextActive;
        private BuildingPlacer _buildingPlacer;
        private BuildingRemover _buildingRemover;

        // Инициализация данных
        public void Construct(BuildingPlacer buildingPlacer, BuildingRemover buildingRemover)
        {
            _buildingPlacer = buildingPlacer;
            _buildingRemover = buildingRemover;
        }
        
        //Передача данных в кнопку
        public void InitButtonInfo(BuildingStaticData buildingData)
        {
            _buildingData = buildingData;
        }

        private void Awake()
        {
            placeButton.onClick.AddListener( PlaceClickEvent);
            removeButton.onClick.AddListener( RemoveClickEvent);
        }

        //Запускаем процесс постройки здания, или если здания не выбранны предупреждаем игрока что надо выбрать здание
        private void PlaceClickEvent()
        {
            if (_buildingData)
            {
                if (_buildingPlacer)
                {
                    _buildingPlacer.StartPlacingBuilding(_buildingData);
                }

                if (_buildingRemover)
                {
                    _buildingRemover.StopRemoval();
                }
                
            }
            else
            {
                if (!_isTextActive)
                {
                    StartCoroutine(ShowAndHideText());
                }
            }
        }
        
        // Запускаем процесс удаления здания
        private void RemoveClickEvent()
        {
            if (_buildingPlacer)
            {
                _buildingPlacer.StopPlacingBuilding();
            }

            if (_buildingRemover)
            {
                _buildingRemover.StartRemoval();
            }
        }

        // Выводим на короткое время текст если здание не выбрано
        private System.Collections.IEnumerator ShowAndHideText()
            {
                _isTextActive = true;
                yield return StartCoroutine(Fade(0, 1, fadeDuration));
                yield return new WaitForSeconds(displayDuration);
                yield return StartCoroutine(Fade(1, 0, fadeDuration));
                
                _isTextActive = false;
            }

            private System.Collections.IEnumerator Fade(float startAlpha, float endAlpha, float duration)
            {
                float elapsedTime = 0;
                Color textColor = text.color;

                while (elapsedTime < duration)
                {
                    textColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                    text.color = textColor;
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                textColor.a = endAlpha; 
                text.color = textColor;
            }
        
    }
}
