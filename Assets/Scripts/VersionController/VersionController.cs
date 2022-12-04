using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionController : MonoBehaviour
{
    private List<Branch> _branches;
    private Branch _currentBranch;
    private Commit _lastCommit;
    
    private TurnController _turnController;
    private CheckersData[] _currentCheckersData;
    private int _currentTurnIndex;

    private void Start()
    {
        _branches = new List<Branch>();
        _currentBranch = new Branch("master");
        _branches.Add(_currentBranch);
        _turnController = FindObjectOfType<TurnController>();
    }
    // Добавить ветку
    // Поменять ветку
    // Удалить ветку

    public void Commit()
    {
        _currentCheckersData = GenerateCheckersData();
        _currentTurnIndex = _turnController.TurnIndex;
        _lastCommit = new Commit(_currentBranch.CurrentCommit, _currentCheckersData, _currentTurnIndex);
        _currentBranch.AddCommit(_lastCommit);
        //update ui
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

    public void UpdateCheckersData()
    {
        // Args: Commit
        // This function calls when current branch was switched
        // Find all Checker objects and change their current data to the commit`s data 
    }
}
