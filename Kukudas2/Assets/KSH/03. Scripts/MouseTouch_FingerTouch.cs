using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseTouch_FingerTouch : MonoBehaviour
{
    public GameObject factory;
    void Start()
    {



    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //PC
            if (EventSystem.current.IsPointerOverGameObject() == false)
            //¸ð¹ÙÀÏ
            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            {
                GameObject go = Instantiate(factory);
                go.transform.position = Camera.main.transform.forward;
            }
        }
    }
}
