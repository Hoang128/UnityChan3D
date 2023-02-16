using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCharJump : State<PlayerController>
{
    public StateCharJump(StateMachine<PlayerController> stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachineUser = stateMachine.StateMachineUser;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        stateMachineUser.Animator.Play(stateMachineUser.CharacterAnimsConfig.TOP_OF_JUMP);
        float averageRunSpeed = (stateMachineUser.CharacterParametersConfig.runAnimationSpeedMax + stateMachineUser.CharacterParametersConfig.runAnimationSpeedMin) / 2f;
        if (stateMachineUser.Velocity.magnitude < averageRunSpeed)
            stateMachineUser.CameraController.CameraMode = CameraMode.JUMP;
    }

    public override void OnHandleInput()
    {
        base.OnHandleInput();

        //stateMachineUser.OnReleaseJumpButtonInAir();
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();

        float averageRunSpeed = (stateMachineUser.CharacterParametersConfig.runAnimationSpeedMax + stateMachineUser.CharacterParametersConfig.runAnimationSpeedMin) / 2f;
        if (stateMachineUser.Velocity.magnitude < averageRunSpeed)
            stateMachineUser.CameraController.CameraMode = CameraMode.NORMAL;
    }
}
