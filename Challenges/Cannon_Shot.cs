using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Shot : MonoBehaviour , IDamagable
{
    private int speed = 10;

    public GameObject boom;

    void Start()
    {
        StartCoroutine(CannonShotDestruction());
    }

    void Update()
    {
        FireShot();
    }

    void FireShot()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(boom, transform.position, transform.rotation);
        Destroy(gameObject);
        if(other.gameObject.tag == "Player")
        {
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);            
        }
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

    IEnumerator CannonShotDestruction()
    {
        yield return new WaitForSeconds(5.0f);
        Instantiate(boom, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
