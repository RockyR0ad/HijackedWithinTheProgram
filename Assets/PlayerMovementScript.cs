using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
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
}
