using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{


    public UI_Manager UiManager;
    AudioSource source;
    public AudioClip music;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        source.clip = music;
        source.Play();
        source.volume = 0.5f;

        UiManager = FindObjectOfType<UI_Manager>();


        if (UiManager != null)
        {
            UiManager.MusicVolume.value = source.volume;
            UiManager.MusicVolumeChange = VolumeChange;
        }
    }

    void VolumeChange()
    {
        source.volume = UiManager.MusicVolume.value;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (source.volume < 1)
                source.volume += 0.1f;
            else
                source.volume = 1f;

            if(UiManager!=null)
                UiManager.MusicVolume.value = source.volume;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (source.volume > 0)
                source.volume -= 0.1f;
            else
                source.volume = 0f;
            if (UiManager != null)
                UiManager.MusicVolume.value = source.volume;
        }

    }
}
