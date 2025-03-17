#if UNITY_EDITOR
using AssemblyDefinition.StaticData;
using UnityEditor;
using UnityEngine;

namespace AssemblyDefinition.Editor
{
    [CustomEditor(typeof(BuildingStaticData))]
    public class BuildingStaticDataEditor : UnityEditor.Editor
    {
        
        // Если ID не задан, генерируем его и сохраняем
        public override void OnInspectorGUI()
        {
            var buildingData = (BuildingStaticData)target;

            
            if (buildingData.id == 0)
            {
                buildingData.id = GetNextID();
                EditorUtility.SetDirty(buildingData);
            }

            // Отображаем остальные поля
            DrawDefaultInspector();
        }

        private int GetNextID()
        {
            // Находим все объекты BuildingStaticData
            var allBuildings = Resources.FindObjectsOfTypeAll<BuildingStaticData>();

            // Находим максимальный ID
            int maxID = 0;
            foreach (var building in allBuildings)
            {
                if (building.id > maxID)
                {
                    maxID = building.id;
                }
            }

            // Возвращаем следующий ID
            return maxID + 1;
        }
    }
}
#endif