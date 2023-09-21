using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : StateMachineBehaviour
{
    public override void OnStateMachineExit ( Animator animator , int stateMachinePathHash ) 
    {
        Debug.Log("!!!");
        Destroy(animator.gameObject);
    }
    
}
