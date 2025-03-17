using AssemblyDefinition.Infrastructure.AssetManagement;
using AssemblyDefinition.Infrastructure.Factory;
using AssemblyDefinition.Services;
using AssemblyDefinition.Services.Input;
using AssemblyDefinition.Services.SaveLoad;
using AssemblyDefinition.Services.StaticData;

namespace AssemblyDefinition.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    // инициализируем данные
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;

      RegisterServices();
    }

    public void Enter()
    {
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    // регистрируем сервисы
    private void RegisterServices()
    {
      RegisterStaticDataService();
      _services.RegisterSingle<IInputService>(new InputService());
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),
        _services.Single<IStaticDataService>(),
        _services.Single<IInputService>(),
        _services.Single<ISaveLoadService>()));
    }

    private void RegisterStaticDataService()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.Load();
      _services.RegisterSingle(staticData);
    }

    // переходим в следующий стейт после регистрации сервисов
    private void EnterLoadLevel()
    {
      _stateMachine.Enter<LoadLevelState, string>("Level01");
    }
  }
}