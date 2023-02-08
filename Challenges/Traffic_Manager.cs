using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_Manager : MonoBehaviour
{
    public GameObject Right_Truck;
    public GameObject Left_Truck;
    GP_Canvas canvasRef;

    private float playDis;
    public float CurrentTimer;
    private float MaxTimer;
    private float TruckTimer;

    public float delay; //a delay for the first cycle to be ran. i.e. delay = 5? waits 5 seconds to start first cycle
    float delayMax; //storing the max value of delay to reset to when player leaves trigger
    private int attack;
    public int begin;
    public int end;
    private int time = 2;
    private int mode = 2;

    public Transform A_poin;
    public Transform B_poin;
    private Transform playPos;
    public Transform TrafficPos;
    private Transform l_pos;
    private Transform r_pos;
    private Transform L_Light;
    private Transform R_Light;

    private Color offColor = Color.gray;
    private Color redColor = Color.red;
    private Color blueColor = Color.blue;

    public Behaviour l_halo;
    public Behaviour r_halo;

    public bool random;
    public bool truckGoesLeftToRight;
    public bool frontLane;
    bool reset = false;
    public FightArea myFightArea;

    private void Awake()
    {
        canvasRef = FindObjectOfType<GP_Canvas>();
    }

    void Start ()
    {
        MaxTimer = CurrentTimer;
        TruckTimer = 5.0f;

        playPos = GameObject.FindGameObjectWithTag("Player").transform;

        l_pos = this.gameObject.transform.GetChild(2).transform;
        r_pos = this.gameObject.transform.GetChild(3).transform;
        L_Light = this.gameObject.transform.GetChild(4).transform;
        R_Light = this.gameObject.transform.GetChild(5).transform;
        L_Light.GetComponent<Renderer>().material.color = offColor;
        R_Light.GetComponent<Renderer>().material.color = offColor;

        attack = Random.Range(begin, end);
        delayMax = delay;
    }

    public void EndFunctionality()
    {
        L_Light.GetComponent<Renderer>().material.color = offColor;
        R_Light.GetComponent<Renderer>().material.color = offColor;
        canvasRef.FrontRightTraffic(false);
        canvasRef.FrontLeftTraffic(false);
        canvasRef.BackRightTraffic(false);
        canvasRef.BackLeftTraffic(false);
        reset = false;
    }


    void Update()
    {
        if (random)
        {
            CurrentTimer = attack;
        }

        if (myFightArea != null)
        {
            if (myFightArea.Battle_Barrier.activeSelf)
            {
                BetweenCycles();
            }
            else if (reset)
            {
                EndFunctionality();
            }
        }
        else
        {
            playDis = Vector3.Distance(playPos.transform.position, TrafficPos.transform.position);
            if (playDis >= 35)
            {
                //l_halo.enabled = false;
                //r_halo.enabled = false;
                L_Light.GetComponent<Renderer>().material.color = offColor;
                R_Light.GetComponent<Renderer>().material.color = offColor;
                CurrentTimer = MaxTimer;
                delay = delayMax;
            }
            else
            {
                BetweenCycles();
            }
        }
    }

    void BetweenCycles()
    {
        reset = true;
        if(delay >= 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            CurrentTimer -= Time.deltaTime;
            if(CurrentTimer <= 0)
            {
                WarningAlarm();
                L_Light.GetComponent<Renderer>().material.color = redColor;
                R_Light.GetComponent<Renderer>().material.color = blueColor;
            }
            else
            {
                L_Light.GetComponent<Renderer>().material.color = offColor;
                R_Light.GetComponent<Renderer>().material.color = offColor;
            }
        }
    }

    void Alarm()
    {
        switch (time)
        {
            case 2:
                L_Light.transform.position = r_pos.transform.position;
                R_Light.transform.position = l_pos.transform.position;
                break;
            case 1:
                L_Light.transform.position = l_pos.transform.position;
                R_Light.transform.position = r_pos.transform.position;
                break;
            default:
                L_Light.transform.position = r_pos.transform.position;
                R_Light.transform.position = l_pos.transform.position;
                break;
        }
    }

    void WarningAlarm()
    {
        Alarm();
        if (time == 2)
        {
            StartCoroutine(LeftDelay());
        }
        if (time == 1)
        {
            StartCoroutine(RightDelay());
        }
        //l_halo.enabled = true;
        //r_halo.enabled = true;

        TruckTimer -= Time.deltaTime;

        if (TruckTimer <= 2)
        {
            if (frontLane)
                {
                    if (truckGoesLeftToRight)
                    {
                        canvasRef.FrontLeftTraffic(true);
                    }
                    else
                    {
                        canvasRef.FrontRightTraffic(true);
                    }
                }
            else
            {
                if (truckGoesLeftToRight)
                    canvasRef.BackLeftTraffic(true);
                else
                    canvasRef.BackRightTraffic(true);
            }
            
        }

        if(TruckTimer <= 0)
        {
            if (truckGoesLeftToRight)
            {
                Instantiate(Right_Truck, B_poin);
            }
            if (!truckGoesLeftToRight)
            {
                Instantiate(Left_Truck, A_poin);
            }
            canvasRef.FrontRightTraffic(false);
            canvasRef.FrontLeftTraffic(false);
            canvasRef.BackRightTraffic(false);
            canvasRef.BackLeftTraffic(false);
            CurrentTimer = MaxTimer;
            TruckTimer = 5.0f;
            //l_halo.enabled = false;
            //r_halo.enabled = false;
        }

    }

    IEnumerator LeftDelay()
    {
        yield return new WaitForSeconds(1f);
        time = 1;
    }

    IEnumerator RightDelay()
    {
        yield return new WaitForSeconds(1f);
        time = 2;      
    }
}
