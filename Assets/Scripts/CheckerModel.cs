using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckerModel", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class CheckerModel : ScriptableObject
{
    public Sprite usualSprite;
    public Sprite queenSprite;
    public Side side;
}
