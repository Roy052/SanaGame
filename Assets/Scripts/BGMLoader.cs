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
        audioSource.Play();
    }

    public void MidMusicON()
    {
        audioSource.clip = mid;
        audioSource.Play();
    }

    public void EndMusicON()
    {
        audioSource.clip = end;
        audioSource.Play();
    }

    public void MusicStop()
    {
        audioSource.Stop();
    }
}
