using AssemblyDefinition.Buildings;
using AssemblyDefinition.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyDefinition.UI
{
    public class SelectBuildingButton : MonoBehaviour
    {
        public Toggle toggle;
        public Image icon;
        public Outline outline;
        
        private BuildingStaticData _buildingData;
        
        //Инициализируем данные
        public void Construct(BuildingStaticData buildingData)
        {
            _buildingData = buildingData;
            
            SetIcon(_buildingData.buildingIcon);
            toggle.onValueChanged.AddListener(SelectButton);
        }
        // устанавливаем иконку для конпки
        private void SetIcon(Sprite buildingIcon)
        {
            if (buildingIcon)
            {
                icon.sprite = buildingIcon;
            }
        }

        //При выборе кнопки передаем статик дату на основе которой будем строить здание
        private void SelectButton(bool toggle)
        {
            Color outlineColor = outline.effectColor;
            
            if (toggle)
            {
                PlaceRemoveButton placeRemoveButton = GetComponentInParent<PlaceRemoveButton>();
                placeRemoveButton.InitButtonInfo(_buildingData);
                outlineColor.a = 1;
                outline.effectColor = outlineColor;
            }
            else
            {
                outlineColor.a = 0f;
                outline.effectColor = outlineColor;
            }
        }
    }
}
