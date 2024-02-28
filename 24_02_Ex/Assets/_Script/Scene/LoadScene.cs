using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int sceneNum = 0;
    public void GoScene()
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
