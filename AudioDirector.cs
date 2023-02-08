/* Creator: Tim Stiles
 * Date Created: 10/29/18
 * Function: Plays background audio and instantiates audio clips to play in certain scenarios.
 * 
 * References:
 * 
 * GameGrind - Making a Simple Game in Unity: Playing Sound Effects: Unity C# Tutorial (Part 9)
 * https://www.youtube.com/watch?v=u5DaPCiP0Xs
 * 
 * IvovdMarel - Unity Answers User
 * https://answers.unity.com/questions/1331807/playing-multiple-sounds-at-the-same-time.html
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioDirector : MonoBehaviour
{
    public GameObject PlayerAudio;
    public GameObject EnemyAudio;
    public GameObject EnviromentAudio;
    public GameObject MusicPlayer;

    public AudioClip[] PlayerSound;
    public AudioClip[] EnemySound;
    public AudioClip[] EnviromentSound;
    public AudioClip[] Song;

    private int PsIndex = 6;

    //private int AttackIndex;
    //private int HurtIndex;
    //private int BlockIndex;
    //private int LandIndex;
    //private int JumpIndex;
    //private int WalkIndex;
    //private int HitIndex;
    //private int DeathIndex;

    private int EnemyAttackIndex;
    private int EnemyHurtIndex;
    private int EnemyBlockIndex;
    private int EnemyLandIndex;
    private int EnemyJumpIndex;
    private int EnemyWalkIndex;
    private int EnemyHitIndex;
    private int EnemyDeathIndex;

    private int EnvIndex;

    private GameObject MenuThing;
    public AudioSource Player;
    public AudioSource Enemy;
    public AudioSource Enviroment;
    public AudioSource Music;

    Scene curScene;
    int sceneIndex;
    GameObject Source;
    AudioSource trackPlayer;

    // Use this for initialization
    void Start ()
    {
        Player.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvol", 0.5f);
        Enemy.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvol", 0.5f);
        Enviroment.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvol", 0.5f);
        Music.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVol", 0.5f);

        curScene = SceneManager.GetActiveScene();
        sceneIndex = curScene.buildIndex;
        Source = GameObject.Find("MusicPlayer").gameObject;
        trackPlayer = Source.GetComponent<AudioSource>();

        SceneCheck();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void SceneCheck()
    {
        if (sceneIndex == 0)
        {
            trackPlayer.clip = Song[9];
            trackPlayer.Play();
        }

        else if (sceneIndex == 1)
        {
            trackPlayer.clip = Song[0];
            trackPlayer.Play();
        }

        else if (sceneIndex == 2)
        {
            trackPlayer.clip = Song[3];
            trackPlayer.Play();
        }

        else if (sceneIndex == 3)
        {
            trackPlayer.clip = Song[16];
            trackPlayer.Play();
        }

        else if (sceneIndex == 4)
        {
            trackPlayer.clip = Song[10];
            trackPlayer.Play();
        }

        else if (sceneIndex == 5)
        {
            trackPlayer.clip = Song[17];
            trackPlayer.Play();
        }

        else if (sceneIndex == 6)
        {
            trackPlayer.clip = Song[18];
            trackPlayer.Play();
        }

        else if (sceneIndex == 7)
        {
            trackPlayer.clip = Song[5];
            trackPlayer.Play();
        }

    }

    void E_PlaySound(int E_index)
    {
        EnemyAudio.GetComponent<AudioSource>().clip = EnemySound[E_index];
        EnemyAudio.GetComponent<AudioSource>().PlayOneShot(EnemySound[E_index]);
    }

    public void EnemyAttack()
    {
        E_PlaySound(0);
    }

    public void EnemyHurt()
    {
        E_PlaySound(7);
    }

    public void EnemyBlock()
    {
        EnemyBlockIndex = Random.Range(41, 45);
        E_PlaySound(EnemyBlockIndex);
    }

    public void EnemyLanded()
    {
        E_PlaySound(16);
    }

    public void EnemyWalk()
    {
        E_PlaySound(35);
    }

    public void EnemyHit()
    {
        EnemyHitIndex = Random.Range(45, 51);
        E_PlaySound(EnemyHitIndex);
    }

    public void EnemyDeath()
    {
        E_PlaySound(11);
    }

    public void EnemyJump()
    {
        E_PlaySound(5);
    }

    public void BruiserWindUp()
    {
        E_PlaySound(60);
    }

    public void BruiserAttack()
    {
        int x = Random.Range(54, 60);
        E_PlaySound(x);
    }

    public void BruiserDeath()
    {
        int x = Random.Range(61, 63);
        E_PlaySound(x);
    }

    public void ThugAttack()
    {
        int x = Random.Range(63, 66);
        E_PlaySound(x);
    }

    public void ThugDeath()
    {
        int x = Random.Range(66, 68);
        E_PlaySound(x);
    }

    void Env_PlaySound(int Env_index)
    {
        EnviromentAudio.GetComponent<AudioSource>().clip = EnviromentSound[Env_index];
        EnviromentAudio.GetComponent<AudioSource>().PlayOneShot(EnviromentSound[Env_index]);
    }

    public void Env1()
    {
        Env_PlaySound(0);
    }

    public void Env2()
    {
        Env_PlaySound(1);
    }

    public void Env3()
    {
        Env_PlaySound(2);
    }

    public void Env4()
    {
        Env_PlaySound(3);
    }

    public void Env5()
    {
        Env_PlaySound(4);
    }

    public void Env6()
    {
        Env_PlaySound(5);
    }

    public void Env7()
    {
        Env_PlaySound(6);
    }

    public void Env8()
    {
        Env_PlaySound(7);
    }

    public void Env9()
    {
        Env_PlaySound(8);
    }

    public void Env10()
    {
        Env_PlaySound(9);
    }

    public void Env11()
    {
        Env_PlaySound(10);
    }

    public void Env12()
    {
        Env_PlaySound(11);
    }

    public void Env13()
    {
        Env_PlaySound(12);
    }

    public void Env14()
    {
        Env_PlaySound(13);
    }

    public void Env15()
    {
        Env_PlaySound(14);
    }

    public void Env16()
    {
        Env_PlaySound(15);
    }

    public void Env17()
    {
        Env_PlaySound(16);
    }

    public void Env18()
    {
        Env_PlaySound(17);
    }

    public void Env19()
    {
        Env_PlaySound(18);
    }

    public void Env20()
    {
        Env_PlaySound(19);
    }

    public void Env21()
    {
        Env_PlaySound(20);
    }

    public void Env22()
    {
        Env_PlaySound(21);
    }

    public void Env23()
    {
        Env_PlaySound(22);
    }

    public void Env24()
    {
        Env_PlaySound(23);
    }

    public void Env25()
    {
        Env_PlaySound(24);
    }

    public void Env26()
    {
        Env_PlaySound(25);
    }

    public void Env27()
    {
        Env_PlaySound(26);
    }

    public void Env28()
    {
        Env_PlaySound(27);
    }

    public void Env29()
    {
        Env_PlaySound(28);
    }

    public void Env30()
    {
        Env_PlaySound(29);
    }

    public void Env31()
    {
        Env_PlaySound(30);
    }

    public void Env32()
    {
        Env_PlaySound(31);
    }

    public void Env33()
    {
        Env_PlaySound(32);
    }

    void PlayMusic(int Music_index)
    {
        GetComponent<AudioSource>().clip = Song[Music_index];
        GetComponent<AudioSource>().Play();
    }

    public void Song1()
    {
        PlayMusic(0);
    }

    public void Song2()
    {
        PlayMusic(1);
    }

    public void Song3()
    {
        PlayMusic(2);
    }

    public void Song4()
    {
        PlayMusic(3);
    }

    public void Song5()
    {
        PlayMusic(4);
    }

    public void Song6()
    {
        PlayMusic(5);
    }

    public void Song7()
    {
        PlayMusic(6);
    }

    public void VictorySong()
    {
        PlayMusic(7);
    }

    public void Song9()
    {
        PlayMusic(8);
    }

    public void Song10()
    {
        PlayMusic(9);
    }

















    void P_PlaySound(int P_index)
    {
        PlayerAudio.GetComponent<AudioSource>().clip = PlayerSound[P_index];
        PlayerAudio.GetComponent<AudioSource>().PlayOneShot(PlayerSound[P_index]);
    }

    //public void PlayerAttack()
    //{
    //        AttackIndex = Random.Range(0, 5);
    //        P_PlaySound(AttackIndex);      
    //}

    //public void PlayerHurt()
    //{
    //    HurtIndex = Random.Range(8, 20);
    //    P_PlaySound(HurtIndex);
    //}

    //public void PlayerBlock()
    //{
    //    BlockIndex = Random.Range(30, 34);
    //    P_PlaySound(BlockIndex);
    //}

    //public void PlayerLanded()
    //{
    //    P_PlaySound(20);
    //}

    //public void PlayerJump()
    //{
    //    P_PlaySound(29);
    //}

    //public void PlayerWalk()
    //{
    //    P_PlaySound(42);
    //}

    //public void PlayerHit()
    //{
    //    P_PlaySound(38);
    //}

    //public void PlayerRole()
    //{
    //    P_PlaySound(40);
    //}

    //public void PlayerDeath()
    //{
    //    P_PlaySound(14);
    //}
}
