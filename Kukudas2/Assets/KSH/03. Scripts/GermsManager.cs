using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermsManager : MonoBehaviour
{
    float currTime = 0;
    float germcreateTime = 1.5f;
    float germDestroyTime = 15;
    public GameObject germsFactory;
    public GameObject gm;

    void Start()
    {
        
    }

    void Update()
    {
        currTime += Time.deltaTime;
        if(currTime > germcreateTime)
        {
            GameObject germ = Instantiate(germsFactory);
            germ.transform.position = transform.position;
            germ.transform.forward = transform.forward;
            currTime = 0;
        }
        if(currTime > germDestroyTime)
        {
            Destroy(gm);
        }
    }
}
