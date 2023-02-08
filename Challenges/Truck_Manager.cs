using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck_Manager : MonoBehaviour
{
    public GameObject warningSign;
    public AudioClip honk;
    AudioSource audioSour;
    GP_Canvas canvasRef;
    GameObject warningPopUp;
    PlayerPushbox playerRef;
    float timer = 2.5f;

    bool sound;

    
    Vector3 targetPoint;

    private float travel;
	
	void Start ()
    {
        canvasRef = FindObjectOfType<GP_Canvas>();
        playerRef = FindObjectOfType<PlayerPushbox>();
        audioSour = GameObject.Find("EnviromentAudio").GetComponent<AudioSource>();

        //warningPopUp = Instantiate(warningSign, canvasRef.transform);

        audioSour.loop = false;
        audioSour.clip = honk;
        audioSour.Play();

        if (GetComponentInParent<Traffic_Manager>().transform.position.x > transform.position.x)
        {
            targetPoint = transform.position + new Vector3(100, 0, 0);
            //warningPopUp.transform.localPosition = new Vector3(-350, 23, 0);
        }
        else
        {
            targetPoint = transform.position + new Vector3(-100, 0, 0);
            //warningPopUp.transform.localPosition = new Vector3(350, 23, 0);
        }

        travel = 40;
        
    }
	

	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, travel * Time.deltaTime);
        timer -= Time.deltaTime;
        if (transform.position == targetPoint || timer <= 0)
        {
            audioSour.loop = false;
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<IDamagable>().TakeEnviroDamage(12);
        }
    }
}
