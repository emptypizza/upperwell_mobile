using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class VirtualJoystick_m : MonoBehaviour
{

    public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    public float handleRange = 1.0f;

    private Vector2 inputVector;


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out value))
        {
            value.y = (value.y / joystickBackground.sizeDelta.y);
           // inputVector = new Vector2(value.x * 2 - 1, value.y * 2 - 1);
            inputVector = new Vector2(0, value.y * 2 - 1);
            inputVector = inputVector.magnitude > 1 ? inputVector.normalized : inputVector;

            // 세로 이동만 허용 하도록 핸들의 Y의 위치만 변경
            joystickHandle.anchoredPosition = new Vector2(0, inputVector.y * (joystickBackground.sizeDelta.y * handleRange));

        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public float Vertical { get { return inputVector.y; } }


// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
