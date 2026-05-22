using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelScript : InteractableObject
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
    private void OnCollisionEnter(Collision collision)
    {
         
    }

    public override void Interact()
    {
        
        
            SceneManager.LoadScene(Scene);
        
    }
}
