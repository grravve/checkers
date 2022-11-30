using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commit : MonoBehaviour
{
    public Commit LastCommit;
    public CheckersData[] CheckersData;
    public int CurrentTurnIndex; 

    public Commit(CheckersData[] checkersData, int currentTurnIndex)
    {
        CheckersData = checkersData;
        CurrentTurnIndex = currentTurnIndex;
    }
}
