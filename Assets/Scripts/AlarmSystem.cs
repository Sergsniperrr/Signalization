using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TurnOnAlarm();
    }

    private void OnTriggerExit(Collider other)
    {
        TurnOfAlarm();
    }

    private void TurnOnAlarm()
    {
        float maxVolume = 1;

        ChangeVolumeTo(maxVolume);
    }

    private void TurnOfAlarm()
    {
        float minVolume = 0;

        ChangeVolumeTo(minVolume);
    }

    private void ChangeVolumeTo(float targetVolume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothVolumeChange(targetVolume));
    }

    public IEnumerator SmoothVolumeChange(float targetVolume)
    {
        float delay = 0.7f;
        float minVolumeForPlaying = 0.1f;

        if (targetVolume >= minVolumeForPlaying)
            _audioSource.Play();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, delay * Time.deltaTime);
            yield return null;
        }

        if (_audioSource.volume < minVolumeForPlaying)
            _audioSource.Stop();

        yield break;
    }
}
