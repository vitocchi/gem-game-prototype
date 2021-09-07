using UnityEngine;

public class GemViewerController : MonoBehaviour
{
    [SerializeField]
    private GemGenerator _gemGenerator;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Canvas _canvas;
    void Start()
    {
        _gemGenerator = GetComponentInChildren<GemGenerator>();
        _camera = GetComponentInChildren<Camera>();
        _canvas = GetComponentInChildren<Canvas>();
    }
    public void ResetGem(Color color, int gemMeshIndex, float reflectionStrength, float environmentLight, float emission, float scale)
    {
        _gemGenerator.Reset(color, gemMeshIndex, reflectionStrength, environmentLight, emission, scale);
    }

    public void SetEnableViewer()
    {
        _camera.enabled = true;
        _canvas.enabled = true;
    }

    public void SetDisableViewer()
    {
        _camera.enabled = false;
        _canvas.enabled = false;
    }
}
