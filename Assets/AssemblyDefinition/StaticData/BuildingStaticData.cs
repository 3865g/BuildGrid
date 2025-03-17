using UnityEngine;

namespace AssemblyDefinition.StaticData
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "Static Data/Building")]
    public class BuildingStaticData : ScriptableObject
    {
        
        public Sprite buildingIcon;
        public string buildingName;
        
        [TextArea(1, 4)]
        public string buildingDescription;
        

        public Vector2 buildingsize;

        public int id;
        
        public GameObject buildingPrefab;

    }
}
