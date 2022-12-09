using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commit: IEquatable<Commit>
{
    public Commit PreviosCommit;
    public List<Commit> NextCommits;
    public string Id;

    public CheckersData[] CheckersData;
    public int CurrentTurnIndex; 

    public Commit(CheckersData[] checkersData, int currentTurnIndex)
    {
        Id = Guid.NewGuid().ToString("N");
        NextCommits = new List<Commit>();
        CheckersData = checkersData;
        CurrentTurnIndex = currentTurnIndex;
    }

    public bool Equals(Commit other)
    {
        if (other.CheckersData.Equals(CheckersData) == false)
        {
            return false;
        }

        if(other.CurrentTurnIndex != CurrentTurnIndex)
        {
            return false;
        }

        return true;
    }
}
