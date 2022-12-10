using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Black = -1, White = 1}
public enum Rank { Usual = 0, Queen = 1}

public delegate bool CheckMoveDelegate(Vector2 targetPosition, out bool killedEnemy);

public class Checker : MonoBehaviour
{
    public Side Side => _side;
    public Rank Rank => _rank;
    public CheckMoveDelegate CheckMove;

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

        CheckMove = CanMoveUsual;
    }

    public void Move(Vector2 clickPoint)
    {
       gameObject.transform.position = ConvertWorldToModelXY(clickPoint) + new Vector2(_grid.CellSize, _grid.CellSize) * 0.5f;
    }

    private bool CanMoveUsual(Vector2 clickPoint, out bool killedEnemy)
    {
        killedEnemy = false;
        Vector3 clickedCellCenter = ConvertWorldToModelXY(clickPoint) + new Vector2(_grid.CellSize, _grid.CellSize) * 0.5f;
        Vector2 direction = clickedCellCenter - transform.position;
        
        if(Physics2D.OverlapPoint(clickedCellCenter, LayerMask.GetMask("Default")) != null)
        {
            return false;
        }

        double distanceToClick = Math.Round(Vector2.Distance(transform.position, clickedCellCenter), 4);
        double diagonal = Math.Round(Math.Sqrt(Math.Pow(_grid.CellSize, 2) + Math.Pow(_grid.CellSize, 2)), 4);

        direction = direction.normalized * (int)_side;
        if (direction.y < 0 && distanceToClick == diagonal)
        {
            return false;
        }

        if (distanceToClick > diagonal * 2)
        {
            return false;
        }
        
        if(distanceToClick == diagonal)
        {
            return true;
        }

        if(distanceToClick != diagonal * 2)
        {
            return false;
        }

        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, clickedCellCenter, LayerMask.GetMask("Default"));

        foreach(var hit in hits)
        {
            if(hit.collider.gameObject == gameObject)
            {
                continue;
            }

            if(hit.collider.gameObject.tag == gameObject.tag)
            {
                return false;
            }

            if (hit.collider.gameObject.tag != gameObject.tag)
            {
                hit.collider.gameObject.SetActive(false);
                killedEnemy = true;
                return true;
            }
        }

        return false;
    }

    private bool CanMoveQueen(Vector2 clickPoint, out bool killedEnemy) 
    {
        killedEnemy = false;

        Vector3 clickedCellCenter = ConvertWorldToModelXY(clickPoint) + new Vector2(_grid.CellSize, _grid.CellSize) * 0.5f;
        Vector2 direction = clickedCellCenter - transform.position;

        if (Physics2D.OverlapPoint(clickedCellCenter, LayerMask.GetMask("Default")) != null)
        {
            return false;
        }

        double distanceToClick = Math.Round(Vector2.Distance(transform.position, clickedCellCenter), 4);
        double diagonal = Math.Round(Math.Sqrt(Math.Pow(_grid.CellSize, 2) + Math.Pow(_grid.CellSize, 2)), 4);

        direction = direction.normalized * (int)_side;

        Debug.Log($"Distance: {distanceToClick}\nDiagonal: {diagonal}\nDirection: {direction}");

        if (distanceToClick == diagonal)
        {
            return true;
        }

        /*if (direction.y < 0 && distanceToClick > diagonal)
        {
            return false;
        }*/

        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, clickedCellCenter, LayerMask.GetMask("Default"));
        
        if(hits.Length > 2)
        {
            return false;
        }

        foreach(var hit in hits)
        {
            if(hit.collider.gameObject == gameObject)
            {
                continue;
            }

            if (hit.collider.gameObject.tag == gameObject.tag)
            {
                return false;
            }

            if (hit.collider.gameObject.tag != gameObject.tag)
            {
                hit.collider.gameObject.SetActive(false);
                killedEnemy = true;
                return true;
            }
        }

        if(distanceToClick > diagonal)
        {
            Debug.Log("Far cell");
            return true;
        }

        return false;
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
        CheckMove = CanMoveQueen;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void SetSide(Side side)
    {
        _side = side;
    }

    public void SetRank(Rank rank)
    {
        _rank = rank;

        if (_rank == Rank.Queen)
        {
            _visual.sprite = _checkerModel.queenSprite;
            CheckMove = CanMoveQueen;
            return;
        }

        _visual.sprite = _checkerModel.usualSprite;
        CheckMove = CanMoveUsual;
    }
}
