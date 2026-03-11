using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void ConfirmReturn() 
     {
        SceneManager.LoadScene("TitleScreen");
     }

    public void DenyReturn() 
    { 
      Panel.SetActive(false);
        Player.GetComponent<PlayerMovementScript>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        { 
            Panel.SetActive(true);
            Player.GetComponent<PlayerMovementScript>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }
}
