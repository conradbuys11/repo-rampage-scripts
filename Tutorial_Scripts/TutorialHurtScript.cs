using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHurtScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStatus>().health = 15;
        }
    }
}
