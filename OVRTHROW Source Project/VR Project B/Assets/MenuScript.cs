using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public void PlayGame()
    {
        GameObject mPlayer = GameObject.Find("Player").gameObject;
        Destroy(mPlayer);
        SceneManager.LoadScene("activeScene");
        
    }

    public void QuitGame()
    {
      Debug.Log("Game Quitted");
      Application.Quit();
    }

}
