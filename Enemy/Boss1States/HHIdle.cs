using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHIdle : StateMachineBehaviour
{
    private Transform thePlayer;
    private Transform hhPos;

    private float moveDel;

    public float IminT;
    public float ImaxT;
    private float attackDis;
    private int randAtk;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveDel = Random.Range(IminT, ImaxT);
        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        hhPos = GameObject.FindObjectOfType<Boss_Manager>().transform;
        attackDis = Vector3.Distance(thePlayer.transform.position, hhPos.transform.position);
        randAtk = Random.Range(0, 10);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (moveDel <= 0)
        {
            animator.SetBool("Is_Move", true);
        }
        else
        {
            moveDel -= Time.deltaTime;
        }
        if (attackDis <= 8)
        {
            if (randAtk <= 7)
            {
                animator.SetTrigger("Is_Basic");
            }
            else
            {
                animator.SetTrigger("Is_Strong");
            }
        }

    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
