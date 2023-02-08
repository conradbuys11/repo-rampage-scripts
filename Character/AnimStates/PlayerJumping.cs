﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : StateMachineBehaviour {

    public PlayerPushbox myPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myPlayer = FindObjectOfType<PlayerPushbox>();
        myPlayer.myRB.velocity = new Vector3(0, myPlayer.myRB.velocity.y, 0);
        Physics.IgnoreLayerCollision(9, 13, true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myPlayer.WhileJumping();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics.IgnoreLayerCollision(9, 13, false);
        myPlayer.ResetAnimTriggers();
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