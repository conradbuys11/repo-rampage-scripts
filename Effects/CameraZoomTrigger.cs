using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    //Default CameraFollow varibales 
    CameraFollow cameraRef;
    float xPrevious;
    float yPrevious;
    float zPrevious;
    float camSpeedPrevious;
    float clippingPlanePrevious;


    //Used for resetting the trigger to its default vaules. 
    [Header("Camera stats")][Tooltip("Dont change for the zoom out trigger if you want default settings reapplied")]
    public float camSpeed;
    public float clippingPlane;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    
    [Header("Dont Change")] [Tooltip("DONT CHANGE")]
    public bool camResetTrigger;


    // Use this for initialization
    void Start ()
    {
        //Gets Refrence and assigns variables default vaules for the camera
        cameraRef = FindObjectOfType<CameraFollow>();
        xPrevious = cameraRef.xOffset;
        yPrevious = cameraRef.yOffset;
        zPrevious = cameraRef.zOffset;
        camSpeedPrevious = cameraRef.camSpeed;
        clippingPlanePrevious = cameraRef.clippingPlane;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks for player, 
        //Checks if a camera reset 
        //Sets vaules accordingly
        if (other.CompareTag("Player"))
        {
            if (camResetTrigger)
            {
                cameraRef.camSpeed = camSpeedPrevious;
                cameraRef.xOffset = xPrevious;
                cameraRef.yOffset = yPrevious;
                cameraRef.zOffset = zPrevious;
                cameraRef.GetComponent<Camera>().nearClipPlane = clippingPlanePrevious;
            }
            else
            {
                cameraRef.camSpeed = camSpeed;
                cameraRef.xOffset = xOffset;
                cameraRef.yOffset = yOffset;
                cameraRef.zOffset = zOffset;
                cameraRef.GetComponent<Camera>().nearClipPlane = clippingPlane;
            }
        }
    }
}
