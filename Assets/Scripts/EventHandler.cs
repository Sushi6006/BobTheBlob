using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour {
    
    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
    
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public static void EndGame() {
        SceneManager.LoadScene(2);
    }

    public static void dieBlob() {
        SceneManager.LoadScene(1);
    }

}

