using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public AudioSource musicSource;

    public void SetVolumeLow() => musicSource.volume = 0.33f;
    public void SetVolumeMedium() => musicSource.volume = 0.66f;
    public void SetVolumeHigh() => musicSource.volume = 1f;
}