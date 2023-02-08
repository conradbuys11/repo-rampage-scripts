using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Fan_Manager : MonoBehaviour
{
    private Transform rotatePoint;

    public GameObject wind;

    public bool AlwaysOn;
    public bool Interval;
    private bool startUp;

    private int on;
    private int randFan;
    private int off = 2;

    private float timer;
    private float maxTimer;
    private float randTime;

	void Start ()
    {
        randTime = Random.Range(3.0f, 8.0f);
        timer = randTime;
        maxTimer = timer;
        rotatePoint = this.gameObject.transform.GetChild(2).transform;
        on = 1;
	}
		
	void Update ()
    {
        OneOrTheOther();
        IntervalMode();

        rotatePoint.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * on);

        if (AlwaysOn)
        {
            HitTheFan();
        }

        if (Interval)
        {
            timer -= Time.deltaTime;
            if(timer <= 0 && off == 2)
            {
                off = 1;
                timer = maxTimer;
            }
            if(timer <= 0 && off == 1)
            {
                off = 2;
                timer = maxTimer;
            }
        }
	}

    public void HitTheFan()
    {
        wind.SetActive(true);
        on = 10;
    }

    public void StopTheFan()
    {
        wind.SetActive(false);
        on = 1;
    }

    public void OneOrTheOther()
    {
        if (Interval)
        {
            AlwaysOn = false;
        }
        if (AlwaysOn)
        {
            Interval = false;
        }
    }

    public void IntervalMode()
    {
        if (Interval)
        {
            switch (off)
            {
                case 2:
                    {
                        StopTheFan();                      
                        break;
                    }
                case 1:
                    {
                        HitTheFan();
                        break;
                    }
                default:
                    {
                        HitTheFan();
                        break;
                    }
            }
        }
    }
}
