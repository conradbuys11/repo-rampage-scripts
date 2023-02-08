using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public GameObject TutorialOption;

    public Animator transition;

    private bool change;
    public bool Level_Goal;
    public bool BossLevel;

    //private void Awake()
    //{
    //    ChasePlayer[] chase = FindObjectsOfType<ChasePlayer>();
    //    PickUp[] pickUp = FindObjectsOfType<PickUp>();

    //    for(int i = 0; i < pickUp.Length; i++)
    //    {
    //        if (!pickUp[i].LevelPickUp)
    //        {
    //            Destroy(pickUp[i]);
    //        }
    //    }

    //    for(int i = 0; i < chase.Length; i++)
    //    {
    //        Destroy(pickUp[i]);
    //    }
    //}

    public void BeforeChange()
    {
        change = true;
    }

    public void OptionOfTutorial()
    {
        TutorialOption.SetActive(true);
    }

    public void DeclineTutorial()
    {
        TutorialOption.SetActive(false);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevelFour()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadLevelFive()
    {
        SceneManager.LoadScene(6);
    }

    public void LoadLevelSix()
    {
        SceneManager.LoadScene(7);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ToQuit()
    {
        Application.Quit();

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }

    //Handles the delay for transition before switching scenes.
    IEnumerator LoadLevelOneDelay()
    {
        print("Load Level 1");
        yield return new WaitForSeconds(1.2f);
        print("Done waiting");
        
    }

    IEnumerator LoadLevelTwoDelay()
    {
        yield return new WaitForSeconds(1.2f);
        
    }

    IEnumerator LoadLevelThreeDelay()
    {
        yield return new WaitForSeconds(1.2f);
    }
    IEnumerator LoadLevelFourDelay()
    {
        yield return new WaitForSeconds(1.2f);
    }
    IEnumerator LoadLevelFiveDelay()
    {
        yield return new WaitForSeconds(1.2f);
    }
    IEnumerator LoadMainMenuDelay()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("MainMenu");
    }




    IEnumerator LoadLevelSelectDelay()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("World_Select");
    }
    IEnumerator LoadAnyDelay(string levelLoad)
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(levelLoad);
    }
    IEnumerator LoadGameFeaturesDelay()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("TestGameFeatures");
    }
    IEnumerator LoadCreditsDelay()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Credits");
    }
    IEnumerator LoadMeDelay()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(gameObject.name);
    }
    IEnumerator LastCheckandLevelDelay()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));
    }
    IEnumerator LoadBossOneDelay()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Level_1_Boss");
    }
    IEnumerator LoadBossTwoDelay()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Level_2_Boss");
    }


}
