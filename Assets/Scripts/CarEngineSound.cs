using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class CarEngineSound : MonoBehaviour
{
    [EventRef]
    public string carEvent = "event:/Car_Engine";

    private EventInstance carInstance;

    [Range(0f, 1f)] public float occlusionLevel = 0f;
    [Range(0f, 1f)] public float volumeLevel = 1f;

    private void Start()
    {
        carInstance = RuntimeManager.CreateInstance(carEvent);
        carInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

        // Arranca el coche por defecto
        carInstance.setParameterByName("occlusion", occlusionLevel);
        carInstance.setParameterByName("volume", volumeLevel);
        carInstance.start();
    }

    private void Update()
    {
        // Aumenta oclusión con P
        if (Input.GetKeyDown(KeyCode.P))
        {
            occlusionLevel = Mathf.Clamp01(occlusionLevel + 0.1f);
            carInstance.setParameterByName("occlusion", occlusionLevel);
        }

        // Disminuye oclusión con O
        if (Input.GetKeyDown(KeyCode.O))
        {
            occlusionLevel = Mathf.Clamp01(occlusionLevel - 0.1f);
            carInstance.setParameterByName("occlusion", occlusionLevel);
        }

        // Subir volumen con +
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            volumeLevel = Mathf.Clamp01(volumeLevel + 0.1f);
            carInstance.setParameterByName("volume", volumeLevel);
        }

        // Bajar volumen con -
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            volumeLevel = Mathf.Clamp01(volumeLevel - 0.1f);
            carInstance.setParameterByName("volume", volumeLevel);
        }
    }

    private void OnDestroy()
    {
        carInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        carInstance.release();
    }
}