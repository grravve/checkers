using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersData
{
    private Vector2 _worldPosition;
    private Rank _checkerRank;
    private Side _checkerSide;

    public CheckersData(Checker checker)
    {
        _worldPosition = checker.transform.position;
        _checkerRank = checker.Rank;
        _checkerSide = checker.Side;
    }
}
