using System.Collections.Generic;
using AssemblyDefinition.Data;
using AssemblyDefinition.Logic;
using AssemblyDefinition.Services.Input;
using AssemblyDefinition.Services.SaveLoad;
using AssemblyDefinition.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AssemblyDefinition.Buildings
{
    public class BuildingPlacer : MonoBehaviour
    {
        [SerializeField] private GridForPlace grid;

        [SerializeField] private LayerMask environmentLayer;
        [SerializeField] private LayerMask buildingLayer;

        private BuildingStaticData _buildingStaticData;
        private Building _currentBuilding;
        private Camera _mainCamera;
        private IInputService _inputService;
        private ISaveLoadService _saveLoadService;
        private List<BuildingData> _placedBuildings = new List<BuildingData>();

        //инициализируем сервисы
        public void Construct(IInputService inputService, ISaveLoadService saveLoadService)
        {
            _inputService = inputService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        // делаем загрузку при старте
        private void Start()
        {
            LoadBuildings();
        }

        // Создаем объект здания, инициализируем его, если здание уже было уничтожаем старое
        public void StartPlacingBuilding(BuildingStaticData buildingStaticData)
        {
            if (_currentBuilding)
            {
                Destroy(_currentBuilding.gameObject);
            }

            _buildingStaticData = buildingStaticData;

            _currentBuilding = Instantiate(_buildingStaticData.buildingPrefab).GetComponent<Building>();
            _currentBuilding.Construct(_buildingStaticData, null);
        }

       

        private void Update()
        {
            if (_currentBuilding)
            {
                bool canPlace = CanPlaceBuilding();
                UpdateBuildingColor(canPlace);
                FollowCursor();
                DetectClick();
                
            }
        }

        private void FixedUpdate()
        {
            if (_currentBuilding)
            {
                
            }
        }

        // проверям клик, можем ли строить, пришелся ли клик на UI, если всё ок строим здание
        private void DetectClick()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (_inputService != null && _inputService.IsLeftClickPerformed())
            {
                if (CanPlaceBuilding())
                {
                    PlaceBuilding();
                }
                else
                {
                    Debug.Log("Невозможно разместить здание!");
                }
            }

            if (_inputService != null && _inputService.IsRightClickPerformed())
            {
                StopPlacingBuilding();
            }
        }

        // логика следования за курсором
        private void FollowCursor()
        {
            if (_inputService != null && _mainCamera && grid)
            {
                Vector2 cursorPosition = _inputService.GetCursorPosition();
                Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(cursorPosition.x, cursorPosition.y,
                    _mainCamera.nearClipPlane));
                Vector3 gridPosition = grid.GetNearestPointOnGrid(worldPosition);

                if (_currentBuilding)
                {
                    _currentBuilding.transform.position = gridPosition;
                }
            }
        }

        // логика установки здания, если здание построено, сохраняем его
        public void PlaceBuilding()
        {
            if (_currentBuilding && CanPlaceBuilding())
            {
                BuildingData newBuilding = new BuildingData
                {
                    buildingId = _currentBuilding.Id,
                    position = _currentBuilding.transform.position,
                    buildingData = _buildingStaticData
                };

                Color color = Color.white;
                color.a = 1f;
                _currentBuilding.UpdateColor(color);

                _placedBuildings.Add(newBuilding);
                _saveLoadService.SaveBuildings(_placedBuildings);

                _currentBuilding = null;
                Debug.Log("Здание размещено и сохранено!");
            }
        }
        
        //очищаем переменную если здание установлено или постройка отменена
        public void StopPlacingBuilding()
        {
            if (_currentBuilding)
            {
                Destroy(_currentBuilding.gameObject);
                _currentBuilding = null;
            }
        }

        // проверяем можем ли построить здание в этом месте
        private bool CanPlaceBuilding()
        {
            if (!_currentBuilding) return false;
            Color color = Color.white;

            Collider2D buildingCollider = _currentBuilding.GetComponent<Collider2D>();
            if (!buildingCollider) return false;

            Collider2D[] environmentColliders = Physics2D.OverlapBoxAll(
                buildingCollider.bounds.center,
                buildingCollider.bounds.size,
                0,
                environmentLayer
            );

            if (environmentColliders.Length > 0)
            {
                //Debug.Log("Здание пересекается с окружением!");
                return false;
            }

            Collider2D[] buildingColliders = Physics2D.OverlapBoxAll(
                buildingCollider.bounds.center,
                buildingCollider.bounds.size,
                0,
                buildingLayer
            );

            if (buildingColliders.Length > 1)
            {
                //Debug.Log("Здание пересекается с другим зданием!");
                _currentBuilding.UpdateColor(color);
                return false;
            }

            return true;
        }

        // загрузка зданий из сохранения
        private void LoadBuildings()
        {
            PlayerProgress data = _saveLoadService.LoadBuildings();
            _placedBuildings = data.buildings;

            foreach (var buildingData in _placedBuildings)
            {
                RestoreBuilding(buildingData);
            }
        }

        // инициализация зданий после загрузки
        private void RestoreBuilding(BuildingData buildingData)
        {
            BuildingStaticData buildingStaticData = buildingData.buildingData;

            if (buildingStaticData)
            {
                GameObject building = Instantiate(buildingStaticData.buildingPrefab);

                building.GetComponent<Building>().Construct(buildingStaticData, buildingData.buildingId);
                building.transform.position = buildingData.position;
            }
        }

        //Изменяем цвет префабу в зависимости от того пересекается ли он с чем то
        private void UpdateBuildingColor(bool canPlace)
        {
            if (canPlace)
            {
                Color color = Color.green;
                color.a = .7f;
                _currentBuilding.UpdateColor(color);
            }
            else
            {
                Color color = Color.red;
                color.a = .7f;
                _currentBuilding.UpdateColor(color);
            }
        }
    }
}