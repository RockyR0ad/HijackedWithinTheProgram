using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectReturnScript : MonoBehaviour
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
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void DenyReturn()
    {

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
