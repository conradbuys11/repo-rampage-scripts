using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Triggers : MonoBehaviour
{
    public string information;
    string input;
    string active;
    //TuteBasic Case: 6
    string baseAttack;
    //TuteBasic Case: 5
    string hadoAttack;
    //TuteBasic Case: 4
    string dashAttack;
    //TuteBasic Case: 3
    string grabAttack;
    //TuteBasic Case: 2
    string laneSwitch;
    //TuteBasic Case: 1
    string jump;

    public GameObject TutorialPanel;
    public GameObject ActivatePanel;

    bool queueTutorial;
    bool tutorial;
    bool help;

    public int TuteBasic = 6;

    PlayerPushbox myPlayer;
    public Turtorial_Layout ControllerSet;

    void Start()
    {
        myPlayer = FindObjectOfType<PlayerPushbox>();
        TutorialPanel.SetActive(false);
    }

    void Update()
    {
        OnXboxControls();
        
        if(ControllerSet.Comp == true)
        {
            input = "Return";
            active = "Return";
            baseAttack = "Press 'K' for Heavy and 'L' for Light attacks";
            hadoAttack = "Press 'I' for Hadouken ranged attack";
            dashAttack = "Press 'O' for Dash attack";
            grabAttack = "Press 'P' for Grab attack";
            laneSwitch = "Press 'W' to go to far lane, press 'S' to return to close lane";
            jump = "Press 'Space' to Jump";
        }
        else
        {
            input = "A";
            active = "A";
            baseAttack = "Press 'RT' for Heavy and 'RB' for Light attacks";
            hadoAttack = "Press 'X' for Hadouken ranged attack";
            dashAttack = "Press 'Y' for Dash attack";
            grabAttack = "Press 'B' for Grab attack";
            laneSwitch = "Press 'Up' to go to far lane, press 'Down' to return to close lane";
            jump = "Press 'Space' to Jump";
        }

        if (!tutorial)
            ControllerSet.Activate.text = "Press '" + active + "' to display turorial";
        else
            ControllerSet.Activate.text = "";

        if (tutorial && ControllerSet.Comp == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TutorialPanel.SetActive(false);
                Time.timeScale = 1;
                myPlayer.AllowInput(true);
                tutorial = false;
            }
        }
        if (tutorial && ControllerSet.Comp == false)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                TutorialPanel.SetActive(false);
                Time.timeScale = 1;
                myPlayer.AllowInput(true);
                tutorial = false;
            }
        }
        if (queueTutorial)
        {
            queueTutorial = false;
            tutorial = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivatePanel.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (ControllerSet.Comp == true && Input.GetKeyDown(KeyCode.Return))
        {
            queueTutorial = true;
            TutorialPanel.SetActive(true);
            Time.timeScale = 0;
            myPlayer.AllowInput(false);
            TuteBasicSystem();
            ControllerSet.Continue.text = "Press '" + input + "' to Continue";
            ActivatePanel.SetActive(false);
        }
        else if (ControllerSet.Comp == false && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            queueTutorial = true;
            TutorialPanel.SetActive(true);
            Time.timeScale = 0;
            myPlayer.AllowInput(false);
            TuteBasicSystem();
            ControllerSet.Continue.text = "Press '" + input + "' to Continue";
            ActivatePanel.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivatePanel.SetActive(false);
        }
    }

    private void OnXboxControls()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ControllerSet.Comp = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ControllerSet.Comp = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ControllerSet.Comp = true;
        }
    }

    private void TuteBasicSystem()
    {
        switch (TuteBasic)
        {
            case 6:
                ControllerSet.prompt.text = information + baseAttack;
                break;
            case 5:
                ControllerSet.prompt.text = information + hadoAttack;
                break;
            case 4:
                ControllerSet.prompt.text = information + dashAttack;
                break;
            case 3:
                ControllerSet.prompt.text = information + grabAttack;
                break;
            case 2:
                ControllerSet.prompt.text = information + laneSwitch;
                break;
            case 1:
                ControllerSet.prompt.text = information + jump;
                break;
            default:
                ControllerSet.prompt.text = information;
                break;

        }
    }
}
