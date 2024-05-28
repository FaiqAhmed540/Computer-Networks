using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        if (gameObject.tag == "Mute")
        {
            if (SoundManager.GetSoundState() == 1)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);

        }
        else
        {
            if (SoundManager.GetSoundState() == 0)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);

        }

         }

 
}
