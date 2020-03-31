using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Public Properties
    public List<AudioClip> clips;
    public List<AudioClip> songs;

    // Private Properties
    private List<AudioSource> sources = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        if (clips == null || clips.Count < 1)
            Debug.LogError("\tNo audio clips defined in [ AudioManager ] script!");
        if (songs == null || songs.Count < 1)
            Debug.LogError("\tNo songs defined in [ AudioManager ] script!");

        // IF there are clips and songs...
        if (clips.Count > 0 && songs.Count > 0)
        {
            // Creating an AudioSource component for one song and each clip
            int createAmount = 1 + clips.Count;
            for (int num = 0; num < createAmount; num++)
            {
                sources.Add(gameObject.AddComponent<AudioSource>());
                if (num == 0)
                {
                    sources[0].clip = songs[0];
                    sources[0].volume = 0.05f;
                    sources[0].loop = true;
                    sources[0].Play();
                }
                else
                {
                    sources[num].clip = clips[num - 1];
                    sources[num].volume = 0.5f;
                    sources[num].playOnAwake = false;
                }
            }
        }
    }

    // PlaySong()
    public void PlaySong(int index)
    {
        if (sources[0].clip != songs[index])
        {
            if (sources[0].isPlaying)
                sources[0].Stop();
            sources[0].clip = songs[index];
            sources[0].Play();
        }
    }

    // PlaySFX()
    public void PlaySFX(int index)
    {
        if (!sources[index + 1].isPlaying)
            sources[index + 1].Play();
    }

    // StopSFX()
    public void StopSFX(int index)
    {
        if (sources[index + 1].isPlaying)
            sources[index + 1].Stop();
    }
}
