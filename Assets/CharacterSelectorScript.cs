using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelectorScript : MonoBehaviour
{
    public GameObject[] characters;
    public int SelectedCharacters;
    public void NextCharacter() 
    {
        characters[SelectedCharacters].SetActive(false);
        SelectedCharacters = (SelectedCharacters + 1) % characters.Length;
        characters[SelectedCharacters].SetActive(true);
    }
    public void PreviousCharacter() 
    {
        characters[SelectedCharacters].SetActive(false);
        SelectedCharacters--;
        if (SelectedCharacters < 0) 
        {
            SelectedCharacters += characters.Length;
        }
        characters[SelectedCharacters].SetActive(true);
    }
    public void StartGame() 
    { 
        PlayerPrefs.SetInt("SelectedCharacter", SelectedCharacters);
        SceneManager.LoadScene("LevelSelectScene");
    }
}
