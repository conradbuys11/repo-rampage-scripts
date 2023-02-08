using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class ScoreData : MonoBehaviour
{
    int scoreToSave;
    //*****************************************
    //Save and load game slot one
    //*****************************************
    public static void SaveGameOne(int[] scores)
    {       
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ScoreSave1.dat");

        LevelData myLevelData = new LevelData();
        myLevelData.levelScores = scores;
        bf.Serialize(file, myLevelData);
        file.Close();
    }
    public static LevelData LoadGame1()
    {
        LevelData myLevelData = new LevelData();
        if (File.Exists(Application.persistentDataPath + "/ScoreSave1.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ScoreSave1.dat", FileMode.Open);
            myLevelData = (LevelData)bf.Deserialize(file);
            file.Close();
        }
        return myLevelData;

    }
    public static void SaveHighScore(string levelName, int newHighScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/HighScoreData" + levelName +".Dat");

        LevelData myLevelData = new LevelData();
        myLevelData.highScore = newHighScore;
        bf.Serialize(file, myLevelData);
        file.Close();
    }
    public static LevelData LoadHighScores(string levelName)
    {
        LevelData myLevelData = new LevelData();
        if (File.Exists(Application.persistentDataPath + "/HighScoreData" + levelName + ".Dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/HighScoreData" + levelName + ".Dat", FileMode.Open);
            myLevelData = (LevelData)bf.Deserialize(file);
            file.Close();
        }
        return myLevelData;
    }

    //*****************************************
    //Save and load game slot Two
    //*****************************************
    public void SaveGameTwo(Save mySave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ScoreSave2.dat");
        bf.Serialize(file, mySave);
        file.Close();
    }
    public Save LoadGame2(Save mySave)
    {
        if (File.Exists(Application.persistentDataPath + "/ScoreSave2.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Score.dat", FileMode.Open);
            mySave = (Save)bf.Deserialize(file);
            file.Close();
        }
        return mySave;
    }

    //*****************************************
    //Save and load game slot Three
    //*****************************************
    public void SaveGameThree(Save mySave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ScoreSave3.dat");
        bf.Serialize(file, mySave);
        file.Close();
    }
    public Save LoadGame3(Save mySave)
    {
        if (File.Exists(Application.persistentDataPath + "/ScoreSave3.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Score.dat", FileMode.Open);
            mySave = (Save)bf.Deserialize(file);
            file.Close();
        }
        return mySave;
    }
}



[System.Serializable]
public class LevelData
{
    public string levelName;
    public int scoreLoad;
    public int highScoreLoad;
    public int highScore;
    public int[] levelScores;
    public int[] levelHighScores;
}
