using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GemViewerController _gemViewerController;


    [ContextMenu("MineGem")]
    [System.Obsolete]
    public void MineGem()
    {
        StartCoroutine(MineGemCoroutine());
    }

    [System.Obsolete]
    public IEnumerator MineGemCoroutine()
    {
        Request<GemResponse> request = OffchainClient.CreateGem(new EthereumAddress("0x0CE599133FE3619e84F38BeCF1e68A40f1a8B294"));
        yield return StartCoroutine(request.RequestCoroutine());
        GemParameter parameter = request.Response().GemParameter();
        _gemViewerController.ResetGem(parameter);
        _gemViewerController.SetEnableViewer();
    }
}
