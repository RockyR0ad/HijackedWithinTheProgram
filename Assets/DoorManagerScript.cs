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
            level.enabled = ProgressionManagerScript.Levels[i];
            i++;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
