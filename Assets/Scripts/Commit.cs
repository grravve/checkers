using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commit
{
    public Commit PreviosCommit;
    public List<Commit> NextCommits;
    public CheckersData[] CheckersData;
    public int CurrentTurnIndex; 

    public Commit(CheckersData[] checkersData, int currentTurnIndex)
    {
        CheckersData = checkersData;
        CurrentTurnIndex = currentTurnIndex;
        NextCommits = new List<Commit>();
    }
}
