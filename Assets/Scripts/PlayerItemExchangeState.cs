using UnityEngine;
using System.Collections;
//TLM
public class PlayerItemExchangeState : StateMachineBehaviour {
    [SerializeField]
    bool isGiving;
    [SerializeField]
    InventoryObject objectToExchange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        InventoryManager inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        if (isGiving && !inventoryManager.inventoryObjects.Contains(objectToExchange))
        {
            inventoryManager.inventoryObjects.Add(objectToExchange);
            inventoryManager.UpdateInventoryManager();
            animator.SetBool("hasItem", true);
        }
        else if (isGiving)
        {
            animator.SetBool("hasItem", true);
        }
        else if (!isGiving && inventoryManager.inventoryObjects.Contains(objectToExchange))
        {
            inventoryManager.inventoryObjects.Remove(objectToExchange);
            inventoryManager.UpdateInventoryManager();
            animator.SetBool("hasItem", false);
        }
        else if (!isGiving)
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
