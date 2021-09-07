using UnityEngine;
using System;

public class GemGenerator : MonoBehaviour
{
    [SerializeField]
    private Mesh[] _gemMeshes;

    [SerializeField]
    private Transform _gem;

    public void Reset(Color color, int gemMeshIndex, float reflectionStrength, float environmentLight, float emission, float scale)
    {
        if (gemMeshIndex < 0 || gemMeshIndex >= _gemMeshes.Length)
        {
            throw new ArgumentException("invalid gemMeshIndex");
        }
        if (reflectionStrength < 0 || reflectionStrength > 2)
        {
            throw new ArgumentException("invalid reflectionStrength");
        }
        if (environmentLight < 0 || environmentLight > 2)
        {
            throw new ArgumentException("invalid environmentLight");
        }
        if (emission < 0 || emission > 2)
        {
            throw new ArgumentException("invalid emission");
        }
        var particleEmissionRate = 5.0f / emission + 1;
        if (scale < 1 || scale > 3)
        {
            throw new ArgumentException("invalid scale");
        }
        _gem.GetComponent<MeshFilter>().mesh = _gemMeshes[gemMeshIndex];
        Renderer rend = _gem.GetComponent<Renderer>();
        rend.sharedMaterial.SetColor("_Color", color);
        rend.sharedMaterial.SetFloat("_ReflectionStrength", reflectionStrength);
        rend.sharedMaterial.SetFloat("_EnvironmentLight", environmentLight);
        rend.sharedMaterial.SetFloat("_Emission", emission);
        _gem.transform.localScale = Vector3.one * scale;
        ParticleSystem particleSystem = _gem.GetComponentInChildren<ParticleSystem>();
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = particleEmissionRate;
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.radius = scale;
    }
}
