using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProgressionManagerScript
{
    public static bool Level1 = true;
    public static bool Level2;
    public static bool Level3;
    public static bool Level4;
    public static bool Boss1;

    public static List<bool> Levels = new List<bool>{Level1, Level2, Level3, Level4}; 
}
