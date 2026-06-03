using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 6f;
    public float TurnSpeed = 12f;
    public Transform CamPivot;
    public LayerMask DoorMask;
    public float InteractDistance;
    public float JumpForce;
    public float DashSpeed;
    public float MaxSpeed;
    public LayerMask GroundMask;
    public Transform GroundCastPos;
    public Transform PlayerRespawnPoint;
    private Rigidbody rb;
    private Vector3 MoveDirection;
    private Vector3 InputDirection;
    private Vector3 TargetPosition;
    

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        { 
            AirDash();
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
        if(transform.position.y < -5f) 
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = PlayerRespawnPoint.position;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Z = Input.GetAxisRaw("Vertical");

        InputDirection = new Vector3(X, 0f, Z).normalized;

        if(InputDirection.magnitude < 0.1f) 
        {
            return;
        }

        if (rb.velocity.x > MaxSpeed)
        {
            X = 0;
        }
        if (-rb.velocity.x < -MaxSpeed)
        {
            X = 0;
        }
        if (rb.velocity.z > MaxSpeed)
        {
            Z = 0;
        }
        if (-rb.velocity.z < -MaxSpeed)
        {
            Z = 0;
        }
        Vector3 CamForward = CamPivot.forward;
        Vector3 CamRight = CamPivot.right;

        CamForward.y = 0f;
        CamRight.y = 0f;

        CamForward.Normalize();
        CamRight.Normalize();

        MoveDirection = CamForward * Z + CamRight * X;
        TargetPosition = MoveDirection * speed * Time.fixedDeltaTime;
        rb.AddForce(TargetPosition);

        Quaternion TargetRot = Quaternion.LookRotation(MoveDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, TargetRot, TurnSpeed * Time.fixedDeltaTime));
    }

    public void Jump() 
    { 
        if(InputDirection.magnitude < 0.1f)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
        else 
        { 
            rb.AddForce(transform.forward +  Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
    public bool IsGrounded() 
    { 
        if(Physics.Raycast(GroundCastPos.position, Vector3.down, out RaycastHit hit, .2f, GroundMask)) 
        { 
            return true;
        }
        else 
        { 
           return false;
        }
    }

    void AirDash() 
    { 
      rb.AddForce(transform.forward * DashSpeed, ForceMode.Impulse);
    }
    void Interact()
    {
        Vector3[] directions = 
        {
            transform.forward,
            transform.forward + transform.right,
            transform.right,
            transform.forward - transform.right,
            -transform.forward,
            -(transform.forward + transform.right),
            -transform.right,
            -(transform.forward - transform.right),
                                  
        };
        InteractableObject ClosestDoor = null;
        foreach (Vector3 dir in directions) 
        { 
          Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, InteractDistance, DoorMask)) 
            { 
              InteractableObject CurrentDoor = hit.collider.GetComponent<InteractableObject>();
                if (ClosestDoor == null)
                {
                    ClosestDoor = CurrentDoor;
                }
                else if (CurrentDoor != null && ClosestDoor != null) 
                { 
                    if(Vector3.Distance(transform.position, CurrentDoor.transform.position) < Vector3.Distance(transform.position, ClosestDoor.transform.position)) 
                    {
                        ClosestDoor = CurrentDoor;
                    }
                }

            }
            Debug.DrawRay(transform.position, dir * InteractDistance, Color.red, 1f);

        }
        Debug.Log(ClosestDoor);
        if (ClosestDoor != null) 
        { 
            ClosestDoor.Interact();
        }
    }

    void SignInteract()
    {
        Vector3[] directions =
        {
            transform.forward,
            transform.forward + transform.right,
            transform.right,
            transform.forward - transform.right,
            -transform.forward,
            -(transform.forward + transform.right),
            -transform.right,
            -(transform.forward - transform.right),

        };
        InteractableObject ClosestSign = null;
        foreach (Vector3 dir in directions)
        {
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, InteractDistance, DoorMask))
            {
                InteractableObject CurrentSign = hit.collider.GetComponent<InteractableObject>();
                if (ClosestSign == null)
                {
                    ClosestSign = CurrentSign;
                }
                else if (CurrentSign != null && ClosestSign != null)
                {
                    if (Vector3.Distance(transform.position, CurrentSign.transform.position) < Vector3.Distance(transform.position, ClosestSign.transform.position))
                    {
                        ClosestSign = CurrentSign;
                    }
                }

            }
            Debug.DrawRay(transform.position, dir * InteractDistance, Color.red, 1f);

        }
        Debug.Log(ClosestSign);
        if (ClosestSign != null)
        {
            ClosestSign.SignInteract();
        }
    }
}
