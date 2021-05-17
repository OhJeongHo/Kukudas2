using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTarget : MonoBehaviour
{
    Transform origin;
    float currTime;
    float setTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        // origin.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= setTime)
        {
            float newX = Random.Range(-3f, 3f), newZ = Random.Range(-3f, 3f);
            transform.position = new Vector3(origin.position.x + newX, 0, origin.position.z + newZ);

            currTime = 0;
        }
    }
}
