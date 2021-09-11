using Cinemachine;
using UnityEngine;

public class GemViewerController : MonoBehaviour
{
    [SerializeField]
    private GemGenerator _gemGenerator;

    [SerializeField]
    private CinemachineFreeLook _camera;

    public void Init(GemParameter parameter)
    {
        Transform gem = _gemGenerator.Generate(parameter).transform;
        _camera.Follow = gem;
        _camera.LookAt = gem;
    }

    public void Close()
    {
        Destroy(this);
    }
}
