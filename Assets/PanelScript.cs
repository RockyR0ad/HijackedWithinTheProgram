using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public string Scene;




    public void ConfirmWorld1()
    {
        SceneManager.LoadScene(Scene);
    }
    

    public void Cancel()
    {
        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
