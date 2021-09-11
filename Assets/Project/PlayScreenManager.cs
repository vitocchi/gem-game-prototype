using System.Collections;
using UnityEngine;

public class PlayScreenManager : MonoBehaviour
{
    [SerializeField]
    private GemViewerController _gemViewerPrefab;

    private UserData _userData;

    public void Init(UserData userData)
    {
        _userData = userData;
    }

    [ContextMenu("MineGem")]
    [System.Obsolete]
    public void MineGem()
    {
        StartCoroutine(MineGemCoroutine());
    }

    [System.Obsolete]
    public IEnumerator MineGemCoroutine()
    {
        Request<GemResponse> request = OffchainClient.CreateGem(_userData.Address);
        yield return StartCoroutine(request.RequestCoroutine());
        GemParameter parameter = request.Response().GemParameter();
        GemViewerController gemViewerController = Instantiate<GemViewerController>(_gemViewerPrefab);
        gemViewerController.Init(parameter);
    }
}
