using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitManager : MonoBehaviour
{

    public GameObject shitFactory;
    float currTime = 0;
    float shitcreateTime = 60;
    void Start()
    {

    }

    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime > shitcreateTime)
        {
            GameObject shit = Instantiate(shitFactory);
            shit.transform.position = transform.position;
            currTime = 0;
        }
    }
}

