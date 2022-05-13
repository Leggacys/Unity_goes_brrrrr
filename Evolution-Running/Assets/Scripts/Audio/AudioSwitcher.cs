using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{

    public AudioClip clip1;

    public AudioClip clip2;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip1;
        source.Play();
    }

    public void SwitchClips()
    {
        if (source.clip == clip1)
        {
            source.clip = clip2;
            source.Play();
        }
        else
        {

            source.clip = clip1;
            source.Play();
        }
    }

}
