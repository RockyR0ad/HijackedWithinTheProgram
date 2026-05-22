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

        if (Input.GetKeyDown(KeyCode.Space))
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
        float Z = Input.GetAxisRaw("Vertical");

        InputDirection = new Vector3(X, 0f, Z).normalized;

        if(InputDirection.magnitude < 0.1f) 
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
        TargetPosition = rb.position + MoveDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(TargetPosition);

        Quaternion TargetRot = Quaternion.LookRotation(MoveDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, TargetRot, TurnSpeed * Time.fixedDeltaTime));
    }

    void Jump() 
    { 
    
    }

    void AirDash() 
    { 
    
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
}
