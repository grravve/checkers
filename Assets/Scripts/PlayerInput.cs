using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private Board _board;
	[SerializeField] private Side _side;
	[SerializeField] private TurnController _turnController;
		
	private Grid _grid;
	private Checker _selectedChecker;
    private List<Vector2> possibleMoves;

    private void Start()
	{
        _grid = _board.Grid;
        possibleMoves = new List<Vector2>();
       // Debug.Log(gameObject);
    }

	public void HandleInput()
	{
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 _mouseWorldPosition = GetMouseWorldPosition(Input.mousePosition, Camera.main);

            if(_mouseWorldPosition.x > _grid.Width || _mouseWorldPosition.x < 0 
                || _mouseWorldPosition.y > _grid.Height || _mouseWorldPosition.y < 0)
            {
                return;
            }

            _grid.SetValue(_mouseWorldPosition);
            Collider2D selectedCollider = Physics2D.OverlapPoint(_mouseWorldPosition);
           

            if (selectedCollider == null && _selectedChecker == null)
            {
                return;
            }

            if (selectedCollider == null && _selectedChecker != null)
            {
                bool isMoved = _selectedChecker.Move(_mouseWorldPosition, possibleMoves);
                
                if(isMoved)
                {
                    _selectedChecker = null;
                    _turnController.SwitchTurn();
                }
                
                return;
            }

            if (selectedCollider.TryGetComponent(out Checker checker) && checker.Side == _side)
            {
                _selectedChecker = checker;
                checker.CanMove(out possibleMoves);
                return;
            }
        }
    }

    private Vector3 GetMouseWorldPosition(Vector3 clickPosition, Camera camera)
    {
        return camera.ScreenToWorldPoint(clickPosition);
    }
}
