﻿using System.Collections;
using UnityEngine;

namespace AssemblyDefinition.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}