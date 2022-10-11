using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
   public delegate void StartTouchEvent(Vector2 position, float time);
   public event StartTouchEvent OnStartTouch;

   public delegate void EndTouchEvent(Vector2 position, float time);
   public event EndTouchEvent OnEndTouch;


   private TouchCountrol touchCountrols;

   private void Awake() {
    touchCountrols= new TouchCountrol();
   }
   private void OnEnable() {
    touchCountrols.Enable();
   }
   private void OnDisable() {
    touchCountrols.Disable();
   }
    private void Start() {
        touchCountrols.Touch.TouchPress.started += ctx => StartTouch(ctx);       
       touchCountrols.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }
    private void StartTouch(InputAction.CallbackContext context){
      Debug.Log("Touch startes " +touchCountrols.Touch.TouchPosition.ReadValue<Vector2>());
      if(OnStartTouch != null) OnStartTouch(touchCountrols.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }
    private void EndTouch(InputAction.CallbackContext context){
      Debug.Log("Touch End");
       if(OnEndTouch != null) OnEndTouch(touchCountrols.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    }

}
