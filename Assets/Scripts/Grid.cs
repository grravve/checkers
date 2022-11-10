using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid
{
    private const string BlackSpawnPointTagName = "BlackSpawnPoint";
    private const string WhiteSpawnPointTagName = "WhiteSpawnPoint";

    private int _width;
    private int _height;
    private int _cellSize;

    private GameObject[,] _gridSquareObjects;
    private GameObject _activeCell;
    
    private Texture2D _texture;

    private int[,] _checkersPattern =
    {
        {0, 2, 0, 2, 0, 2, 0, 2},
        {2, 0, 2, 0, 2, 0, 2, 0},
        {0, 2, 0, 2, 0, 2, 0, 2},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {1, 0, 1, 0, 1, 0, 1, 0},
        {0, 1, 0, 1, 0, 1, 0, 1},
        {1, 0, 1, 0, 1, 0, 1, 0}
    };

    public Grid(int width, int height, int cellSize)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;

        _gridSquareObjects = new GameObject[width, height];

        _texture = Resources.Load<Texture2D>("ActiveBorder");

        GridRender();
    }

    private void GridRender()
    {
        for (int x = 0; x < _gridSquareObjects.GetLength(0); x++)
        {
            for (int y = 0; y < _gridSquareObjects.GetLength(1); y++)
            {
                _gridSquareObjects[x, y] = new GameObject();
                _gridSquareObjects[x, y].transform.position = GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * 0.5f;
                _gridSquareObjects[x, y].transform.localScale = new Vector3(.75f, .75f);

                if (_checkersPattern[x, y] == 1)
                {
                    _gridSquareObjects[x, y].tag = WhiteSpawnPointTagName;
                }
                else if (_checkersPattern[x, y] == 2) 
                {
                    _gridSquareObjects[x, y].tag = BlackSpawnPointTagName;
                }
                
                SpriteRenderer squareSR = _gridSquareObjects[x, y].AddComponent<SpriteRenderer>();
                squareSR.sprite = Sprite.Create(_texture, new Rect(0.0f, 0.0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                squareSR.color = new Color(1, 1, 1, 0);
                squareSR.sortingLayerName = "Testing";
                squareSR.sortingOrder = 1;

                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_height, _width), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y) * _cellSize;

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        y = Mathf.FloorToInt(worldPosition.y / _cellSize);
    }
    
    public void SetValue(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < _width && y < _height)
        {
            Color tmpColor;

            if (_activeCell != null)
            {
                tmpColor = _activeCell.GetComponent<SpriteRenderer>().color;
                tmpColor.a = 0;
                _activeCell.GetComponent<SpriteRenderer>().color = tmpColor;
            }

            _activeCell = _gridSquareObjects[x, y];
            tmpColor = _activeCell.GetComponent<SpriteRenderer>().color;
            tmpColor.a = 1;
            _activeCell.GetComponent<SpriteRenderer>().color = tmpColor;
        }
    }

    public void SetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y);
    }

    public GameObject[,] GetSquareObjects() => _gridSquareObjects;
}
