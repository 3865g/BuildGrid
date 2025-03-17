using AssemblyDefinition.Services;
using UnityEngine;

namespace AssemblyDefinition.Infrastructure.AssetManagement
{
  public interface IAssetProvider:IService
  {
    GameObject Instantiate(string path);
  }
}