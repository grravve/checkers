using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private int cellSize;
    GameObject[,] gridSquareObjects;
    private int[,] gridPattern =
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



    public Grid(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridSquareObjects = new GameObject[width, height];

        Texture2D texture = Resources.Load<Texture2D>("Square");
        
        Debug.Log($"grid {width} by {height} was created");

        for(int x = 0; x < gridSquareObjects.GetLength(0); x++)
        {
            for(int y = 0; y < gridSquareObjects.GetLength(1); y++)
            {
                if (gridPattern[x, y] != 1)
                {
                    continue;
                }

                gridSquareObjects[x, y] = new GameObject();
                gridSquareObjects[x, y].transform.position = GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f;
                gridSquareObjects[x, y].transform.localScale = new Vector3(.4f, .4f);

                SpriteRenderer squareSR = gridSquareObjects[x, y].AddComponent<SpriteRenderer>();
                squareSR.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                squareSR.color = Color.red;
                squareSR.sortingLayerName = "Testing";
                squareSR.sortingOrder = 1;


                Debug.Log(texture);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(height, width), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y) * cellSize;

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }
    
    public void SetValue(int x, int y, Color value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridSquareObjects[x, y].GetComponent<SpriteRenderer>().color = value;
        }
    }

    public void SetValue(Vector3 worldPosition, Color value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }



    
}
