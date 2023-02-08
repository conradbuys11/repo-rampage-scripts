/* Purpose: Switch between scenes with UI button 
 *          Now handles the audil sliders and saves data for managing sfx and music volume.
 * Creator: Bryan Garcia
 * Date: Oct 22, 2018
 * 
 * Contents: Scene to change to Main Menu, Credits, Game Features, Quit. 
 * 
 * Refrences:
 * 
 * Sebastian Lague - Unity Create a Game Series (E24. Menu)
 * https://www.youtube.com/watch?v=EA-tBcTxE8M&t=1555s
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject LevSele;

    public GameObject AreSureWin;

    public GameObject Pause;

    public GameObject OpMen;

    public GameObject Main;

    public GameObject Cont;

    public Slider[] MenuSlider;

    Level_Manager loadCheck;


    // Use this for initialization
    void Start()
    {
        loadCheck = FindObjectOfType<Level_Manager>();
        if(MenuSlider.Length > 0)
        {
            MenuSlider[0].value = PlayerPrefs.GetFloat("SFXvol", MenuSlider[0].value);
            MenuSlider[1].value = PlayerPrefs.GetFloat("MusicVol", MenuSlider[1].value);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        Pause.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OpLevelSel()
    {
        LevSele.SetActive(true);
        Main.SetActive(false);
    }
    public void CloLevelSel()
    {
        LevSele.SetActive(false);
        Main.SetActive(true);
    }

    public void ToQuit()
    {
        Application.Quit();

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }

    public void ToAreYouSure()
    {
        AreSureWin.SetActive(true);
    }

    public void ToNotSure()
    {
        AreSureWin.SetActive(false);
    }

    public void OpenOption()
    {
        OpMen.SetActive(true);
        Main.SetActive(false);
    }
    public void CloseOption()
    {
        OpMen.SetActive(false);
        Main.SetActive(true);
    }
    
    public void SetVolumeSFX(float value)
    {
        //Was giving Error. Current main menu doesnt have "Audio" as an object
        //GameObject.Find("Audio").gameObject.GetComponent<AudioSource>().volume = MenuSlider[0].value;
        GameObject.Find("Player3.0").gameObject.GetComponent<AudioSource>().volume = MenuSlider[0].value;
        GameObject.Find("EnemyAudio").gameObject.GetComponent<AudioSource>().volume = MenuSlider[0].value;
        GameObject.Find("EnviromentAudio").gameObject.GetComponent<AudioSource>().volume = MenuSlider[0].value;
        PlayerPrefs.SetFloat("SFXvol", MenuSlider[0].value);
    }

    public void SetVolumeMusic(float value)
    {
        GameObject.Find("MusicPlayer").gameObject.GetComponent<AudioSource>().volume = MenuSlider[1].value;
        PlayerPrefs.SetFloat("MusicVol", MenuSlider[1].value);
    }
}
