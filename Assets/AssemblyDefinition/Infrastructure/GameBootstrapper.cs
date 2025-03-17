using AssemblyDefinition.Infrastructure.States;
using AssemblyDefinition.Logic;
using UnityEngine;

namespace AssemblyDefinition.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public LoadingCurtain curtainPrefab;
    
    private Game _game;

      //при создании переводим стетйт машину в бутстрап стейт
    private void Awake()
    {
      _game = new Game(this, Instantiate(curtainPrefab));
      _game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}