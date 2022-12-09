using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersData: IEquatable<CheckersData>
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

    public bool Equals(CheckersData other)
    {
        if(other._checkerRank != _checkerRank)
        {
            return false;
        }

        if(other._checkerSide != _checkerSide)
        {
            return false;
        }

        if(other._worldPosition.x != _worldPosition.x || other._worldPosition.y != _worldPosition.y)
        {
            return false;
        }

        return true;
    }
}
