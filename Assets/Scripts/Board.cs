using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private const string BlackSpawnPointTagName = "BlackSpawnPoint";
    private const string WhiteSpawnPointTagName = "WhiteSpawnPoint";

    public Grid Grid => _grid;

    [SerializeField] private GameObject _blackCheckerPrefab;
    [SerializeField] private GameObject _whiteCheckerPrefab;

    private Grid _grid;
    private int _gridHeight = 8;
    private int _gridWidth = 8;

    private void Awake()
    {
        _grid = new Grid(_gridWidth, _gridHeight, 1);
    }

    private void Start()
    {
        // Set checkers to the board

        SpawnCheckersOfSide(BlackSpawnPointTagName, _blackCheckerPrefab);
        SpawnCheckersOfSide(WhiteSpawnPointTagName, _whiteCheckerPrefab);
    }

    private void SpawnCheckersOfSide(string tag, GameObject prefab)
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(tag);

        foreach (var spawnPoint in spawnPoints)
        {
            var go = Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
            go.GetComponent<Checker>().Initialize(_grid);
        }
    }
}
