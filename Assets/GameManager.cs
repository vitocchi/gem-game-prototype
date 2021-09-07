using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GemViewerController _gemViewerController;


    [ContextMenu("MineGem")]
    public void MineGem()
    {
        Color color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.4f, 1.0f));
        int index = Random.Range(0, 18);
        float reflectionStrength = Random.Range(0.0f, 2.0f);
        float environmentLight = Random.Range(0.0f, 2.0f);
        float emission = Random.Range(0.0f, 2.0f);
        float scale = Random.Range(1.0f, 3.0f);
        _gemViewerController.ResetGem(color, index, reflectionStrength, environmentLight, emission, scale);
        _gemViewerController.SetEnableViewer();
    }
}
