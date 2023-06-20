using UnityEngine;

public class PersonController : MonoBehaviour
{
    public Vector2 moveDirection;
    [SerializeField] private float maxSpeed;
    private bool isJumping;
    private float turningDirection;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (moveDirection.y >= 0) turningDirection = moveDirection.x;
        else
        {
            if (moveDirection.x > 0) turningDirection = 2 - moveDirection.x;
            else turningDirection = -2 - moveDirection.x;
        }
        currentSpeed = Vector2.Distance(moveDirection, Vector2.zero) * maxSpeed;
    }

    private void FixedUpdate()
    {
        if (currentSpeed > 0)
        {
            animator.SetBool("isRunning", true);
            rb.MovePosition(rb.transform.position + transform.forward * currentSpeed * Time.deltaTime);
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(0, turningDirection * 60, 0), Time.deltaTime));
        }
        else animator.SetBool("isRunning", false);
    }

    public void JumpStarting()
    {
        if (!isJumping)
        {
            isJumping = true;
            animator.SetTrigger("jump");
        }
    }

    public void JumpEnding()
    {
        isJumping = false;
    }

    public void ShootStarting()
    {
        animator.SetBool("isShooting", true);
    }

    public void ShootEnding()
    {
        animator.SetBool("isShooting", false);
    }
}
