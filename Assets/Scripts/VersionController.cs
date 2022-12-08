using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionController : MonoBehaviour
{
    public Commit CurrentCommit;

    public List<Branch> Branches => _branches;

    private List<Branch> _branches;
    private Branch _currentBranch;
    private Commit _lastCommit;
    private TurnController _turnController;

    private CheckersData[] _currentCheckersData;
    private int _currentTurnIndex;

    public void Awake()
    {
        _branches = new List<Branch>();
        CurrentCommit = null;
        _lastCommit = null;
        _currentBranch = new Branch("master");
        _branches.Add(_currentBranch);
        _turnController = FindObjectOfType<TurnController>();
    }

    public void Commit()
    {
        _currentCheckersData = GenerateCheckersData();
        _currentTurnIndex = _turnController.TurnIndex;
        _lastCommit = new Commit(_currentCheckersData, _currentTurnIndex);
        _currentBranch.AddCommit(_lastCommit);
        CurrentCommit = _lastCommit;
        //update ui
    }

    public void AddBranch(string branchName, Commit newBranchCommit)
    {
        CurrentCommit.NextCommits.Add(newBranchCommit);
        newBranchCommit.PreviosCommit = CurrentCommit;
        _lastCommit = newBranchCommit;

        _currentBranch = new Branch(branchName, _lastCommit);
        _branches.Add(_currentBranch);
    }

    public void SwitchBranch(Branch branch)
    {
        if(!_branches.Contains(branch))
        {
            return;
        }

        _currentBranch = branch;
        _lastCommit = _currentBranch.CurrentCommit;
        UpdateCheckersData(_lastCommit);
    }

    public void DeleteBranch(Branch branch)
    {
        if (!_branches.Contains(branch))
        {
            return;
        }

        _branches.Remove(branch);
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

    public void UpdateCheckersData(Commit commit)
    {
        // Args: Commit
        // This function calls when current branch was switched
        // Find all Checker objects and change their current data to the commit`s of the current branch data 
    }
}
