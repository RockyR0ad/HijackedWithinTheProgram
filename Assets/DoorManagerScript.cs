using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagerScript : MonoBehaviour
{
    public List<LevelScript> Doors;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (LevelScript level in Doors) 
        {
            if (ProgressionManagerScript.Levels[i] == true) 
            { 
                level.Activate();
            }
            else 
            { 
                level.Deactivate();
            }
            
            i++;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
