using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This only handles the rotation of the player
//In place until we can get rotation for the parent in PLayerPushbox script
public class PlayerMovement : MonoBehaviour
{
    //PlayerPushbox myRotate;
    public bool FacingRight;

    void Start()
    {
        //myRotate = FindObjectOfType<PlayerPushbox>();
        FacingRight = true;
    }

    private void Update()
    {
        //Facing();

        //if (myRotate.myRB.velocity.x <= -.001)
        //{
        //    myRotate.myRotation = Quaternion.AngleAxis(180, Vector3.up);
        //}
        //else if (myRotate.myRB.velocity.x >= .001)
        //{
        //    myRotate.myRotation = Quaternion.AngleAxis(0, Vector3.up);
        //}

        //transform.rotation = myRotate.myRotation;
    }

    void Facing()
    {
        //if (myRotate.myRotation == Quaternion.AngleAxis(180, Vector3.up))
        //{
        //    FacingRight = false;
        //}

        //else if (myRotate.myRotation == Quaternion.AngleAxis(0, Vector3.up))
        //{
        //    FacingRight = true;
        //}


    }















    //   Rigidbody playerRB;

    //   public Animator playerAnimator;

    //   Quaternion playerRotation;

    //   [Header("Player Move Stats")]
    //   public float moveUp;
    //   public float moveRight;
    //   public float speed;
    //   [SerializeField]float stopSpeed;


    //// Use this for initialization
    //void Start ()
    //   {
    //       playerRB = GetComponent<Rigidbody>();
    //       playerAnimator = GetComponentInChildren<Animator>();
    //}

    //   //IEnumerator RollTime()
    //   //{
    //   //    yield return new WaitForSeconds(0.2f);
    //   //    playerAnimator.SetBool("Is_Roll", false);
    //   //}

    //   //For the canned jump mechanic, see below...
    //   //IEnumerator JumpDelay()
    //   //{
    //   //    yield return new WaitForSeconds(0.1f);
    //   //    playerAnimator.SetBool("Is_Jump", false);
    //   //}
    //   //IEnumerator FallDelay()
    //   //{
    //   //    yield return new WaitForSeconds(0.6f);
    //   //    playerAnimator.SetBool("Is_Airborne", false);
    //   //}

    //// Update is called once per frame
    //void Update ()
    //   {
    //       //Value for moving player along the X axis
    //       moveRight = Input.GetAxis("Horizontal");
    //       //Value for moving player along the Y axis
    //       moveUp = Input.GetAxis("Vertical");
    //       //Force that moves the player based on the values moveRight, MoveUp and speed.
    //       playerRB.velocity = new Vector3(moveRight * speed, moveUp * speed, 0);

    //       //Zeros out the movement if the value is bellow a thresh hold set in the inspector.
    //       //Stops the player from feeling floaty of like they are on ice.
    //       if(Input.GetAxis("Horizontal") <= stopSpeed && Input.GetAxis("Horizontal") >= -stopSpeed)
    //       {
    //           playerRB.velocity = new Vector3(0, playerRB.velocity.y, 0);
    //       }
    //       //Zeros out the movement if the value is bellow a thresh hold set in the inspector.
    //       //Stops the player from feeling floaty of like they are on ice.
    //       if (Input.GetAxis("Vertical") <= stopSpeed && Input.GetAxis("Vertical") >= -stopSpeed)
    //       {
    //           playerRB.velocity = new Vector3(playerRB.velocity.x, 0, 0);
    //       }

    //       if(Input.GetAxis("Vertical") >= .8 || Input.GetAxis("Vertical") <= -.8)
    //       {
    //           stopSpeed = .8f;
    //       }
    //       else
    //       {
    //           stopSpeed = .1f;
    //       }

    //       if (Input.GetAxis("Horizontal") <= .8 || Input.GetAxis("Horizontal") >= -.8)
    //       {
    //           stopSpeed = .8f;
    //       }
    //       else
    //       {
    //           stopSpeed = .1f;
    //       }

    //       if (playerRB.velocity == Vector3.zero)
    //       {
    //           playerAnimator.SetBool("Is_Move", false);
    //       }
    //       else
    //       {
    //           playerAnimator.SetBool("Is_Move", true);
    //       }
    //       if(playerRB.velocity.x <= -.01)
    //       {
    //           playerRotation = Quaternion.AngleAxis(180, Vector3.up);
    //       }
    //       else if(playerRB.velocity.x >= .01)
    //       {
    //           playerRotation = Quaternion.AngleAxis(0, Vector3.up);
    //       }


    //       transform.rotation = playerRotation;

    //       PlayerRoll();
    //       //PlayerJump();
    //   }
    //   public void PlayerRoll()
    //   {
    //       if (Input.GetKeyDown(KeyCode.Space))
    //       {
    //           playerAnimator.SetTrigger("Is_Roll");
    //       }
    //   }
    //Canned jump mechanic, left it in here in case we want to change our minds...
    //public void PlayerJump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        playerRB.AddForce(Vector3.up * 10000);
    //        playerAnimator.SetBool("Is_Jump", true);
    //        playerAnimator.SetBool("Is_Airborne", true);
    //        StartCoroutine(JumpDelay());
    //        StartCoroutine(FallDelay());
    //    }
    //}
}
