using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infoposition : MonoBehaviour
{
    public GameObject order;
    public GameObject love;
    Vector3 pos;
    private void Start()
    {
        pos = gameObject.transform.position;
    }
    void Update()
    {
        if (order.activeSelf || love.activeSelf)
        {
            pos.y = 550;
            gameObject.transform.position = pos;
        }
        if (order.activeSelf == false && love.activeSelf == false)
        {
            pos.y = 350;
            gameObject.transform.position = pos;
        }
    }
}
