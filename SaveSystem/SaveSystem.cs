using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

//Simple RPG: Save System script
//Made by: Conrad Buys
//References:
//https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934
//https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity

public static class SaveSystem
{

    public static void SaveGame(Save mySave)
    {
        //Debug.Log("Save data: gold " + mySave.currentGold + ", xp " + mySave.currentExp + ", level " + mySave.playerLevel + ".");
        BinaryFormatter bf = new BinaryFormatter(); //Binary Formatter does serialization work
        FileStream file = File.Create(Application.persistentDataPath + "/mysave.save"); //Creating a file in our directory. This contains all the data.
        bf.Serialize(file, mySave);
        file.Close();
    }

    public static Save LoadGame(Save mySave)
    {
        if (File.Exists(Application.persistentDataPath + "/mysave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/mysave.save", FileMode.Open);
            mySave = (Save)bf.Deserialize(file);
            file.Close();
        }
        return mySave;
    }

    public static void ClearData()
    {
        if (File.Exists(Application.persistentDataPath + "/mysave.save"))
        {
            File.Delete(Application.persistentDataPath + "/mysave.save");
        }
    }
}


//what things to save?
//level we're in, player level
//current HP, current skills
//current PP, current inventory
//player location in level, enemy/ies location in level

//start at the inn! screw level we're in, location, & enemies

[System.Serializable]
public class Save
{
    //we could just have a game class that has all this info? save would just be a reference to all the values in the script

    //public int playerLevel;
    //public int currentExp;
    //public int currentHP;
    //public int currentPP;
    //public int currentGold;
    //public List<string> mySkills = new List<string>(); //are we already making this? can i just import another list/etc.
    //list of our skills & their current values
    //how are we doing saving the inventory? just import our current inventory class?


    public float maxHP;
    public float curHP;
    public int playerLevel;
    public float exp;
    public int money;
    public int strength;
    public int defense;
    public int speed;
    public float nextXPLevel;
    public Vector3Serialze position;

    public int gameLevel;
    public int checkpoint;
}

[System.Serializable]
public class Vector3Serialze
{
    public float x, y, z;

    public Vector3Serialze(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
