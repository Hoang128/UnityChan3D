using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCharIdle : State<PlayerController>
{
    public StateCharIdle(StateMachine<PlayerController> stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachineUser = stateMachine.StateMachineUser;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnHandleInput()
    {
        base.OnHandleInput();

        stateMachineUser.CharacterInputMoveStart();
        stateMachineUser.CharacterJump();
    }
}
