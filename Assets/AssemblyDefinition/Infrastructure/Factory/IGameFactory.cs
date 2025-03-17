using AssemblyDefinition.Services;
using UnityEngine;

namespace AssemblyDefinition.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    GameObject CreatePlayer();
    GameObject CreateHud();
  }
}