using UnityEngine;

public class MetalFenceWindSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string windEvent = "event:/Wind_Fence";

    private FMOD.Studio.EventInstance windInstance;

    private void Start()
    {
        // Crea la instancia
        windInstance = FMODUnity.RuntimeManager.CreateInstance(windEvent);
        windInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

        windInstance.setVolume(1.5f);

        // Lo reproducimos una vez al inicio
        PlayWindSound();
    }

    private void PlayWindSound()
    {
        windInstance.start();
    }

    private void OnDestroy()
    {
        windInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        windInstance.release();
    }
}