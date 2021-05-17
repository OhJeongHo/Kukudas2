using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanitizer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}

