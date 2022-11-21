using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { White = 0, Black = 1}
public enum Rank { Usual = 0, Queen = 1}

public class Checker : MonoBehaviour
{
    public Side Side => _side;

    private Side _side;
    private Rank _rank;
    private Grid _grid;
    private SpriteRenderer _visual;

    [SerializeField] private CheckerModel _checkerModel;

    public void Initialize(Grid grid)
    {
        _grid = grid;
    }

    private void Awake()
    {
        _checkerModel = Instantiate(_checkerModel);
        _side = _checkerModel.side;
        _rank = Rank.Usual;
        _visual = GetComponent<SpriteRenderer>();
        _visual.sprite = _checkerModel.usualSprite;
    }

    public bool Move(Vector2 endPoint, List<Vector2> possibleMoves)
    {
        Vector2 movePointModel = ConvertWorldToModelXY(endPoint);
        
        for(int i = 0; i < possibleMoves.Count; i++)
        {
            if (ConvertWorldToModelXY(possibleMoves[i]) == movePointModel)
            {
                gameObject.transform.position = movePointModel + new Vector2(_grid.CellSize, _grid.CellSize) * 0.5f;
                return true;
            }
        }

        return false;
    }

    public void CanMove(out List<Vector2> resultList)
    {
        /*
            Шашка может двигаться на пустую диагональную соседнюю клетку
         */
        List<Vector2> possibleMoves = new List<Vector2>();

        if (_side == Side.White)
        {
            possibleMoves = CheckOneCellMove(_grid.CellSize, _grid.CellSize);
        }

        if(_side == Side.Black)
        {
            possibleMoves = CheckOneCellMove(_grid.CellSize, -_grid.CellSize);
        }
        
        foreach(Vector2 value in possibleMoves)
        {
            Debug.Log(value);
        }

        resultList = possibleMoves;
    }

    private List<Vector2> CheckOneCellMove(int diagonalX, int diagonalY)
    {
        List<Vector2> potentialMoves = new List<Vector2>();

        Vector2 checkRightDirection = (Vector2)transform.position + new Vector2(diagonalX, diagonalY);
        Vector2 checkLeftDirection = (Vector2)transform.position + new Vector2(-diagonalX, diagonalY);

        if(checkRightDirection.x < _grid.Width)
        {
            Collider2D rightCell = Physics2D.OverlapPoint(checkRightDirection);
            if(rightCell == null)
            {
                potentialMoves.Add(checkRightDirection);
            }
        }

        if (checkLeftDirection.x > 0)
        {
            Collider2D leftCell = Physics2D.OverlapPoint(checkLeftDirection);
            if (leftCell == null)
            {
                potentialMoves.Add(checkLeftDirection);
            }
        }
        return potentialMoves;
    }    

    private Vector2 ConvertWorldToModelXY(Vector2 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / _grid.CellSize);
        int y = Mathf.FloorToInt(worldPosition.y / _grid.CellSize);
        return new Vector2(x, y);
    }

    public void UpToQueen()
    {
        _rank = Rank.Queen;
        _visual.sprite = _checkerModel.queenSprite;
    }
}
