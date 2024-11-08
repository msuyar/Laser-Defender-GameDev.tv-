using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneDelay = 1f;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitandLoadScene(2,sceneDelay));
    }
    IEnumerator WaitandLoadScene(int sceneIndex, float sec)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        //Debug.Log("We are quiting");
        Application.Quit();
    }
}
