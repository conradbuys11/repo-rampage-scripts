using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHMovement : StateMachineBehaviour
{
    private Transform thePlayer;
    private Transform hhPos;

    private float transDel;

    public float minT;
    public float maxT;
    public float hhSpeed;
    private float attackDis;
    private int randAtk;

    //Start
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transDel = Random.Range(minT, maxT);
        hhPos = GameObject.FindObjectOfType<Boss_Manager>().transform;
        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        attackDis = Vector3.Distance(thePlayer.transform.position, hhPos.transform.position);
        randAtk = Random.Range(0, 10);
    }

	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (transDel <= 0)
        {
            animator.SetBool("Is_Move", false);
            animator.SetTrigger("Is_Idle");
        }
        else
        {
            float front;
            float back;
            Vector3 target;
            front = Vector3.Distance(animator.transform.position, thePlayer.position + new Vector3(7, 0, 0));
            back = Vector3.Distance(animator.transform.position, thePlayer.position + new Vector3(-7, 0, 0));
            if (front > back)
                target = thePlayer.position + new Vector3(-4, 2, 0);
            else
                target = thePlayer.position + new Vector3(4, 2, 0);
            if(animator.transform.position.x - thePlayer.position.x < 0)
            {
                animator.transform.rotation = Quaternion.Euler(0, 0, 0);
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, target, hhSpeed * Time.deltaTime);
            }
            else if(animator.transform.position.x - thePlayer.position.x > 0)
            {
                animator.transform.rotation = Quaternion.Euler(0, -180, 0);
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, target, hhSpeed * Time.deltaTime);
            }
            transDel -= Time.deltaTime;
        }
        if (attackDis <= 8)
        {
            if(randAtk <= 7)
            {
                animator.SetTrigger("Is_Basic");
            }
            else
            {
                animator.SetTrigger("Is_Strong");
            }           
        }
    }


	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
