/* Creator: Tim Stiles
 * Date Created: 11/10/18
 * Function: Instantiates a stun grenade prefab that travels a certain distance and then explodes.
 * 
 * References:
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject StunNadePrefab;
    private GameObject ProjectileInstance;
    public int NadeCount;
    private float ForcePower;
    public float LifeTime;
    private Vector3 InstancePos;

    private Vector3 Direction;
    //public bool FacingRight;

    void Start ()
    {
        NadeCount = 99;
        ForcePower = 16.0f;

        //FacingRight = true;
        Direction = new Vector3(100, 0, 0);
    }

    void Update ()
    {
        if (NadeCount > 99)
        {
            NadeCount = 99;
        }

        if (NadeCount < 0)
        {
            NadeCount = 0;
        }

        NadeUse();
        DirectionChange();
        InstancePos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "StunNade")
        {
            ++NadeCount;
            Destroy(other.gameObject);
        }
    }

    void DirectionChange()
    {
        if (Input.GetKeyDown(KeyCode.D) && !FindObjectOfType<PlayerMovement>().FacingRight)
        {
            FindObjectOfType<PlayerMovement>().FacingRight = true;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            FindObjectOfType<PlayerMovement>().FacingRight = false;
        }
    }

    void NadeUse()
    {
        if (FindObjectOfType<PlayerMovement>().FacingRight)
        {
            Direction = new Vector3(100, 0, 0);
        }

        else
        {
            Direction = new Vector3(-100, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.J) && NadeCount > 0)
        {
            NadeCount = NadeCount - 1;
            ProjectileInstance = Instantiate(StunNadePrefab, InstancePos, transform.rotation) as GameObject;
            ProjectileInstance.GetComponent<Rigidbody>().AddForce(Direction * ForcePower);
        }

    }
}
