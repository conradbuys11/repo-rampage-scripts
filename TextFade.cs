using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    float alpha;

    // Update is called once per frame
    void Update()
    {
        alpha = GetComponent<TextMesh>().color.a;
        alpha -= Time.deltaTime;
        GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, alpha);
        transform.position += new Vector3(0, .1f, 0) * Time.deltaTime;
        if(GetComponent<TextMesh>().color.a <= .1f)
        {
            Destroy(gameObject);
        }
    }
}
