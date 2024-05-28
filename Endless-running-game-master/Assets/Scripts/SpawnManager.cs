using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public void Awake()
    {
        instance = this;
    }
    public Transform[] spawnpoints;
    void Start()
    {
        foreach(Transform spaw in spawnpoints)
        {
            spaw.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform getspawnpoint()
    {
        return spawnpoints[Random.Range(0,spawnpoints.Length)]; 
    }
}
