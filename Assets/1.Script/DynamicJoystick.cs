using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/*Joystick 클래스는 조이스틱의 기본 기능을 처리하며, DynamicJoystick은 그것을 상속하여 더 동적인 동작을 도입합니다.

여기 주요 부분의 분석입니다:

IPointerDownHandler, IDragHandler, IPointerUpHandler는 터치 이벤트를 처리하기 위해 구현되었습니다.

HandleRange 및 DeadZone과 같은 속성이 조이스틱의 범위와 데드 존을 정의합니다.




OnDrag()은 드래그 이벤트 데이터를 기반으로 입력을 계산합니다.



DynamicJoystick 클래스에서는 드래그가 특정 임계값(MoveThreshold)을 초과할 때 조이스틱이 동적으로 움직이도록 일부 메서드를 오버라이드합니다.

또한 1.5초 이상 눌려 있을 때 조이스틱 핸들 색상을 빨간색으로 변경하는 코드(fPressTime 및 bGstate)도 있습니다. 이것은 충전된 샷 같은 특별한 게임 기능을 위한 것일 수 있습니다.

전반적으로 코드는 잘 구조화되어 있지만 꽤 긴 편입니다. 코드를 깔끔하게 유지하고 주석을 달아 유지보수성을 높이는 것이 좋습니다.

더 알고 싶은 구체적인 사항이 있나요?
*/public class DynamicJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;

    protected override void Start()
        //Start() 메서드에서는 초기 설정이 구성되고 조이스틱이 캔버스 내에 있는지 확인하는 검사가 수행됩니다.

    {
        MoveThreshold = moveThreshold;
        base.Start();
     //  background.gameObject.SetActive(false); //
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);//
       // background.gameObject.SetActive(true);//
        base.OnPointerDown(eventData);
    }
    // OnPointerDown() 및 OnPointerUp() 메서드는 각각 터치 이벤트의 시작과 끝을 처리합니다.
    public override void OnPointerUp(PointerEventData eventData)
    {
      //  background.gameObject.SetActive(false);//
        base.OnPointerUp(eventData);
    }


    //HandleInput()은 조이스틱의 움직임의 크기와 방향을 처리합니다.
    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {

        // Removing the code that moves the joystick's background
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }


    /*
    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        // Removing the code that moves the joystick's background
        // if (magnitude > moveThreshold)
        // {
        //     Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
        //     background.anchoredPosition += difference;
        // }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
    */



}
