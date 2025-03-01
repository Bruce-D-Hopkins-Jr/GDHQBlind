using System.Collections;
using System.Collections.Generic;
using UnityEditor.VisionOS;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;


    // I decided to add a bool
    public bool isPlayingBGM;

    int bgmIndex;

    int sfxIndex;



    // Start is called before the first frame update

    private void Update()
    {
        if (!isPlayingBGM)
        {
            StopAllBG();
        }
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }
    public void PlaySFX(int _index) // one singular shot
    {
      sfxIndex = _index;
        if (_index < sfx.Length)
        {
            // reduce the pitch
            sfx[_index].pitch = Random.Range(.01f, 1.1f);
            sfx[_index].PlayOneShot(sfx[_index].clip);
        }
    }
    public void PlayLongSFX(int _index)
    {
        sfxIndex = _index;
        if (_index < sfx.Length)
        {
            sfx[_index].Play();
        }
    }
    public void StopPlayingSFX(int _index)
    {
        sfxIndex =  _index;
        sfx[_index].Stop();
    }

    public void PlayBGM(int index)
    {
        bgmIndex = index;
        StopAllBG();

        bgm[index].Play();
    }

    private void StopAllBG()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    
}
