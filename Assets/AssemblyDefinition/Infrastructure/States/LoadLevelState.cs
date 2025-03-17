using AssemblyDefinition.Infrastructure.Factory;
using AssemblyDefinition.Logic;

namespace AssemblyDefinition.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;

      // инициализируем 
    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
    }

    // включаем загрузочный экран, загрузчика сцен
    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    //как уровень загрузился создаем "игрока" и hud
    private void OnLoaded()
    {
      InitGameWorld();
      _stateMachine.Enter<GameLoopState>();
    }

    private void InitGameWorld()
    {
      _gameFactory.CreatePlayer();
      _gameFactory.CreateHud();
    }
  }
}