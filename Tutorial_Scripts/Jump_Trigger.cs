using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Trigger : MonoBehaviour
{
    public GameObject TutorialPanel;
    public GameObject KeyboardPrompt;
    public GameObject XBoxPrompt;
    public GameObject CloseBut;

    //tutorial bools letter changes chronologically for each panel
    bool tutorialF;

    PlayerPushbox myPlayer;
    Turtorial_Layout ControllerSet;

    void Start()
    {
        myPlayer = FindObjectOfType<PlayerPushbox>();
        ControllerSet = FindObjectOfType<Turtorial_Layout>();
    }

    void Update()
    {
        if (tutorialF && ControllerSet.Comp == true)
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
        if (tutorialF && ControllerSet.Comp == false)
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
            tutorialF = true;
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
