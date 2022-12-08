using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _branchList;
    [SerializeField] private Transform _commitList;
    [SerializeField] private GameObject _branchesPage;
    [SerializeField] private GameObject _commitsPage;
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

            branchButton.GetComponentInChildren<Text>().text = _versionController.Branches[i].Name;
            branchButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                // Switch branch
                int index = i - 1;
                Debug.Log(index);
                _versionController.SwitchBranch(_versionController.Branches[index]);

                _branchesPage.SetActive(false);
                _commitsPage.SetActive(true); ;

                UpdateCommitsList(_versionController.Branches[index]);
            });
        }
    }

    private void UpdateCommitsList(Branch branch)
    {
        _commitsPage.GetComponentInChildren<Text>().text = branch.Name;
        
        if (branch.CurrentCommit == null)
        {
            return;
        }

        // Show current commits in this branch

        foreach (Transform commitButtonObj in _commitList)
        {
            Destroy(commitButtonObj.gameObject);
        }
        
        for(int i = 0; i < _versionController.CurrentCommit.NextCommits.Count; i++)
        {
            var commitButton = Instantiate(_listItemPrefab);
            commitButton.transform.SetParent(_commitList, false);

            commitButton.GetComponentInChildren<Text>().text = _versionController.CurrentCommit.NextCommits[i].Id;
            commitButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Commit selectedCommit = _versionController.CurrentCommit.NextCommits[i];
                _versionController.UpdateCheckersData(selectedCommit);
            });
        }
        // Event when current commit in list or current branch was changed, update data on scene
    }


}
