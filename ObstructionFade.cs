using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionFade : MonoBehaviour {

    private GameObject obs;
    private Renderer wallRend;
    private Color32 obsColor = new Color32(94, 94, 94, 255);
    private Color32 obsFade = new Color32(94, 94, 94, 110);
    private float rate = 2f;
    private float t = 0;
    private bool behind;
    float startTime;
    int stillBehind;
	// Use this for initialization
	void Start ()
    {
        obs = GetComponentInChildren<Obstruction>().gameObject;
        wallRend = obs.GetComponent<Renderer>();
        behind = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Fade();
        Unfade();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            behind = true;
            stillBehind++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stillBehind--;
        if ((other.tag == "Player" && stillBehind == 0) || (other.CompareTag("Enemy") && stillBehind == 0))
        {
            behind = false;
        }
    }

    private void Fade()
    {
        if (behind && t < 1)
        {
            t += Time.deltaTime * rate;
            Mathf.Clamp(t, 0, 1);
            wallRend.material.color = Color.Lerp(obsColor, obsFade, t);
        }
    }

    private void Unfade()
    {
        if (!behind && t > 0)
        {
            t -= Time.deltaTime * rate;
            Mathf.Clamp(t, 0, 1);
            wallRend.material.color = Color.Lerp(obsColor, obsFade, t);
        }

    }
}
