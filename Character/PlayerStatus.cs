using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerStatus : MonoBehaviour, IDamagable
{
    Text livesText;

    //Current Extra live set up. 
    public int currentLives;
    public int maxLives;

    //Equipment flash
    public int currentEqu;
    public int maxEqu;

    //Curent, Max and......Min health values. 
    public int health;
    public int maxHealth;
    public int minHealth;

    //Curent and max levels for the player.
    public int level;
    public int maxLevel;

    //Game over screen for when player dies.
    public GameObject gameOver;

    //Bool used to check if the player is dead for respawn
    public bool isDead;
    public bool loss;
    bool respawning;
    PlayerPushbox moveRef;

    public int livesCount;

    public Checkpoint checkPos;
    PlayerMovement playRef;

    public GameObject[] lvlCheckPoints;
    public Transform cpRef;
    
    public int healingPotionCost;

    public float DeathTime;

    public float RespawnTime;

    float timer;

    public Animator Die;

    void Awake()
    {
        health = 100;
        maxHealth = 100;
        minHealth = 0;
        maxLives = 5;
        maxEqu = 3;
        livesCount = 3;
    }

    // Use this for initialization
    void Start()
    {
        //Set starting values for all items.
        livesText = GameObject.Find("Extra Lives Text").GetComponent<Text>();
        isDead = true;
        loss = false;
        livesCount = 1;
        livesText.text = "Lives: " + livesCount;
        moveRef = FindObjectOfType<PlayerPushbox>();
        playRef = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (respawning)
        {
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                moveRef.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

                if(timer < -.2f)
                {
                    timer = .2f;
                }
            }
            else
            {
                moveRef.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            }
        }
    }

    IEnumerator Respawn()
    {
        //player drops to death pose
        moveRef.myAnimator.Play("Player3_0_Death");
        //no longer accepts pain
        moveRef.myHurtbox.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        //turns on bool that starts flashing of player
        respawning = true;
        moveRef.myAnimator.ResetTrigger("Knockback");
        yield return new WaitForSeconds(2f);
        //stands player up and returns control
        moveRef.myAnimator.Play("Player3_0Battle_Idle");
        yield return new WaitForSeconds(3f);
        //accepts damage, stops flashing, and just incase player is currently invisible turns on mesh
        moveRef.myHurtbox.GetComponent<BoxCollider>().enabled = true;
        respawning = false;
        moveRef.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }

    public IEnumerator DeathDelay()
    {
        moveRef.myAnimator.Play("Player3_0_Death");
        //no longer accepts pain
        moveRef.myHurtbox.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<Level_Manager>().GetComponentInChildren<Animator>().SetTrigger("Fade_Out");
        NavMeshAgent[] enemies = FindObjectsOfType<NavMeshAgent>();
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Funtion handels all things death related. 
    void OnDeath()
    {
        if(livesCount < 0)
        {
            gameOver.SetActive(true);
            FindObjectOfType<PlayerPushbox>().gameObject.SetActive(false);
            Destroy(GetComponent<PlayerStatus>());
        }
        else
        {
            health = maxHealth;
            playRef.transform.position = checkPos.playerSpawnLocation;
            loss = true;
        }
    }
    
    public void TakeDamage(float dmg)
    {
        health -= Mathf.RoundToInt(dmg);

        if (health <= 0)
        {
            livesCount--;
            livesText.text = "Lives: " + livesCount;
            if (livesCount < 0)
            {
                moveRef.isAlive = false;
                StartCoroutine(DeathDelay());
            }
            else
            {
                StartCoroutine(Respawn());
                health = maxHealth;
            }
        }
    }

    public void TakeEnviroDamage(int damage)
    {

    }

    public int GivePoints(float mod)
    {
        return 0;
    }
}
