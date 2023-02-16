using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCharRun : State<PlayerController>
{
    public StateCharRun(StateMachine<PlayerController> stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachineUser = stateMachine.StateMachineUser;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnLogicUpdate()
    {
        base.OnLogicUpdate();

        stateMachineUser.CharacterJump();
        stateMachineUser.ChangeAnimationSpeedByRunSpeed();
        float topSpeed = stateMachineUser.CharacterParametersConfig.moveSpeedMax - stateMachineUser.CharacterParametersConfig.moveDownSpeed * 2f;

        if (stateMachineUser.Velocity.magnitude >= topSpeed * Time.deltaTime)
        {
            if (stateMachineUser.CameraController.CameraMode == CameraMode.NORMAL)
                stateMachineUser.CameraController.CameraMode = CameraMode.HIGH_SPEED;
        }
        
        if (stateMachineUser.Velocity.magnitude < topSpeed * Time.deltaTime)
        {
            if (stateMachineUser.CameraController.CameraMode == CameraMode.HIGH_SPEED)
                stateMachineUser.CameraController.CameraMode = CameraMode.NORMAL;
        }
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();

        stateMachineUser.CharacterPhysicsGroundMove();
        stateMachineUser.CharacterPhysicsStopMove();
    }
}
