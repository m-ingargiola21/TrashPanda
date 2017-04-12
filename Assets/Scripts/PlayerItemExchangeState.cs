﻿using UnityEngine;
using System.Collections;

public class PlayerItemExchangeState : StateMachineBehaviour {
    [SerializeField]
    bool isGiving;
    [SerializeField]
    InventoryObject objectToExchange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        InventoryManager inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        if (isGiving)
            inventoryManager.inventoryObjects.Add(objectToExchange);
        else if (inventoryManager.inventoryObjects.Contains(objectToExchange))
        {
            inventoryManager.inventoryObjects.Remove(objectToExchange);
            animator.SetBool("hasItem", true);
        }
        else
            animator.SetBool("hasItem", false);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
