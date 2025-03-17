using System.Collections.Generic;
using AssemblyDefinition.Services.StaticData;
using AssemblyDefinition.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyDefinition.UI
{
    public class BuildingsUi : MonoBehaviour
    {

        public GameObject buildingSelectButton;
        public Transform buttonsParent;
        public ToggleGroup toggleGroup;
     
        private IStaticDataService _staticDataService;
        private List<BuildingStaticData> _buildings;
        
        //инициализируем данные
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            FillList();
        }

        //Заполняем из статик дат
        private void FillList()
        {

            if (_buildings == null)
            {
                _buildings = new List<BuildingStaticData>();
            }
            else
            {
                _buildings.Clear();
            }
            
            foreach (int buildingId in _staticDataService.GetAllBuildingIds())
            {
                BuildingStaticData buildingData = _staticDataService.ForBuildings(buildingId);
                if (buildingData != null)
                {
                    SelectBuildingButton buildingButton = Instantiate(buildingSelectButton, buttonsParent).GetComponent<SelectBuildingButton>();
                    buildingButton.Construct(buildingData);
                    buildingButton.GetComponent<Toggle>().group = toggleGroup;
                    
                }
            }
        }
    }
}
