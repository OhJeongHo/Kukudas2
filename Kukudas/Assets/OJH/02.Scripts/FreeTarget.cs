using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeTarget : MonoBehaviour
{
    float currTime;
    float setTime = 5;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        currTime = setTime;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= setTime)
        {
            float newX = Random.Range(-5f, 5f), newZ = Random.Range(-5f, 5f);
            transform.position = new Vector3(player.position.x + newX, 0, player.position.z + newZ);

            currTime = 0;
        }
    }
}
