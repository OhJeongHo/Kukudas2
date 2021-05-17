using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    
    public GameObject portal1;
    public GameObject portal2;
    public GameObject house;
    public GameObject system1;
    public GameObject system2;

    Vector3 dir = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = portal2.transform.position - house.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Camera"))
        {
            house.transform.position = portal1.transform.position + dir;
            house.transform.Rotate(0, 180, 0);
            system1.SetActive(false);
            system2.SetActive(false);
        }
    }
}
