using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Grid grid;
    private int[,] checkersPattern =
    {
        {0, 1, 0, 1, 0, 1, 0, 1},
        {1, 0, 1, 0, 1, 0, 1, 0},
        {0, 1, 0, 1, 0, 1, 0, 1},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {1, 0, 1, 0, 1, 0, 1, 0},
        {0, 1, 0, 1, 0, 1, 0, 1},
        {1, 0, 1, 0, 1, 0, 1, 0}
    };

    void Start()
    {
        grid = new Grid(8, 8, 1);

        // Set checkers to the board
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Выделяем выбранную область через Grid
            Vector3 mouseWorldPosition = GetMouseWorldPosition(Input.mousePosition, Camera.main);
            grid.SetValue(mouseWorldPosition);
            
            // Получаем компонент шашки   
        }
    }

    private Vector3 GetMouseWorldPosition(Vector3 clickPosition, Camera camera)
    {
        return camera.ScreenToWorldPoint(clickPosition);
    }

}
