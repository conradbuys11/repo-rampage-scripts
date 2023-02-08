/* Creator: Tim Stiles
 * Purpose: Gives fuctionality to GP_Canvas which controls the HP system and scoring system.
 * Date Created: 10/22/18
 * 
 * References & Links: (most of these are refreshers)
 *  
 * MCrafterzz - Unity forum user
 * https://forum.unity.com/threads/change-text-color.484117/
 *  
 * flashframe - Unity answers user
 * https://answers.unity.com/questions/1244968/changing-text-color-at-runtime-always-white.html
 * 
 * Jayanam - Unity Game Asset Animation Tutorial
 * https://www.youtube.com/watch?v=dEpH6-vwxYY
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GP_Canvas : MonoBehaviour
{
    private float HP;
    private float PrevHP;
    private float MaxHP;
    private float MinHP;
    private float EffectNum;
    private Text HPtxt;
    private GameObject HPiconGREEN;
    private GameObject HPiconRED;


    private Text equipmentText;

    private Text HSText;

    private GameObject NadeThrower;

    public GameObject Score_GO;
    public GameObject Health_GO;
    private Animator HPshake;
    private GameObject HPbarGreen;
    private GameObject HPbarRed;
    [SerializeField]
    float MatchGreenTime;
    [SerializeField]
    float RedUpdate;
    GameObject PainTint;

    private Animator RedFlash;

    public float ColorChangeTimer;
    public bool isPaused;
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    public GameObject loseScreen;
    public GameObject victoryScreen;

    public GameObject frontLeft;
    public GameObject frontRight;
    public GameObject backLeft;
    public GameObject backRight;

    PlayerStatus playerStatus;
    PlayerPushbox myPlayer;
    bool talkBox = false;


    Projectile proj;

    public GameObject DebugMenu;

    public GameObject PlayCon;

    float FlashKey;

    public bool isImmortal = true;


    public void TalkBox(bool on)
    {
        talkBox = on;
    }

    // Use this for initialization
    void Start()
    {
        isImmortal = false;
        isPaused = false;

        HPtxt = GameObject.Find("HPtext").GetComponent<Text>();
        myPlayer = FindObjectOfType<PlayerPushbox>();
        playerStatus = FindObjectOfType<PlayerStatus>();
        proj = FindObjectOfType<Projectile>();
        HPiconGREEN = FindObjectOfType<GreenIcon>().gameObject;
        HPiconRED = FindObjectOfType<RedIcon>().gameObject;

        HPshake = Health_GO.GetComponent<Animator>();
        HPbarGreen = GameObject.Find("HPbarGreen").gameObject;
        HPbarRed = GameObject.Find("HPbarRed").gameObject;
        PainTint = GameObject.Find("PainTint").gameObject;
        RedFlash = PainTint.GetComponent<Animator>();

        HPiconGREEN.SetActive(true);
        HPiconRED.SetActive(false);




        //Value Library:
        PrevHP = HP;
        MaxHP = 100;
        MinHP = 0;



        HealthDisplay();

    }

    // Update is called once per frame
    void Update()
    {
        if (isImmortal == true)
        {
            playerStatus.health = 999;
            if (playerStatus.health <= playerStatus.maxHealth)
            {
                playerStatus.health = 999;
            }
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (DebugMenu.activeInHierarchy == false)
            {
                DebugMenu.SetActive(true);
            }
            else
            {
                DebugMenu.SetActive(false);
            }
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !talkBox)
        {
            if (PauseMenu.activeInHierarchy == false)
            {
                PauseMenu.SetActive(true);
                myPlayer.AllowInput(false);
                Time.timeScale = 0;
            }
            else
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                myPlayer.AllowInput(true);
            }
        }



        ColorChangeTimer = ColorChangeTimer - Time.deltaTime;

        if (PauseMenu.activeInHierarchy == true)
        {
            isPaused = true;
        }

        else
        {
            isPaused = false;
        }

        if (!isPaused)
        {
            HealthManager();
            HealthDisplay();
            MatchGreenBar();
        }
    }


    //*******************************************Work in Progress*************************************************************
    void MatchGreenBar()
    {
        MatchGreenTime -= Time.deltaTime;
        RedUpdate -= Time.deltaTime;

        if (MatchGreenTime <= 0 && HPbarRed.GetComponent<Image>().fillAmount > HPbarGreen.GetComponent<Image>().fillAmount)
        {
            HPbarRed.GetComponent<Image>().fillAmount = HPbarGreen.GetComponent<Image>().fillAmount;
            //HPbarRed.GetComponent<Image>().fillAmount = playerStatus.health * 0.01f;
        }

    }
    //*******************************************************************************************************************

    //Self Explanitory
    void HealthManager()
    {

        //Checks if the player HP was decremented & changes the color of the Health icon and text.
        if (playerStatus.health < PrevHP)
        {
            HPbarGreen.GetComponent<Image>().fillAmount = playerStatus.health * 0.01f;
            MatchGreenTime = 1.3f; //**********Relivant Value in edits*****************************************************
            HPiconGREEN.SetActive(false);
            HPiconRED.SetActive(true);
            HPtxt.GetComponent<Text>().color = new Color32(208, 2, 27, 255);
            HPshake.SetBool("isDamaged", true);
            PrevHP = playerStatus.health;
            RedFlash.SetTrigger("TAKINDAMAGE");
            ColorChangeTimer = 0.2f;
        }

        //Updates the HPredBar to match the HPgreenBar whenever the green bar's fillAmount value becomes higher the red's
        if (playerStatus.health > PrevHP)
        {
            HPbarGreen.GetComponent<Image>().fillAmount = playerStatus.health * 0.01f;
            HPbarRed.GetComponent<Image>().fillAmount = playerStatus.health * 0.01f;
        }

        //Returns the Health icon, text and pain tinter to their original colors.
        if (ColorChangeTimer <= 0)
        {
            HPiconGREEN.SetActive(true);
            HPiconRED.SetActive(false);
            HPtxt.GetComponent<Text>().color = new Color32(64, 223, 0, 255);
            HPshake.SetBool("isDamaged", false);
            // ScoreShake.SetBool("isDamaged", false);
        }

        //Keeps playerStatus.health from going below MinHP.
        if (playerStatus.health <= 0)
        {
            HPtxt.GetComponent<Text>().color = new Color32(208, 2, 27, 255);
            HPiconGREEN.SetActive(false);
            HPiconRED.SetActive(true);

            playerStatus.health = playerStatus.minHealth;

        }

        //Keeps HP from going above MaxHP;
        if (playerStatus.health >= 100)
        {
            playerStatus.health = playerStatus.maxHealth;
        }

        //Checks if the player HP was incremented.
        if (playerStatus.health > PrevHP)
        {
            EffectNum = playerStatus.health - PrevHP;
            PrevHP = playerStatus.health;
        }
    }

    void HealthDisplay()
    {
        HPtxt.text = Mathf.RoundToInt(playerStatus.health).ToString();
    }

    public void OPM()
    {
        OptionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }
    public void ClosOPM()
    {
        OptionsMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }

    //Debug Menu
    public void Immortal()
    {
        isImmortal = !isImmortal;
    }
    public void Die()
    {
        playerStatus.health = 0;
    }
    // Health
    public void TakeDamage()
    {
        playerStatus.health += -10;
    }
    public void GainHealth()
    {
        playerStatus.health += 10;
    }

    public void MaxEquip()
    {
        proj.NadeCount = 3;
        playerStatus.currentEqu = playerStatus.maxEqu;
    }

    public void RestLev()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void FrontRightTraffic(bool on)
    {
        frontRight.SetActive(on);
    }

    public void FrontLeftTraffic(bool on)
    {
        frontLeft.SetActive(on);
    }

    public void BackRightTraffic(bool on)
    {
        backRight.SetActive(on);
    }

    public void BackLeftTraffic(bool on)
    {
        backLeft.SetActive(on);
    }
}
