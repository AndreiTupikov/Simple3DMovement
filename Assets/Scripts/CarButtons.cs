using UnityEngine;

public class CarButtons : MonoBehaviour
{
    private bool leftPressed;
    private bool rightPressed;
    private bool gasPressed;
    private bool breakPressed;
    private CarController carController;

    private void Start()
    {
        carController = GetComponent<CarController>();
    }

    private void Update()
    {
        if (leftPressed ^ rightPressed) carController.turningDirection = leftPressed ? -1 : 1;
        else carController.turningDirection = 0;
        if (gasPressed ^ breakPressed) carController.movementDirection = gasPressed ? 1 : -1;
        else carController.movementDirection = 0;
    }

    public void ButtonControl(int button)
    {
        bool pressed = button % 2 == 1;
        switch (button / 10)
        {
            case 1: leftPressed = pressed; break;
            case 2: rightPressed = pressed; break;
            case 3: gasPressed = pressed; break;
            case 4: breakPressed = pressed; break;
        }
    }
}