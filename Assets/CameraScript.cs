using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Player;
    public float MouseSensitivity;
    public float XRot = 0f;
    public float YRot = 0f;
    public float MinY = -75f;
    public float MaxY = 75f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        MouseLook();
        transform.position = Player.position;
    }

    void MouseLook() 
    { 
        float MouseX = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;

        XRot -= MouseY;
        XRot = Mathf.Clamp(XRot, MinY, MaxY);

        YRot += MouseX;
        transform.localRotation = Quaternion.Euler(XRot, YRot, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ReturnTrigger")) 
        { 
          Cursor.lockState = CursorLockMode.None;
        }
    }
}
