using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 6f;
    public float TurnSpeed = 12f;
    public Transform CamPivot;

    private Rigidbody rb;
    private Vector3 MoveDirection;
    private Vector3 InputDirection;
    private Vector3 TargetPosition;
    public float JumpForce;
    public float SpeedDash;
    public LayerMask GroundMask;
    public LayerMask DoorMask;
    public Transform GroundCastPos;
    public float MaxSpeed;
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
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float X = Input.GetAxisRaw("Horizontal");
        if (rb.velocity.x > MaxSpeed) 
        {
            X = 0;
        }
        float Z = Input.GetAxisRaw("Vertical");
        if (rb.velocity.z > MaxSpeed)
        {
            Z = 0;
        }
        InputDirection = new Vector3(X, 0f, Z).normalized;

        if(InputDirection.magnitude < 0.1f )
        {
            return;
            
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
    public void AirDash() 
    {
        
            rb.AddForce(transform.forward * SpeedDash, ForceMode.Impulse);
            Vector3[] directions =
            {
              transform.forward,
              (transform.forward + transform.right).normalized,
              transform.right,
              (-transform.forward + transform.right).normalized,
              -transform.forward,
              (-transform.forward - transform.right).normalized,
              -transform.right,
              (transform.forward - transform.right).normalized
            };
        
    }
    public void Jump() 
    {
        if (InputDirection.magnitude < 0.1f)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

        }
        else 
        { 
            rb.AddForce(transform.forward + Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
    
    public bool IsGrounded() 
    {
        if (Physics.Raycast(GroundCastPos.position, Vector3.down, out RaycastHit hit, .4f, GroundMask))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public void Interact() 
    {
        Vector3[] directions =
        {
         transform.forward,
         (transform.forward + transform.right).normalized,
         transform.right,
         (-transform.forward + transform.right).normalized,
            -transform.forward,
         (-transform.forward - transform.right).normalized,
         -transform.right,
         (transform.forward - transform.right).normalized
        };
        LevelScript ClosestDoor = null;
        foreach (Vector3 dir in directions) 
        {
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2f, DoorMask)) 
            { 
                LevelScript CurrentDoor = hit.collider.GetComponent<LevelScript>();
                if(ClosestDoor  == null) 
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

        }
        if (ClosestDoor != null) 
        {
            ClosestDoor.DoorInteraction();
        }

    }
    
}
