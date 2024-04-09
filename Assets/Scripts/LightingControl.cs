using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class LightingControl : MonoBehaviour
{
    public Light sceneLight;

    public void SetLightIntensityLow() => sceneLight.intensity = 0f;
    public void SetLightIntensityMedium() => sceneLight.intensity = 15f;
    public void SetLightIntensityHigh() => sceneLight.intensity = 30f;
}
