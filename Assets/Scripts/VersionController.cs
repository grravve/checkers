using System;
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
        CurrentCommit = null;
        _lastCommit = null;
        _currentBranch = new Branch("master");
        _branches = new List<Branch>();
        _branches.Add(_currentBranch);
        _turnController = FindObjectOfType<TurnController>();
    }

    public void Commit()
    {
        _currentCheckersData = GenerateCheckersData();
        _currentTurnIndex = _turnController.TurnIndex;

        Commit newCommit = new Commit(_currentCheckersData, _currentTurnIndex);

        if(CurrentCommit == null)
        {
            _currentBranch.AddCommit(newCommit);
            _lastCommit = _currentBranch.CurrentCommit;
            CurrentCommit = _lastCommit;
            return;
        }

        if (newCommit.Equals(CurrentCommit) == true)
        {
            return;
        }

        if(CurrentCommit.NextCommits.Count == 0)
        {
            _currentBranch.AddCommit(newCommit);
            _lastCommit = _currentBranch.CurrentCommit;
            CurrentCommit = _lastCommit;
            return;
        }

        foreach(Commit checkCommit in CurrentCommit.NextCommits)
        {
            if (newCommit.Equals(checkCommit) == true)
            {
                return;
            }
        }

        AddBranch(Guid.NewGuid().ToString(), newCommit);
    }

    public void AddBranch(string branchName, Commit newBranchCommit)
    {
        CurrentCommit.NextCommits.Add(newBranchCommit);
        newBranchCommit.PreviousCommit = CurrentCommit;
        _lastCommit = newBranchCommit;

        _currentBranch = new Branch(branchName, _lastCommit);
        _branches.Add(_currentBranch);
        CurrentCommit = _currentBranch.CurrentCommit;
    }

    public void SwitchBranch(Branch branch)
    {
        if(!_branches.Contains(branch))
        {
            return;
        }

        _currentBranch = branch;
        _lastCommit = _currentBranch.CurrentCommit;
        CurrentCommit = _lastCommit;
        UpdateCheckersData(CurrentCommit);
    }

    public void DeleteCurrentBranch()
    {
        if (_branches.Count == 1)
        {
            return;
        }

        _branches.Remove(_currentBranch);
        _currentBranch = _branches[_branches.Count - 1];
        CurrentCommit = _currentBranch.CurrentCommit;
       
        UpdateCheckersData(CurrentCommit);
    }

    public void LoadCommit(Commit commit)
    {
        CurrentCommit = commit;
        UpdateCheckersData(commit);
    }

    public void LoadPreviousCommit()
    {
        Commit previousCommit = CurrentCommit?.PreviousCommit;
        CurrentCommit = previousCommit;

        UpdateCheckersData(CurrentCommit);
    }

    public void LoadLastCommit()
    {
        CurrentCommit = _lastCommit;
        UpdateCheckersData(CurrentCommit);
    }

    private CheckersData[] GenerateCheckersData()
    {
        Checker[] checkers = FindObjectsOfType<Checker>();
        CheckersData[] resultArr = new CheckersData[checkers.Length];

        for (int i = 0; i < checkers.Length; i++)
        {
            resultArr[i] = new CheckersData(checkers[i], _turnController.TurnIndex);
        }

        return resultArr;
    }

    public void UpdateCheckersData(Commit commit)
    {
        Checker[] checkers = Resources.FindObjectsOfTypeAll<Checker>();

        for (int i = 0; i < commit.CheckersData.Length; i++)
        {
            checkers[i].gameObject.SetActive(commit.CheckersData[i].IsDead);
            checkers[i].gameObject.transform.position = commit.CheckersData[i].WorldPosition;
            checkers[i].SetSide(commit.CheckersData[i].CheckerSide);
            checkers[i].SetRank(commit.CheckersData[i].CheckerRank);
            _turnController.ChangeCurrentTurn(commit.CheckersData[i].CurrentTurnIndex);
        }
    }
}
