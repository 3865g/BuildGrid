using System.Collections.Generic;
using System.IO;
using AssemblyDefinition.Data;
using UnityEngine;

namespace AssemblyDefinition.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private string _savePath = Application.dataPath + "/savefile.json";

      //Сохраням здания в Json
    public void SaveBuildings(List<BuildingData> buildings)
    {
      PlayerProgress data = new PlayerProgress();
      data.buildings = buildings;

      string json = JsonUtility.ToJson(data);
      File.WriteAllText(_savePath, json);
    }
    
    //загружаем здания из Json 
    public PlayerProgress LoadBuildings()
    {
      if (File.Exists(_savePath))
      {
        string json = File.ReadAllText(_savePath);
        Debug.Log("Загрузка данных");
        return JsonUtility.FromJson<PlayerProgress>(json);
      }
      else
      {
        return new PlayerProgress();
      }
    }
    
    
    //Удаляем из Json здания которые удаляем на сцене
    public void RemoveBuildingById(string buildingId)
    {
      PlayerProgress data = LoadBuildings();
      BuildingData buildingToRemove = data.buildings.Find(b => b.buildingId == buildingId);

      if (buildingToRemove != null)
      {
        data.buildings.Remove(buildingToRemove);
        SaveBuildings(data.buildings);
        Debug.Log($"Здание с ID={buildingId} удалено из JSON.");
      }
      else
      {
        Debug.LogWarning($"Здание с ID={buildingId} не найдено в JSON.");
      }
    }
    
  }
}