using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;

    void Start()
    {
        Singleton();
    }

    private void Singleton()
    {
        int gameSessions = FindObjectsOfType<GameSession>().Length;
        if (gameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int Getscore()
    {
        return score;
    }

    public void Addscore(int scoreValue)
    {
        score += scoreValue;
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
