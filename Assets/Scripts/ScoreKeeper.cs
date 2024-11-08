using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;
    static ScoreKeeper scoreInstance;
    public ScoreKeeper GetInstance()
    {
        return scoreInstance;
    }

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        //int instanceCount = FindObjectsOfType(GetType()).Length;
        //if(instanceCount > 1)
        if(scoreInstance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            scoreInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }

    public void AddScore(int scoreAdded)
    {
        score += scoreAdded; 
        //Debug.Log(score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
