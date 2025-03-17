using AssemblyDefinition.Buildings;
using AssemblyDefinition.Infrastructure.AssetManagement;
using AssemblyDefinition.Services.Input;
using AssemblyDefinition.Services.SaveLoad;
using AssemblyDefinition.Services.StaticData;
using AssemblyDefinition.UI;
using UnityEngine;

namespace AssemblyDefinition.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    private readonly IInputService _inputService;
    private readonly ISaveLoadService _saveLoadService;
    
    private GameObject _playerGameObject;

    //инициализация
    public GameFactory(IAssetProvider assets, IStaticDataService staticData,  IInputService inputService, ISaveLoadService saveLoadService)
    {
      _assets = assets;
      _staticData = staticData;
      _inputService = inputService;
      _saveLoadService = saveLoadService;
    }

    // создание объекта который расставляет  и удаляет объекты с сцены
    public GameObject CreatePlayer()
    {
      _playerGameObject = InstantiateRegistered(AssetPath.HeroPath);
      _playerGameObject.GetComponent<BuildingPlacer>()?.Construct(_inputService, _saveLoadService);
      _playerGameObject.GetComponent<BuildingRemover>()?.Construct(_inputService, _saveLoadService);
      return _playerGameObject;
    }

    // создание интерфейса с кнопками зданий
    public GameObject CreateHud()
    {
      GameObject hud = InstantiateRegistered(AssetPath.HudPath);
      hud.GetComponent<BuildingsUi>().Construct(_staticData);
      hud.GetComponentInChildren<PlaceRemoveButton>().
        Construct(_playerGameObject.GetComponent<BuildingPlacer>(),
          _playerGameObject.GetComponent<BuildingRemover>());
      
      return hud;
    }

    //логика спавна объектов
    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath);

      return gameObject;
    }
  }
}