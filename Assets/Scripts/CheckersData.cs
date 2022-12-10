using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersData: IEquatable<CheckersData>
{
    public Vector2 WorldPosition { get; private set; }
    public Rank CheckerRank { get; private set; }
    public Side CheckerSide { get; private set; }
    public int CurrentTurnIndex { get; private set; }
    public bool IsDead { get; private set; }

    public CheckersData(Checker checker, int currentTurnIndex)
    {
        WorldPosition = checker.transform.position;
        CheckerRank = checker.Rank;
        CheckerSide = checker.Side;
        CurrentTurnIndex = currentTurnIndex;
        IsDead = checker.gameObject.activeSelf;
    }

    public bool Equals(CheckersData other)
    {
        if(other.CheckerRank != CheckerRank)
        {
            return false;
        }

        if(other.CheckerSide != CheckerSide)
        {
            return false;
        }

        if(other.CurrentTurnIndex != CurrentTurnIndex)
        {
            return false;
        }

        if(other.IsDead != IsDead)
        {
            return false;
        }

        if(other.WorldPosition.x != WorldPosition.x || other.WorldPosition.y != WorldPosition.y)
        {
            return false;
        }

        return true;
    }
}
