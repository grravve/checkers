using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _branchList;
    [SerializeField] private Transform _commitList;
    [SerializeField] private GameObject _listItemPrefab;
    
    private VersionController _versionController;

    private void Start()
    {
        _versionController = FindObjectOfType<VersionController>();
        UpdateBranchList();
    }

    private void UpdateBranchList()
    {
        foreach(Transform branchButtonObj in _branchList)
        {
            Destroy(branchButtonObj.gameObject);
        }

        for (int i = 0; i < _versionController.Branches.Count; i++)
        {
            var branchButton = Instantiate(_listItemPrefab);
            branchButton.transform.SetParent(_branchList, false);

            int index = i;

            branchButton.GetComponentInChildren<Text>().text = _versionController.Branches[i].Name;
            branchButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                // Switch branch
                _versionController.SwitchBranch(_versionController.Branches[index]);
                UpdateCommitsList();
            });
        }
    }

    private void UpdateCommitsList()
    {
        // Show current commits in this branch

        foreach (Transform commitButtonObj in _commitList)
        {
            Destroy(commitButtonObj.gameObject);
        }

        for(int i = 0; i < _versionController.CurrentCommit.NextCommits.Count; i++)
        {
            var commitButton = Instantiate(_listItemPrefab);
            commitButton.transform.SetParent(_commitList, false);

            int index = i;
            commitButton.GetComponentInChildren<Text>().text = _versionController.CurrentCommit.NextCommits[i].Id;
            commitButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Commit selectedCommit = _versionController.CurrentCommit.NextCommits[index];
                _versionController.LoadCommit(selectedCommit);
                UpdateCommitsList();
            });
        }
        // Event when current commit in list or current branch was changed, update data on scene
    }

    public void PressCommitButton()
    {
        _versionController.Commit();

        UpdateBranchList();
        UpdateCommitsList();
    }

    public void PressPreviousButton() 
    {
        if(_versionController.CurrentCommit == null || _versionController.CurrentCommit.PreviousCommit == null)
        {
            return;
        }

        _versionController.LoadPreviousCommit();
        UpdateCommitsList();
    }

    public void PressLastCommitButton()
    {
        _versionController.LoadLastCommit();
        UpdateCommitsList();
    }

    public void PressDeleteBranchButton()
    {
        _versionController.DeleteCurrentBranch();
        UpdateBranchList();
        UpdateCommitsList();
    }
}
