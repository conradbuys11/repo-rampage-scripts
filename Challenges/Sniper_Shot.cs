using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_Shot : MonoBehaviour , IDamagable
{

    private float speed;

    private Transform shotPos;
    private Transform playerPos;

    private Vector3 target;
    private Vector3 offset;

    public float shotDis;

    public bool negative;

    public GameObject boom;

	void Start ()
    {
        speed = 20f;

        shotPos = gameObject.transform;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        shotDis = Vector3.Distance(shotPos.position, playerPos.position);

        if (shotDis < 2)
        {
            offset = new Vector3(0, 0, 0);
        }
        else
        {
            if (transform.position.x - playerPos.position.x > 0)
            {
                offset = new Vector3(-2, 0, 0);
            }
            else
            {
                offset = new Vector3(2, 0, 0);
            }
        }
        playerPos.position += offset;
        target = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
	}
	
	void Update ()
    {
           transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
           if (transform.position == target)
           {
               StartCoroutine(DespawnDelay());
           }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator DespawnDelay()
    {
        yield return new WaitForSeconds(3.0f);
        Instantiate(boom, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {

    }


    public void TakeEnviroDamage(int damage)
    {

    }

    public int GivePoints(float pnt)
    {
        return 0;
    }
}
