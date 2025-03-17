using UnityEngine;

namespace AssemblyDefinition.Services.Input
{
  public class InputService : IInputService
  {
    
    private PlayerMouseActions _inputActions;

    public InputService()
    {
      _inputActions = new PlayerMouseActions();
      _inputActions.Enable();
    }

    //получаем позицию курсора
    public Vector2 GetCursorPosition()
    {
      return _inputActions.Mouse.Point.ReadValue<Vector2>();
    }

    //получаем левый клик
    public bool IsLeftClickPerformed()
    {
      return _inputActions.Mouse.LeftClick.triggered;
    }

    //получаем правый клик
    public bool IsRightClickPerformed()
    {
      return _inputActions.Mouse.RightClick.triggered;
    }
  }
}