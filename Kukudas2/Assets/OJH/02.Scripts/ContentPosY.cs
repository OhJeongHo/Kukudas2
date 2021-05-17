using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPosY : MonoBehaviour
{
    int childCount;
    // Update is called once per frame

    private void Start()
    {
    }
    void Update()
    {

        PosSetting();
    }

    void PosSetting()
    {

        if(childCount != gameObject.transform.childCount)
        {
            childCount = gameObject.transform.childCount;

            if(childCount > 6)
            {
                Vector3 pos = gameObject.transform.position;
                pos.y += 60;
                gameObject.transform.position = pos;
            }
        }
    }
}
