using UnityEngine;

namespace AssemblyDefinition.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    // создаем объект по пути из AssetPath
    public GameObject Instantiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }
  }
}