using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    public int life = 20;
    public int Life
    {
        get { return life; }
    }
    public void OnDamage()
    {
        if(life > 0)
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

    }
    public int GetLife()
    {
        return life;
    }

}
