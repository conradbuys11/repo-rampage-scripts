using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turtorial_Layout : MonoBehaviour
{
    public Text prompt;
    public Text Continue;
    public Text Activate;

    public InputField TextBox;

    PlayerPushbox myPlayer;

    public GameObject Tutorial;
    public GameObject Controls;
    public GameObject XboxButton;
    public GameObject KeyboardButton;
    public GameObject Return;
    public GameObject A;

    public bool Comp;

	void Start ()
    {
        myPlayer = FindObjectOfType<PlayerPushbox>();
        myPlayer.AllowInput(false);
        //gameObject.SetActive(false);
	}

    public void KeyboardControls()
    {
        Comp = true;
        Return.SetActive(true);
        Tutorial.SetActive(false);
        myPlayer.AllowInput(true);
        Time.timeScale = 1;
        Destroy(Controls);
        Destroy(XboxButton);
        Destroy(KeyboardButton);
    }

    public void XBoxControls()
    {
        Comp = false;
        A.SetActive(true);
        Tutorial.SetActive(false);
        myPlayer.AllowInput(true);
        Time.timeScale = 1;
        Destroy(Controls);
        Destroy(XboxButton);
        Destroy(KeyboardButton);
    }

    public void ClosePrompt()
    {
        Tutorial.SetActive(false);
    }
}
