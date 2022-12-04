using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionController : MonoBehaviour
{
    private List<Branch> _branches;
    private Branch _currentBranch;
    private Commit _lastCommit;

    private CheckersData[] _currentCheckersData;
    private int _currentTurnIndex;

    public void Awake()
    {
        _branches = new List<Branch>();
        _currentBranch = new Branch("master");
        _branches.Add(_currentBranch);
    }
    // Добавить ветку
    // Поменять ветку
    // Удалить ветку

    public void Commit()
    {
        _currentCheckersData = GenerateCheckersData();
        _currentTurnIndex = GetComponent<TurnController>().TurnIndex;
    }

    private CheckersData[] GenerateCheckersData()
    {
        Checker[] checkers = FindObjectsOfType<Checker>();
        CheckersData[] resultArr = new CheckersData[checkers.Length];

        for (int i = 0; i < checkers.Length; i++)
        {
            resultArr[i] = new CheckersData(checkers[i]);
        }

        return resultArr;
    }
}
