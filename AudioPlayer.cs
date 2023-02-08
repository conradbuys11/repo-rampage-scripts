using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource SoundPlayer;
    public AudioClip[] attackNoises;
    public AudioClip[] hurtNoises;
    public AudioClip[] miscNoises;

    private int BlockIndex;
    private int LandIndex;
    private int JumpIndex;
    private int WalkIndex;
    private int HitIndex;
    private int DeathIndex;

    // Use this for initialization
    void Awake()
    {
        SoundPlayer = GetComponent<AudioSource>();
        SoundPlayer.volume = PlayerPrefs.GetFloat("SFXvol", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlaySound(AudioClip temp)
    {
        SoundPlayer.PlayOneShot(temp);
    }

    public void QuickEnemyAttack()
    {
        int x = Random.Range(0, 3);
        PlaySound(attackNoises[x]);
    }

    public void QuickEnemyDeath()
    {
        int x = Random.Range(0, 2);
        PlaySound(attackNoises[x]);
    }

    public void Thwap()
    {
        int x = Random.Range(3, 9);
        PlaySound(hurtNoises[x]);
    }

    public void PlayerAttack()
    {
        int temp = Random.Range(0, 5);
        PlaySound(attackNoises[temp]);
    }

    public void AttackGrunt()
    {
        int x = Random.Range(10, 16);
        PlaySound(attackNoises[x]);
    }

    public void HeavyGrunt()
    {
        int x = Random.Range(16, 19);
        PlaySound(attackNoises[x]);
    }

    public void GrabSound()
    {
        PlaySound(attackNoises[19]);
    }

    public void HadoukenSound()
    {
        int x = Random.Range(20, 22);
        PlaySound(attackNoises[5]);
        PlaySound(attackNoises[x]);
    }

    public void TipperSound()
    {
        PlaySound(attackNoises[6]);
    }

    public void BladestormSound()
    {
        PlaySound(attackNoises[7]);
    }

    public void FALCON()
    {
        PlaySound(attackNoises[8]);
    }

    public void PAWNCH()
    {
        PlaySound(attackNoises[9]);
    }

    public void PlayerHurt()
    {
        int temp = Random.Range(0, 3);
        PlaySound(hurtNoises[temp]);
    }

    public void Death()
    {
        PlaySound(hurtNoises[9]);
    }

    public void DashSound()
    {
        PlaySound(miscNoises[1]);
    }

    public void JumpGrunt()
    {
        PlaySound(miscNoises[2]);
    }

    public void GettingUp()
    {
        PlaySound(miscNoises[5]);
    }

    public void PlayerMoveSound()
    {
        //SoundPlayer.GetComponent<AudioSource>().clip = miscNoises[0];
        //SoundPlayer.loop = true;
        //SoundPlayer.Play();
    }

    public void StopSound()
    {
        SoundPlayer.Stop();
        SoundPlayer.loop = false;
    }

    //public void PlayerBlock()
    //{
    //    //BlockIndex = Random.Range(8, 16);
    //    PlaySound(19);
    //}

    //public void PlayerLanded()
    //{
    //    PlaySound(20);
    //}

    //public void PlayerJump()
    //{
    //    PlaySound(29);
    //}

    //public void Walk()
    //{
    //    PlaySound(7);
    //}

    //public void PlayerHit()
    //{
    //    PlaySound(18);
    //}

    //public void PlayerRole()
    //{
    //    PlaySound(40);
    //}

    //public void PlayerDeath()
    //{
    //    PlaySound(10);
    //}



}

