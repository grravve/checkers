using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _branchList;
    [SerializeField] private Transform _commitList;
    [SerializeField] private GameObject _listItemPrefab;
    [SerializeField] private VersionController _versionController;

    private void Start()
    {
        UpdateBranchList();
    }

    private void UpdateBranchList()
    {
        foreach(Transform branchObj in _branchList)
        {
            Destroy(branchObj.gameObject);
        }

        for (int i = 0; i < _versionController.Branches.Count; i++)
        {
            var branchButton = Instantiate(_listItemPrefab);
            branchButton.transform.SetParent(_branchList, false);

            branchButton.GetComponentInChildren<Text>().text = _versionController.Branches[i].Name;
            branchButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                // Switch branch
                _versionController.SwitchBranch(_versionController.Branches[i]);

                _branchList.gameObject.SetActive(false);
                _commitList.gameObject.SetActive(true);

                UpdateCommitsList(_versionController.Branches[i]);

                Debug.Log("Switch to the commit page");
            });
        }
    }

    private void UpdateCommitsList(Branch branch)
    {
        // Update list name 
        // Show current commits in this branch
        // Update world data in the scene (last commit of the branch)
    }

    // Event when current commit in list or current branch was changed, update data on scene

}
