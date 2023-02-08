using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMatch : MonoBehaviour
{
    private Vector3 Pos;
    //public bool FacingRight;
    private Vector3 Direction;
    private float DaPowah = 10.0f;
    bool canDamage = false;
    private float TimerCount;
    public float MoveableProDamage;
    Moveable myHurtbox;
    Rigidbody myRb;

    List<EnemyBase> enemiesCollidedWith;

    private void Awake()
    {
        myHurtbox = GetComponentInChildren<Moveable>();
        myRb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        //FacingRight = true;
        enemiesCollidedWith = new List<EnemyBase>();
        Direction = new Vector3(100, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canDamage)
        {
            myRb.mass = 10000;
        }
        else
        {
            myRb.mass = 1;
        }
        ChangeDirection();
        //DirectionFacing();
    }

    void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.D) && !FindObjectOfType<PlayerMovement>().FacingRight)
        {
            FindObjectOfType<PlayerMovement>().FacingRight = true;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            FindObjectOfType<PlayerMovement>().FacingRight = false;
        }
    }

    //void DirectionFacing()
    //{
    //    if (FindObjectOfType<PlayerMovement>().FacingRight)
    //    {
    //        Direction = new Vector3(100, 0, 0);
    //    }

    //    else
    //    {
    //        Direction = new Vector3(-100, 0, 0);
    //    }
    //}

    public void Moving()
    {
        myRb.mass = 1;
        canDamage = true;
        myRb.AddForce(Direction * DaPowah);
        StartCoroutine(IsMoveableTimer());
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject Cam = GameObject.Find("Player Camera").gameObject;

        if (other.gameObject.GetComponent<EnemyBase>() != null && canDamage)
        {
            foreach (EnemyBase pushbox in enemiesCollidedWith)
            {
                if (other.gameObject.GetComponent<EnemyBase>() == pushbox) { return; }
            }
            Cam.GetComponent<AudioDirector>().EnemyLanded();
            other.GetComponent<EnemyBase>().pointMod = 3f;
            other.SendMessage("GetHitBy", MoveableProDamage);
            other.GetComponent<EnemyBase>().ResetPointModifier();
            Cam.GetComponent<AudioDirector>().EnemyAttack();
            enemiesCollidedWith.Add(other.gameObject.GetComponent<EnemyBase>());
        }
    }

    IEnumerator IsMoveableTimer()
    {
        yield return new WaitForSeconds(.5f);
        canDamage = false;
        myHurtbox.isMoveable = true;
        myRb.velocity = Vector3.zero;
        enemiesCollidedWith.Clear();
    }
}
