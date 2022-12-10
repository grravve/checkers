using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commit: IEquatable<Commit>
{
    public Commit PreviousCommit = null;
    public List<Commit> NextCommits;
    public string Id;

    public CheckersData[] CheckersData;
    public int CurrentTurnIndex; 

    public Commit(CheckersData[] checkersData, int currentTurnIndex)
    {
        Id = Guid.NewGuid().ToString();
        NextCommits = new List<Commit>();
        CheckersData = checkersData;
        CurrentTurnIndex = currentTurnIndex;
    }

    public bool Equals(Commit other)
    {
        for(int i = 0; i < CheckersData.Length; i++)
        {
            if (other.CheckersData[i].Equals(CheckersData[i]) == false)
            {
                return false;
            }
        }

        if(other.CurrentTurnIndex != CurrentTurnIndex)
        {
            return false;
        }

        return true;
    }
}
