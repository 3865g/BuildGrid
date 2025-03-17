using System;
using AssemblyDefinition.StaticData;
using UnityEngine;

namespace AssemblyDefinition.Data
{
    [Serializable]
    public class BuildingData
    {
        public string buildingId; 
        public Vector3 position;
        public BuildingStaticData buildingData;
    }
}