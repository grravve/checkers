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
    }

    public void AddCommit(Commit newCommit)
    {
        _currentCommit = newCommit;
    }
}
