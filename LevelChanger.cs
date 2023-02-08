using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void UnPause()
    {
        GameObject.Find("Pause").gameObject.SetActive(false);
        Time.timeScale = 1;
        FindObjectOfType<PlayerPushbox>().AllowInput(true);
    }

    public void OnOptions()
    {

    }

    public void LoadLevel(string levelToLoad)
    {
        SaveScore();
        SceneManager.LoadScene(levelToLoad);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadCheckpoint()
    {

    }

    void SaveScore()
    {
        if (FindObjectOfType<AssignScores>())
        {
            FindObjectOfType<AssignScores>().saveHighScore();
        }
    }
}
