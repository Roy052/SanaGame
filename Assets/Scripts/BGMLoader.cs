using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMLoader : MonoBehaviour
{
    public AudioClip start, mid, end;
    AudioSource audioSource;

    private static BGMLoader bgmLoaderInstance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(bgmLoaderInstance == null)
        {
            bgmLoaderInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        
    }

    public void StartMusicON()
    {
        audioSource.clip = start;
        StartCoroutine(VolumeUp());
    }

    public void MidMusicON()
    {
        audioSource.clip = mid;
        StartCoroutine(VolumeUp());
    }

    public void EndMusicON()
    {
        audioSource.clip = end;
        StartCoroutine(VolumeUp());
    }

    public void MusicStop()
    {
        StartCoroutine(VolumeDown());
    }

    IEnumerator VolumeUp()
    {
        audioSource.Play();
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }

    IEnumerator VolumeDown()
    {
        while(audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        audioSource.Stop();
    }
}
