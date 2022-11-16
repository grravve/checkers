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

    public bool CanMove()
    {
        return true;
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
