using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> where T : IStateMachineUser
{
    protected StateMachine<T> stateMachine;
    protected T stateMachineUser;

    public State(StateMachine<T> stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachineUser = stateMachine.StateMachineUser;
    }

    public virtual void OnEnter()
    {
        //DisplayOnUI(UIManager.Alignment.Left);
        Debug.Log("enter state: " + GetType().Name);
    }

    public virtual void OnHandleInput()
    {

    }

    public virtual void OnLogicUpdate()
    {

    }

    public virtual void OnPhysicsUpdate()
    {

    }

    public virtual void OnExit()
    {
        Debug.Log("exit state: " + GetType().Name);
    }
}