using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        //Debug.Log(scoreText.text);
    }
    void Start()
    {
        scoreText.text = "Your score is: \n" + scoreKeeper.GetScore().ToString();
    }

    
}
