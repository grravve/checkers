using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private List<PlayerInput> _turnOrder;

    public int TurnIndex => _currentTurnIndex;

    private int _currentTurnIndex = 0;

    private void Update()
    {
        _turnOrder[_currentTurnIndex % _turnOrder.Count].HandleInput();
    }

    public void SwitchTurn()
    {
        _currentTurnIndex++;
    }

    public void ChangeCurrentTurn(int turnIndex)
    {
        _currentTurnIndex = turnIndex;
    }
}
