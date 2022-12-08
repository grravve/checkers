using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commit
{
    public Commit PreviosCommit;
    public List<Commit> NextCommits;
    public string Id;

    public CheckersData[] CheckersData;
    public int CurrentTurnIndex; 

    public Commit(CheckersData[] checkersData, int currentTurnIndex)
    {
        Id = Guid.NewGuid().ToString("N");
        CheckersData = checkersData;
        CurrentTurnIndex = currentTurnIndex;
        NextCommits = new List<Commit>();
    }
}
