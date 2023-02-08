using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EndOfLevelScoring : MonoBehaviour
{
    GP_Canvas refGP_Canvas;
    AssignScores refAssignScores;
    PlayerStatus refPlayerStatus;
    LevelData myLevelDataLoaded;
    
    [SerializeField] int curScore;
    [SerializeField] int highScore;
    [SerializeField] int playerHealth;
    [SerializeField] int playerLives;

    int healthBonusScore;
    int livesBonusScore;

    Text livesText;
    Text playerHealthText;
    Text curScoreText;
    Text highScoreText;

    int healthMultiplier;
    int livesMultiplier;

    public bool giveScore;


    private void Awake()
    {
        healthMultiplier = 100;
        livesMultiplier = 5000;
        refGP_Canvas = FindObjectOfType<GP_Canvas>();
        refAssignScores = FindObjectOfType<AssignScores>();
        refPlayerStatus = FindObjectOfType<PlayerStatus>();
        livesText = GameObject.Find("Bonus Lives Numbers").GetComponent<Text>();
        playerHealthText = GameObject.Find("Bonus Health Numbers").GetComponent<Text>();
        curScoreText = GameObject.Find("Current Score Numbers").GetComponent<Text>();
        highScoreText = GameObject.Find("High Score Numbers").GetComponent<Text>();
        myLevelDataLoaded = ScoreData.LoadHighScores(SceneManager.GetActiveScene().name);
        highScoreText.text = myLevelDataLoaded.highScore.ToString();
    }

    private void Update()
    {
        if (giveScore && healthBonusScore > 0)
        {
            if(healthBonusScore > 100)
            {
                curScore += healthBonusScore / 10;
                healthBonusScore -= healthBonusScore / 10;
            }
            else
            {
                healthBonusScore--;
                curScore++;
                healthBonusScore--;
                curScore++;
                healthBonusScore--;
                curScore++;
                healthBonusScore--;
                curScore++;
            }
            if(healthBonusScore < 0)
            {
                curScore += healthBonusScore;
                healthBonusScore = 0;
            }

            playerHealthText.text = healthBonusScore.ToString();
            curScoreText.text = curScore.ToString();
        }
        else if(giveScore && livesBonusScore > 0) 
        {
            if (livesBonusScore > 100)
            {
                curScore += livesBonusScore / 15;
                livesBonusScore -= livesBonusScore / 15;
            }
            else
            {
                livesBonusScore--;
                curScore++;
                livesBonusScore--;
                curScore++;
                livesBonusScore--;
                curScore++;
            }
            if(livesBonusScore < 0)
            {
                curScore += livesBonusScore;
                livesBonusScore = 0;
            }
            livesText.text = livesBonusScore.ToString();
            curScoreText.text = curScore.ToString();
        }
        else if(livesBonusScore == 0 && healthBonusScore == 0)
        {
            SaveScore();
        }
    }
    

    void SetNumbers()
    {
        playerLives = refPlayerStatus.livesCount;
        playerHealth = refPlayerStatus.health;
      //  curScore = refGP_Canvas.Score;
        highScore = myLevelDataLoaded.highScore;
        curScoreText.text = curScore.ToString();
        playerHealthText.text = refPlayerStatus.health.ToString();
        livesText.text = refPlayerStatus.livesCount.ToString();
    }

    void CalculateHealthBonus()
    {
        healthBonusScore = playerHealth * healthMultiplier;
        playerHealthText.text = healthBonusScore.ToString();
    }

    void CalculateLivesBonus()
    {
        livesBonusScore = playerLives * livesMultiplier;
        livesText.text = livesBonusScore.ToString();
    }

    void DrainScore()
    {
        giveScore = !giveScore;
    }

    void SaveScore()
    {
        if(curScore > myLevelDataLoaded.highScore)
        {
            highScoreText.text = curScore.ToString();
            if (FindObjectOfType<AssignScores>())
            {
                FindObjectOfType<AssignScores>().saveHighScore();
            }
        }
    }
}
