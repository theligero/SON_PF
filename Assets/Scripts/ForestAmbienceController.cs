using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ForestAmbienceController : MonoBehaviour
{
    [Header("FMOD Event")]
    [EventRef]
    public string forestAmbienceEvent = "event:/Ambience/Wind_Forest";

    private EventInstance forestInstance;

    [Header("Parameter Values")]
    [Range(0f, 1f)]
    public float windIntensity = 0.5f;

    [Range(0f, 1f)]
    public float occlusion = 0f;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Header("Runtime Control Keys")]
    public bool allowKeyControl = true;

    private void Start()
    {
        forestInstance = RuntimeManager.CreateInstance(forestAmbienceEvent);
        forestInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

        ApplyParameters();

        forestInstance.start();
    }

    private void Update()
    {
        // Update 3D position (if GameObject moves)
        forestInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

        // Apply parameters at runtime if modified in Inspector
        ApplyParameters();

        if (allowKeyControl)
        {
            HandleDebugKeys();
        }
    }

    private void ApplyParameters()
    {
        forestInstance.setParameterByName("wind_intensity", windIntensity);
        forestInstance.setParameterByName("occlusion", occlusion);
        forestInstance.setParameterByName("volume", volume);
    }

    private void HandleDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            windIntensity = Mathf.Clamp01(windIntensity + 0.1f);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            windIntensity = Mathf.Clamp01(windIntensity - 0.1f);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            occlusion = Mathf.Clamp01(occlusion + 0.1f);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            occlusion = Mathf.Clamp01(occlusion - 0.1f);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            volume = Mathf.Clamp01(volume + 0.1f);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            volume = Mathf.Clamp01(volume - 0.1f);
    }

    private void OnDestroy()
    {
        forestInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        forestInstance.release();
    }
}