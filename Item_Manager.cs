using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{

    PlayerPushbox armRef;
    Item_Area itemSlot;

    public bool is_Pickup;
    public bool holding;

    public GameObject Item;
    private GameObject Instance;

    private float POWAH;
    private Vector3 Direction;
    //public bool facingRight;

    void Start ()
    {
        armRef = FindObjectOfType<PlayerPushbox>();
        itemSlot = FindObjectOfType<Item_Area>();
        is_Pickup = false;
        holding = false;

        POWAH = 10.0f;
        Direction = new Vector3(100, 0, 0);
        //facingRight = true;
	}

	void Update ()
    {
        PickUpItem();
        DropItem();
        Throwable();
        ChangeDirection();
	}

    public void OnTriggerEnter(Collider collision)
    {
        is_Pickup = true;      
    }

    public void OnTriggerExit(Collider other)
    {
        is_Pickup = false;
    }

    public void PickUpItem()
    {
        if (is_Pickup == true && armRef.isArmed == false)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                transform.position = itemSlot.handRef;
                transform.parent = FindObjectOfType<Item_Area>().gameObject.transform;
                armRef.isArmed = true;
                holding = true;
            }
        }
    }

    public void DropItem()
    {
        if (armRef.isArmed == true && holding)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                gameObject.transform.parent = null;
                armRef.isArmed = false;
                holding = false;
            }
        }
    }

    void ChangeDirection()
    {
    //    if (Input.GetKeyDown(KeyCode.D) && !FindObjectOfType<PlayerMovement>().FacingRight)
    //    {
    //        FindObjectOfType<PlayerMovement>().FacingRight = true;
    //    }

    //    else if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        FindObjectOfType<PlayerMovement>().FacingRight = false;
    //    }
    }

    public void Throwable()
    {
        if (holding && Input.GetKeyDown(KeyCode.K))
        {
            Instance = Instantiate(Item, transform.position, transform.rotation) as GameObject;
            Instance.GetComponent<Rigidbody>().AddForce(Vector3.forward * POWAH);
            holding = false;
            armRef.isArmed = false;
            Destroy(gameObject);
        }
    }
}
