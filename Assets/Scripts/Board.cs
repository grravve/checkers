using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private const string BlackSpawnPointTagName = "BlackSpawnPoint";
    private const string WhiteSpawnPointTagName = "WhiteSpawnPoint";

    [SerializeField] private GameObject _blackCheckerPrefab;
    [SerializeField] private GameObject _whiteCheckerPrefab;

    private Grid _grid;

    void Start()
    {
        _grid = new Grid(8, 8, 1);

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

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Выделяем выбранную область через Grid
            Vector3 mouseWorldPosition = GetMouseWorldPosition(Input.mousePosition, Camera.main);
            _grid.SetValue(mouseWorldPosition);
            
            // Получаем компонент шашки   
        }
    }

    private Vector3 GetMouseWorldPosition(Vector3 clickPosition, Camera camera)
    {
        return camera.ScreenToWorldPoint(clickPosition);
    }
}
