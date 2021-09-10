using UnityEngine;
using System;

public class GemParameter
{
    const float MIN_SCALE = 1.0f;
    const float MAX_SCALE = 3.0f;


    public int MeshIndex { get; set; }
    public Color Color { get; set; }

    public readonly float ReflectionStrength;

    public readonly float EnvironmentLight;

    public readonly float Emission;

    public readonly float Scale;
    public GemParameter(int meshIndex, Color color, float reflectionStrength, float environmentLight, float emission, float scale)
    {
        this.MeshIndex = meshIndex;
        this.Color = color;
        this.ReflectionStrength = floatRangeChecked(reflectionStrength, 0.0f, 2.0f);
        this.EnvironmentLight = floatRangeChecked(environmentLight, 0.0f, 2.0f);
        this.Emission = floatRangeChecked(emission, 0.0f, 2.0f);
        this.Scale = floatRangeChecked(scale, 1.0f, 3.0f);
    }

    public float ParticleEmissionRate
    {
        get { return 5.0f / Emission + 1; }
    }

    private static float floatRangeChecked(float value, float minInclusive, float maxInclusive)
    {
        if (value < minInclusive || value > maxInclusive)
        {
            throw new ArgumentException("invalid value");
        }
        else
        {
            return value;
        }
    }
}

public class GemGenerator : MonoBehaviour
{
    [SerializeField]
    private Mesh[] _gemMeshes;

    [SerializeField]
    private Transform _gem;

    public void Reset(GemParameter parameter)
    {
        _gem.GetComponent<MeshFilter>().mesh = _gemMeshes[parameter.MeshIndex];
        Renderer rend = _gem.GetComponent<Renderer>();
        rend.sharedMaterial.SetColor("_Color", parameter.Color);
        rend.sharedMaterial.SetFloat("_ReflectionStrength", parameter.ReflectionStrength);
        rend.sharedMaterial.SetFloat("_EnvironmentLight", parameter.EnvironmentLight);
        rend.sharedMaterial.SetFloat("_Emission", parameter.Emission);
        _gem.transform.localScale = Vector3.one * parameter.Scale;
        ParticleSystem particleSystem = _gem.GetComponentInChildren<ParticleSystem>();
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = parameter.ParticleEmissionRate;
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.radius = parameter.Scale;
    }
}
