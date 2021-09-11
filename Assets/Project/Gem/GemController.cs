using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField]
    private float rotateDegreePerSec;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * rotateDegreePerSec * Time.deltaTime);
    }
}
