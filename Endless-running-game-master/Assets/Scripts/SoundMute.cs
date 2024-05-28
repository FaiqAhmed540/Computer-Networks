using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMute : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (SoundManager.GetSoundState() == 0)
        {
            GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            GetComponent<AudioSource>().volume = 1;
        }
    }
}
