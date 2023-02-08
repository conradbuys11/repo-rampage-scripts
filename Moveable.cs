/* Creator: Tim Stiles
 * Date Created: 11/9/18
 * Function: Allows the player to punch an object to launch it a set distance.
 * 
 * References:
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    //public bool FacingRight;
    public bool isMoveable = true;

    private void OnTriggerStay(Collider other)
    {
        GameObject Cam = GameObject.Find("Player Camera").gameObject;

        if (other.GetComponent<Hitbox>() && isMoveable)
        {
            isMoveable = false;
            Cam.GetComponent<AudioDirector>().Env28();
            GetComponentInParent<PositionMatch>().Moving();
        }
    }
}
