using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenController : MonoBehaviour
{
    [SerializeField] private Side _side;

    private BoxCollider2D _queenBorderCollider;

    private void Start()
    {
        _queenBorderCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Checker inputChecker = other.GetComponent<Checker>();

        if (inputChecker.Side != _side)
        {
            inputChecker.UpToQueen();
        }
    }

}
