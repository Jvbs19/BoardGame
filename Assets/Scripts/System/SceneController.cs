using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadSceneByNumber(int num)
    {
        SceneManager.LoadScene(num);
    }
    public void ExitGame() 
    {
        Application.Quit();
    }
}
