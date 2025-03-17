using AssemblyDefinition.Services.Input;
using AssemblyDefinition.Services.SaveLoad;
using UnityEngine;

namespace AssemblyDefinition.Buildings
{
    public class BuildingRemover : MonoBehaviour
    {
        [SerializeField] private LayerMask buildingLayer; 
        
        private Camera _mainCamera;
        private IInputService _inputService;
        private ISaveLoadService _saveLoadService;
        private bool _canRemove;

        // инициализируем здания
        public void Construct(IInputService inputService, ISaveLoadService saveLoadService)
        {
            _inputService = inputService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        // включение флага что можем удалить здание
        public void StartRemoval()
        {
            _canRemove = true;
        }

        // отключение флага что можем удалить здание
        public void StopRemoval()
        {
            _canRemove = false;
        }

        private void Update()
        {
            if (_inputService != null && _inputService.IsLeftClickPerformed() && _canRemove)
            {
                TryRemoveBuilding();
            }
        }

        // пытаемся удалить здание, если успешно удаляем, то удаляем это здание из Json фала сохранения
        private void TryRemoveBuilding()
        {
            Vector2 cursorPosition = Input.mousePosition;
            Vector3 worldPosition =
                _mainCamera.ScreenToWorldPoint(new Vector3(cursorPosition.x,
                    cursorPosition.y,
                    _mainCamera.nearClipPlane));

            // Проверяем, было ли нажатие на здание
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, buildingLayer);
            if (hitCollider)
            {
                Building building = hitCollider.GetComponent<Building>();
                if (building)
                {
                    _saveLoadService.RemoveBuildingById(building.Id);
                    Destroy(building.gameObject);
                    Debug.Log("Здание удалено!");
                }
            }

            StopRemoval();
        }
    }
}