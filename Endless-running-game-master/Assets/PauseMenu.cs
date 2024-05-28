using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update




    // Update is called once per frame
    void Update()
    {
        if (Player.isPaused)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
