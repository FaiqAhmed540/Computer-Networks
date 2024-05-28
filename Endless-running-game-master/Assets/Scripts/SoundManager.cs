using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    static SoundManager instance = null;
    private void Awake()
    {

        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        
    }
   
    public static void ToggleSound()
    {
        if(PlayerPrefs.GetInt("Muted",0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);

        }
    }

    public static int GetSoundState()
    {
        return PlayerPrefs.GetInt("Muted", 0);
    }
}
