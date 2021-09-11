using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private StartScreenManager _startScreenPrefab;

    [SerializeField]
    private PlayScreenManager _playScreenManager;

    private GameObject _screen;

    void Start()
    {
        StartScreenManager screen = Instantiate<StartScreenManager>(_startScreenPrefab);
        screen.Init(this);
        _screen = screen.gameObject;
    }

    public void OnSignInned(UserData userData)
    {
        Destroy(_screen);
        PlayScreenManager screen = Instantiate<PlayScreenManager>(_playScreenManager);
        screen.Init(userData);
        _screen = screen.gameObject;
    }
}
