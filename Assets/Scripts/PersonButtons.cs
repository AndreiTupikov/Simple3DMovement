using UnityEngine;

public class PersonButtons : MonoBehaviour
{
    [SerializeField] RectTransform joystick;
    [SerializeField] RectTransform manipulator;
    private PersonController controller;
    private Vector2 joystickPosition;
    private Vector2 direction;
    private float manipulatorCorrection;
    private int joystickFingerId = -1;

    private void Start()
    {
        controller = GetComponent<PersonController>();
        joystickPosition = joystick.position;
        manipulatorCorrection = (joystick.sizeDelta.x - manipulator.sizeDelta.x) * 0.9f / 2;
    }

    private void Update()
    {
        direction = (GetJoystickTouchPosition() - joystickPosition).normalized;
        manipulator.position = direction * manipulatorCorrection + joystickPosition;
        controller.moveDirection = direction;
    }

    private Vector2 GetJoystickTouchPosition()
    {
        Vector2 dir = joystickPosition;
#if UNITY_EDITOR
        if (joystickFingerId == 0)
        {
            dir.x = Input.mousePosition.x;
            dir.y = Input.mousePosition.y;
        }
#else
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == joystickFingerId)
            {
                dir.x = touch.position.x;
                dir.y = touch.position.y;
            }
        }
#endif
        return dir;
    }

    public void JoystickControl(bool isPressed)
    {
        if (isPressed)
        {
#if UNITY_EDITOR
            joystickFingerId = 0;
#else
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (joystickFingerId < 0) joystickFingerId = touch.fingerId;
                    else joystickFingerId = GetNearestTouch(joystickFingerId, touch.fingerId);
                }
            }
#endif
        }
        else joystickFingerId = -1;
    }

    public void Jump()
    {
        controller.JumpStarting();
    }

    public void Shoot(bool isPressed)
    {
        if (isPressed) controller.ShootStarting();
        else controller.ShootEnding();
    }

    private int GetNearestTouch(int a, int b)
    {
        float distanceA = 0;
        float distanceB = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == a) distanceA = Vector2.Distance(touch.position, joystickPosition);
            if (touch.fingerId == b) distanceB = Vector2.Distance(touch.position, joystickPosition);
        }
        return distanceB > distanceA ? a : b;
    }
}