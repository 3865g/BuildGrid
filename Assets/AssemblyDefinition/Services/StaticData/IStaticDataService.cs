using System.Collections.Generic;
using AssemblyDefinition.StaticData;

namespace AssemblyDefinition.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    BuildingStaticData ForBuildings(int buildingId);
    IEnumerable<int> GetAllBuildingIds();
  }
}