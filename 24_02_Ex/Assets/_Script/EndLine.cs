using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLine : MonoBehaviour
{
    public int life = 20;
    public int Life
    {
        get { return life; }
    }
    public void OnDamage()
    {
        if(life > 1)
        {
            life--;
        }
        else
        {
            life = 0;
            EndGame();
        }
    }
    void EndGame()
    {
        SceneManager.LoadScene("Ending");
    }
    public int GetLife()
    {
        return life;
    }

}
