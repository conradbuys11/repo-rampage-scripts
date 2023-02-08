using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkBox : MonoBehaviour
{

    GameObject controller;
    GameObject keyboard;
    GameObject panel;
    Text nameText;
    public string nameContents;
    Text bodyText;
    public string[] controllerBodyContents;
    GP_Canvas canvas;

    public string[] keyboardBodyContents;

    bool controllerConnected;

    int talkboxIndex;
    bool triggerTalkbox;
    PlayerPushbox playerRef;

    private void Awake()
    {
        if(GameObject.Find("Controller_Continue_Panel (1)") == null)
        {
            controller = GameObject.Find("Controller_Continue_Panel");
        }
        else
        {
            controller = GameObject.Find("Controller_Continue_Panel (1)");
        }

        keyboard = GameObject.Find("Keyboard_Continue_Panel");
        panel = GameObject.Find("Tutorial_Panel");
        nameText = GameObject.Find("Title/Name").GetComponent<Text>();
        bodyText = GameObject.Find("Tutes").GetComponent<Text>();
        playerRef = FindObjectOfType<PlayerPushbox>();
        canvas = FindObjectOfType<GP_Canvas>();
    }

    private void Start()
    {

        controller.SetActive(false);
        keyboard.SetActive(false);
        panel.SetActive(false);

        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    controllerConnected = true;
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + i + " is disconnected.");

                }
            }
        }
    }

    private void Update()
    {
        if (triggerTalkbox)
        {
            if (controllerConnected)
            {
                playerRef.AllowInput(false);
                if (bodyText.text == "")
                {
                    bodyText.text = controllerBodyContents[talkboxIndex];
                    talkboxIndex++;
                }
                
                if (Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (talkboxIndex >= controllerBodyContents.Length)
                    {
                        playerRef.AllowInput(true);
                        Time.timeScale = 1;
                        canvas.TalkBox(false);
                        bodyText.text = "";
                        playerRef.AllowInput(true);
                        controller.SetActive(false);
                        keyboard.SetActive(false);
                        panel.SetActive(false);
                        triggerTalkbox = false;
                        Destroy(gameObject);
                    }
                    else
                    {
                        bodyText.text = controllerBodyContents[talkboxIndex];
                        talkboxIndex++;
                    }
                }
            }
            else
            {
                if (bodyText.text == "")
                {
                    bodyText.text = keyboardBodyContents[talkboxIndex];
                    talkboxIndex++;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (talkboxIndex >= keyboardBodyContents.Length)
                    {
                        Time.timeScale = 1;
                        canvas.TalkBox(false);
                        bodyText.text = "";
                        playerRef.AllowInput(true);
                        controller.SetActive(false);
                        keyboard.SetActive(false);
                        panel.SetActive(false);
                        triggerTalkbox = false;
                        Destroy(gameObject);
                    }
                    else
                    {
                        bodyText.text = keyboardBodyContents[talkboxIndex];
                        talkboxIndex++;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (controllerConnected)
            {
                controller.SetActive(true);
                keyboard.SetActive(false);
            }
            else
            {
                controller.SetActive(false);
                keyboard.SetActive(true);
            }
            playerRef.AllowInput(false);
            canvas.TalkBox(true);
            Time.timeScale = 0;
            panel.SetActive(true);
            nameText.text = nameContents;
            triggerTalkbox = true;
        }
    }
}
