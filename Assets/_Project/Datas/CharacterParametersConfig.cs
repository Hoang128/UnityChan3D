using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterParameters", menuName = "Scriptable Objects/Character Parameters Config", order = 1)]
[System.Serializable]
public class CharacterParametersConfig : ScriptableObject
{
    public float moveSpeedMax;
    public float moveSpeedMin;
    public float moveUpSpeed;
    public float moveDownSpeed;
    public float moveSideAngle;
    public float jumpSpeed;
    public float groundDetectDistance;
    public float waitTimeToJump;
    public float waitTimeToLand;
    public float runAnimationSpeedMax;
    public float runAnimationSpeedMin;
    public float hardChangeDirAngleMin;
}
