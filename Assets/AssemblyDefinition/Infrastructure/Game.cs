using AssemblyDefinition.Infrastructure.States;
using AssemblyDefinition.Logic;
using AssemblyDefinition.Services;

namespace AssemblyDefinition.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;

    // создаем стейт машину
    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
    }
  }
}