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

    public void Move(Vector2 endPoint)
    {
        if(!CanMove())
        {
            return;
        }

        gameObject.transform.position = ConvertWorldToModelXY(endPoint) + new Vector2(_grid.CellSize, _grid.CellSize) * 0.5f;
    }

    public bool CanMove(Vector2 mouseWorldPosition)
    {
        /*
            Шашка может двигаться на пустую диагональную соседнюю клетку
         */
        List<Collider2D> possibleMoves = new List<Collider2D>();

        if (_side == Side.White)
        {
            possibleMoves = CheckOneCellMove(_grid.CellSize, _grid.CellSize);
        }

        if(_side == Side.Black)
        {
            possibleMoves = CheckOneCellMove(_grid.CellSize, -_grid.CellSize);
        }
        
        Debug.Log("Right: " + possibleMoves[0]);
        Debug.Log("Left: " + possibleMoves[1]);

        return true;
    }

    private List<Collider2D> CheckOneCellMove(int diagonalX, int diagonalY)
    {
        List<Collider2D> potentialMoves = new List<Collider2D>();

        Vector2 checkRightDirection = (Vector2)transform.position + new Vector2(diagonalX, diagonalY);
        Vector2 checkLeftDirection = (Vector2)transform.position + new Vector2(-diagonalX, diagonalY);

        if(checkRightDirection.x < _grid.Width && checkRightDirection.x > 0)
        {
            Collider2D rightCell = Physics2D.OverlapPoint(checkRightDirection);
            potentialMoves.Add(rightCell);
        }

        if (checkLeftDirection.x < _grid.Width && checkLeftDirection.x > 0)
        {
            Collider2D leftCell = Physics2D.OverlapPoint(checkLeftDirection);
            potentialMoves.Add(leftCell);
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
