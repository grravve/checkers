using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch
{
    public readonly string Name;
    public Commit CurrentCommit => _currentCommit;

    private Commit _currentCommit;

    public Branch(string name)
    {
        Name = name;
        _currentCommit = null;
    }

    public Branch(string name, Commit lastCommit)
    {
        Name = name;
        _currentCommit = lastCommit;
    }

    public void AddCommit(Commit newCommit)
    {
        if(_currentCommit == null)
        {
            _currentCommit = newCommit;
        }

        newCommit.PreviosCommit = _currentCommit;
        _currentCommit?.NextCommits.Add(newCommit);
        _currentCommit = newCommit;
    }
}
