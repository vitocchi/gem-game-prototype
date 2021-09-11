using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _rend;

    [SerializeField]
    private Sprite _idleDown;
    [SerializeField]
    private Sprite[] _moveDowns;
    [SerializeField]
    private Sprite _idleUp;
    [SerializeField]
    private Sprite[] _moveUps;
    [SerializeField]
    private Sprite _idleRight;
    [SerializeField]
    private Sprite[] _moveRights;
    [SerializeField]
    private Sprite _idleLeft;
    [SerializeField]
    private Sprite[] _moveLefts;


    public void SetIdle(Direction d)
    {
        switch (d)
        {
            case Direction.Down:
                Set(_idleDown);
                break;
            case Direction.Left:
                Set(_idleLeft);
                break;
            case Direction.Up:
                Set(_idleUp);
                break;
            case Direction.Right:
                Set(_idleRight);
                break;
            default: throw new System.ArgumentException("Invalid Direction");
        }
    }


    public void SetMove(Direction d, int index)
    {
        switch (d)
        {
            case Direction.Down:
                Set(_moveDowns[index]);
                break;
            case Direction.Left:
                Set(_moveLefts[index]);
                break;
            case Direction.Up:
                Set(_moveUps[index]);
                break;
            case Direction.Right:
                Set(_moveRights[index]);
                break;
            default: throw new System.ArgumentException("Invalid Direction");
        }
    }

    private void Set(Sprite _sprite)
    {
        if (this._rend.sprite != _sprite)
            this._rend.sprite = _sprite;
    }
}