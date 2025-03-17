using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssemblyDefinition.Infrastructure
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;

    // Инициализация
    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
    }

    // Логика закгрузки сцены
    public void Load(string name, Action onLoaded = null)
    {
      _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    }

    //Загружаем сцену, проверяя не идет ли уже загрузка этой сцены
    public IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        yield break;
      }
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (!waitNextScene.isDone)
        yield return null;
      
      onLoaded?.Invoke();
    }
  }
}