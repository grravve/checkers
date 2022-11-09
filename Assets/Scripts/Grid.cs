using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private int cellSize;
    private GameObject[,] gridSquareObjects;
    private Texture2D texture;
    private GameObject activeCell;

    public Grid(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridSquareObjects = new GameObject[width, height];

        texture = Resources.Load<Texture2D>("ActiveBorder");

        Debug.Log($"grid {width} by {height} was created");

        GridRender();
    }

    private void GridRender()
    {
        for (int x = 0; x < gridSquareObjects.GetLength(0); x++)
        {
            for (int y = 0; y < gridSquareObjects.GetLength(1); y++)
            {
                gridSquareObjects[x, y] = new GameObject();
                gridSquareObjects[x, y].transform.position = GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f;
                gridSquareObjects[x, y].transform.localScale = new Vector3(.75f, .75f);

                SpriteRenderer squareSR = gridSquareObjects[x, y].AddComponent<SpriteRenderer>();
                squareSR.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                squareSR.color = new Color(1, 1, 1, 0);
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
    
    public void SetValue(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            Color tmpColor;
            if (activeCell != null)
            {
                tmpColor = activeCell.GetComponent<SpriteRenderer>().color;
                tmpColor.a = 0;
                activeCell.GetComponent<SpriteRenderer>().color = tmpColor;
            }

            activeCell = gridSquareObjects[x, y];
            tmpColor = activeCell.GetComponent<SpriteRenderer>().color;
            tmpColor.a = 1;
            activeCell.GetComponent<SpriteRenderer>().color = tmpColor;
        }
    }

    public void SetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y);
    }
}
