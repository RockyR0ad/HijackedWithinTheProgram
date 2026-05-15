using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StatuePortalScript : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Player;
    public string Scene;
    public string description;

    
    
    public void ConfirmWorld1() 
    {
        SceneManager.LoadScene(Scene);
    }
    public void ConfirmWorld2() 
    {
        SceneManager.LoadScene("World2");
    }

    public void ConfirmWorld3() 
    {
        SceneManager.LoadScene("World3");
    }

    public void Cancel() 
    {
        
        Player.GetComponent<PlayerMovementScript>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject Temporary = Instantiate(Panel);
            Temporary.GetComponent<PanelScript>().Scene = Scene;
            Temporary.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = description;
            Player.GetComponent<PlayerMovementScript>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
