using System.Collections.Generic;
using System.Linq;
using AssemblyDefinition.StaticData;
using UnityEngine;

namespace AssemblyDefinition.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string BuildingsDataPath = "StaticData/Buildings";


        private Dictionary<int, BuildingStaticData> _buildings;


        // Загружаем все здания из статик даты
        public void Load()
        {
            _buildings = Resources
                .LoadAll<BuildingStaticData>(BuildingsDataPath)
                .ToDictionary(x => x.id, x => x);
        }

        //получаем конкретное здание по ключу
        public BuildingStaticData ForBuildings(int buildingId)
        {
            return _buildings.TryGetValue(buildingId, out BuildingStaticData staticData) ? staticData : null;
        }
        //получаем список всех зданий
        public IEnumerable<int> GetAllBuildingIds()
        {
            return _buildings.Keys;
        }
    }
}