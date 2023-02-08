using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Atk_Trigger : MonoBehaviour
{
    public GameObject TutorialPanel;
    public GameObject KeyboardPrompt;
    public GameObject XBoxPrompt;
    public GameObject CloseBut;

    //tutorial bools letter changes chronologically for each panel
    bool tutorialC;

    PlayerPushbox myPlayer;
    Turtorial_Layout ControllerSet;

    void Start()
    {
        myPlayer = FindObjectOfType<PlayerPushbox>();
        ControllerSet = FindObjectOfType<Turtorial_Layout>();
    }

    void Update()
    {
        if (tutorialC && ControllerSet.Comp == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TutorialPanel.SetActive(false);
                Time.timeScale = 1;
                myPlayer.AllowInput(true);
                Destroy(KeyboardPrompt);
                Destroy(XBoxPrompt);

                Destroy(gameObject);
            }
        }
        if (tutorialC && ControllerSet.Comp == false)
        {
            //XBox Controller Input A?
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                TutorialPanel.SetActive(false);
                Time.timeScale = 1;
                myPlayer.AllowInput(true);
                Destroy(KeyboardPrompt);
                Destroy(XBoxPrompt);

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialC = true;
            TutorialPanel.SetActive(true);
            CloseBut.SetActive(true);

            //Prompt.SetActive(true);
            Time.timeScale = 0;
            myPlayer.AllowInput(false);
            if (ControllerSet.Comp == true)
            {
                KeyboardPrompt.SetActive(true);
            }
            else
            {
                XBoxPrompt.SetActive(true);
            }
        }
    }
}
