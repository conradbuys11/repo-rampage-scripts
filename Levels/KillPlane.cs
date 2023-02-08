using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlane : MonoBehaviour {

    int currentScene;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerPushbox>())
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
