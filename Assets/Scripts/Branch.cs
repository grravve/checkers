using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public readonly string Name;
    
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
