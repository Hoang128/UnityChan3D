using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAnims", menuName = "Scriptable Objects/Character Anims Config", order = 1)]
[System.Serializable]
public class CharacterAnimStringConfig : ScriptableObject
{
    public string STANDING_LOOP;
    public string RUNNING_LOOP;
    public string WALKING_LOOP;
    public string JUMP_TO_TOP;
    public string TOP_OF_JUMP;
    public string TOP_TO_GROUND;
}
