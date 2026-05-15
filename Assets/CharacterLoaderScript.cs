using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterLoaderScript : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform SpawnPoint;
    public TMP_Text label;
    void Start()
    {
        int SelectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        GameObject prefab = characterPrefabs[SelectedCharacter];
        GameObject clone = Instantiate(prefab, SpawnPoint.position, Quaternion.identity);
        label.text = prefab.name;
    }

    
   
}
