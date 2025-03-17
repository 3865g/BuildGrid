using System.Collections.Generic;
using AssemblyDefinition.Data;

namespace AssemblyDefinition.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SaveBuildings(List<BuildingData> buildings);
    void RemoveBuildingById(string buildingId);
    PlayerProgress LoadBuildings();
    
  }
}