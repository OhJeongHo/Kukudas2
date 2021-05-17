using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class PlayerSanitizer : MonoBehaviour
{
    public GameObject sanitizerFactory;
    public GameObject firePos;

    void Start()
    {

    }

    void Update()
    {


    }

    public void SanitizerSpray()
    {
        GameObject sntz = Instantiate(sanitizerFactory);
        sntz.transform.position = transform.position;
        Rigidbody rigid = sntz.GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * 1000);
    }
}

