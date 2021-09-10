using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OffchainClient
{
    //接続するURL
    private const string BASE_URL = "http://localhost:3000";

    public static Request<GemResponse> GetGem(int id)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(BASE_URL + "/gem/" + id.ToString());
        return new Request<GemResponse>(webRequest);
    }

    public static Request<GemResponse> CreateGem(EthereumAddress address)
    {
        UnityWebRequest webRequest = UnityWebRequest.Post(BASE_URL + "/user/" + address.Value + "/offchain-gems", "");
        return new Request<GemResponse>(webRequest);
    }
}

public class Request<T>
{
    private readonly UnityWebRequest _request;
    private T _response;

    public Request(UnityWebRequest request)
    {
        _request = request;
    }

    [System.Obsolete]
    public IEnumerator RequestCoroutine()
    {
        yield return _request.SendWebRequest();

        if (_request.isNetworkError)
        {
            Debug.Log(_request.error);
        }
        else
        {
            Debug.Log(_request.downloadHandler.text);
            _response = JsonUtility.FromJson<T>(_request.downloadHandler.text);
        }
    }

    public T Response()
    {
        if (_response == null)
        {
            throw new System.Exception("request not finished");
        }
        return _response;
    }
}

public class GemResponse
{
    public int id;
    public float colorH;
    public float colorS;
    public float colorV;
    public float reflectionStrength;
    public float environmentLight;
    public float emission;
    public float scale;
    public string userAddress;

    public GemParameter GemParameter()
    {
        return new GemParameter(0, Color.HSVToRGB(colorH, colorS, colorV), reflectionStrength, environmentLight, emission, scale);
    }
}
