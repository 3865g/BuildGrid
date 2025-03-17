using UnityEngine;

namespace AssemblyDefinition.Services.Input
{
  public interface IInputService : IService
  {
    Vector2 GetCursorPosition();
    bool IsLeftClickPerformed();
    bool IsRightClickPerformed();
  }
}