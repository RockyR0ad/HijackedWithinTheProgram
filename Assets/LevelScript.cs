using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelScript : MonoBehaviour
{
    public string Scene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void DoorInteraction() 
    { 
     SceneManager.LoadScene(Scene); 
    }
    public void Activate() 
    {
        int ActiveLayer = LayerMask.NameToLayer("Door");
        gameObject.layer = ActiveLayer;
    }
    public void Deactivate() 
    {
        int DeactiveLayer = LayerMask.NameToLayer("Default");
        gameObject.layer = DeactiveLayer;
    }
}
