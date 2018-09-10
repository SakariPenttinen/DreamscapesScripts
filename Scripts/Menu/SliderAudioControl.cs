using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SliderAudioControl : MonoBehaviour {
    public string mixerGroupName = "Master";

    public AudioMixer mixer;

    public void SetMusicLevel(float musicLevel)
    {
        mixer.SetFloat(mixerGroupName, Conv(musicLevel));
    }

    float Conv(float fl)
    {
        return Mathf.Log(fl) * 20;
    }
}
