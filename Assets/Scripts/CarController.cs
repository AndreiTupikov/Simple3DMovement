using UnityEngine;

public class CarController : MonoBehaviour
{
    public int turningDirection;
    public int movementDirection;
    [SerializeField] private float maxSpeed = 4;
    [SerializeField] private float accelerationTime = 0;
    [SerializeField] private float maxWheelRotation = 40f;
    [SerializeField] Transform[] wheels;
    private Rigidbody rb;
    private float currentAccelerationTime = 0;
    private float currentVelocity = 0;
    private float totalRotation = 0;
    private float addedRotation = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        totalRotation = turningDirection * maxWheelRotation;

        for (int i = 0; i < wheels.Length; i++)
        {
            Quaternion targetWheelRotation = Quaternion.Euler(addedRotation, (wheels[i].localPosition.z > 0) ? totalRotation : 0, 0);
            wheels[i].localRotation = Quaternion.Lerp(wheels[i].localRotation, targetWheelRotation, Time.deltaTime * 20f);
        }

        if (movementDirection != 0)
        {
            currentAccelerationTime += (movementDirection > 0) ? Time.deltaTime : -Time.deltaTime;
        }
        else
        {
            currentAccelerationTime = Mathf.MoveTowards(currentAccelerationTime, 0, Time.deltaTime);
        }

        currentAccelerationTime = Mathf.Clamp(currentAccelerationTime, -accelerationTime, accelerationTime);

        addedRotation += currentAccelerationTime;
    }

    private void FixedUpdate()
    {
        currentVelocity = (currentAccelerationTime * maxSpeed) * Time.deltaTime;
        rb.MovePosition(rb.transform.position + transform.forward * currentVelocity);
        if (currentVelocity != 0) rb.MoveRotation(Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(0, ((totalRotation * 30) * currentAccelerationTime) * Time.deltaTime, 0), Time.deltaTime));
    }
}
