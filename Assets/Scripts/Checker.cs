using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { White = 0, Black = 1}
public enum Rank { Usual = 0, Queen = 1}

public class Checker : MonoBehaviour
{
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
        _visual = GetComponent<SpriteRenderer>();
        _visual.sprite = _checkerModel.usualSprite;
    }

    public void Move()
    {

    }

    public void CanMove()
    {
        
    }

    public void UpToQueen()
    {
        _rank = Rank.Queen;
        _visual.sprite = _checkerModel.queenSprite;
    }
}
