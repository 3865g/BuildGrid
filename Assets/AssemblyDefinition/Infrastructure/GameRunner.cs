using UnityEngine;

namespace AssemblyDefinition.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    public GameBootstrapper bootstrapperPrefab;
    
    // ищем бутсраепер на сцене если его нет создаем новый
    private void Awake()
    {
      var bootstrapper = FindObjectOfType<GameBootstrapper>();
      
      if(bootstrapper) return;

      Instantiate(bootstrapperPrefab);
    }
  }
}