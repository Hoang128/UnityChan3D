using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : IStateMachineUser
{
    private T stateMachineUser;

    private State<T> currentState;

    public State<T> CurrentState { get => currentState; set => currentState = value; }
    public T StateMachineUser { get => stateMachineUser; set => stateMachineUser = value; }

    public StateMachine(T stateMachineUser)
    {
        this.stateMachineUser = stateMachineUser;
    }

    public void StateHandleInput()
    {
        if (currentState != null)
            currentState.OnHandleInput();
        else
            Debug.Log("Don't have any state in state machine to handle input!");
    }

    public void StateLogicUpdate()
    {
        if (currentState != null)
            currentState.OnLogicUpdate();
        else
            Debug.Log("Don't have any state in state machine to update logic!");
    }

    public void StatePhysicsUpdate()
    {
        if (currentState != null)
            currentState.OnPhysicsUpdate();
        else
            Debug.Log("Don't have any state in state machine to update physics!");
    }

    public void StateChange(State<T> state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter();
        }
    }
}