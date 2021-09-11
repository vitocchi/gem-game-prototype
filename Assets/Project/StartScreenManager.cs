using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        this._gameManager = gameManager;
    }

    public void OnSignInned()
    {
        this._gameManager.OnSignInned(new UserData());
    }
}
