using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class AssignScores : MonoBehaviour
{
    LevelData myLevelDataLoaded;
    Text highScoreText;
    public Text curScoreText;
    Text endOfLevelText;

    int highScoreint;

    public int curScore;

    private void Start()
    {
        highScoreText = GameObject.Find("High Score Text").GetComponent<Text>();
        curScoreText = GameObject.Find("Current Score Text").GetComponent<Text>();
        myLevelDataLoaded = ScoreData.LoadHighScores(SceneManager.GetActiveScene().name);

        LoadLevelHighScore();
    }

    void LoadLevelHighScore()
    {
        highScoreText.text = myLevelDataLoaded.highScore.ToString();
    }
    

    public void saveHighScore()
    {
        if(GameObject.Find("Current Score Numbers") != null)
        {
            endOfLevelText = GameObject.Find("Current Score Numbers").GetComponent<Text>();
        }
        curScore = int.Parse(endOfLevelText.text);
        print("Save Called");
        if(curScore > myLevelDataLoaded.highScore)
        {
            print("New High Score");
            ScoreData.SaveHighScore(SceneManager.GetActiveScene().name, curScore);
            highScoreText.text = "High Score: " + myLevelDataLoaded.highScore.ToString();
        }
    }
}
