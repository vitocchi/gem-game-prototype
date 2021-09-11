using UnityEngine;

public enum Direction
{
    Left,
    Up,
    Right,
    Down
}


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerSpriteController _spriteController;
    [SerializeField]
    private FieldController _fieldController;
    [SerializeField]
    private float _moveTime;

    private (int, int) _position;

    private float _moveDelta;

    private bool _isMoving;

    private int _moveIndex;

    private Direction _direction;

    public void InitPosition((int, int) position)
    {
        _position = position;
        TransformGrid(position);
    }


    // Start is called before the first frame update
    void Start()
    {
        _direction = Direction.Down;
        _moveDelta = 0;
        _isMoving = false;
        _spriteController.SetIdle(_direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isMoving)
        {
            CheckPickupGem();
            CheckStartMoving();
        }
        else
        {
            Move();
            CheckFinishMoving();
        }
    }

    void CheckPickupGem()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _fieldController.GemExistsAt(_position))
        {
            _fieldController.PickupGemAt(_position);
        }
    }

    void CheckStartMoving()
    {
        (int, int) newPosition = _position;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _direction = Direction.Down;
            newPosition.Item2 -= 1;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            _direction = Direction.Up;
            newPosition.Item2 += 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _direction = Direction.Right;
            newPosition.Item1 += 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _direction = Direction.Left;
            newPosition.Item1 -= 1;
        }
        else
        {
            return;
        }

        if (_fieldController.TileAt(newPosition) == Tile.Wall)
        {
            _spriteController.SetIdle(_direction);
            return;
        }
        _isMoving = true;
        _moveIndex = (_moveIndex + 1) % 2;
        _moveDelta = 1;
        _position = newPosition;
    }

    void Move()
    {
        _moveDelta = Mathf.Clamp01(_moveDelta - Time.deltaTime / _moveTime);
        if (_moveDelta > 0.5f)
        {
            _spriteController.SetMove(_direction, _moveIndex);
        }
        else
        {
            _spriteController.SetIdle(_direction);
        }
        switch (_direction)
        {
            case Direction.Down:
                TransformGrid((_position.Item1, _position.Item2 + _moveDelta));
                break;
            case Direction.Up:
                TransformGrid((_position.Item1, _position.Item2 - _moveDelta));
                break;
            case Direction.Right:
                TransformGrid((_position.Item1 - _moveDelta, _position.Item2));
                break;
            case Direction.Left:
                TransformGrid((_position.Item1 + _moveDelta, _position.Item2));
                break;
        }
    }

    void CheckFinishMoving()
    {
        if (_moveDelta == 0)
        {
            _isMoving = false;
        }
    }

    void TransformGrid((float, float) grid)
    {
        this.transform.position = new Vector3(_fieldController.GridToWorld(grid.Item1), _fieldController.GridToWorld(grid.Item2), 0);
    }
}
