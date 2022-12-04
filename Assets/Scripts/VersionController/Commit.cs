using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commit
{
    public Commit LastCommit;
    public CheckersData[] CheckersData;
    public int CurrentTurnIndex; 

    public Commit(Commit lastCommit, CheckersData[] checkersData, int currentTurnIndex)
    {
        LastCommit = lastCommit;
        CheckersData = checkersData;
        CurrentTurnIndex = currentTurnIndex;
    }
}
