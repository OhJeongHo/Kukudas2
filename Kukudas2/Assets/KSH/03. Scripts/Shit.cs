using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : MonoBehaviour
{

    Rigidbody rigid;

    void Start()
    {

        rigid.AddForce(transform.forward * 10);

    }

    void Update()
    {
        


    }
}
