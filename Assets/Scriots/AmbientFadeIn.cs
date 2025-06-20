using UnityEngine;
using System.Collections;
using FMODUnity;

public class AmbientFadeIn : MonoBehaviour
{
    private StudioEventEmitter _emitter;
    [SerializeField]
    private float targetVolume = 1.0f; // volumen final al que queremos llegar

    [SerializeField]
    private float fadeDuration = 3.0f; // tiempo en segundos para el fade-in

    private void Start()
    {
        _emitter = GetComponent<StudioEventEmitter>();

        if (_emitter == null)
        {
            Debug.LogError("No se encontró StudioEventEmitter en el GameObject.");
            return;
        }

        // Asegura que el volumen inicial es cero
        _emitter.EventInstance.setVolume(0f);

        // Si no tiene PlayOnAwake, asegúrate de empezar el evento
        if (!_emitter.IsPlaying())
        {
            _emitter.Play();
        }

        StartCoroutine(FadeInAmbient());
    }

    private IEnumerator FadeInAmbient()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float currentVolume = Mathf.Lerp(0f, targetVolume, timer / fadeDuration);
            _emitter.EventInstance.setVolume(currentVolume);
            yield return null;
        }

        // Asegura que al final queda en el volumen deseado exacto
        _emitter.EventInstance.setVolume(targetVolume);
    }
}