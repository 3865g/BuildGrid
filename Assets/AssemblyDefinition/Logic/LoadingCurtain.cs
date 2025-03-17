using System.Collections;
using UnityEngine;

namespace AssemblyDefinition.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup curtain;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    //показываем загрузочный экран
    public void Show()
    {
      gameObject.SetActive(true);
      curtain.alpha = 1;
    }
    //скрываем загрузочный экран
    public void Hide()
    {
      StartCoroutine(DoFadeIn());
    }

    private IEnumerator DoFadeIn()
    {
      while (curtain.alpha > 0)
      {
        curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
      
      gameObject.SetActive(false);
    }
  }
}