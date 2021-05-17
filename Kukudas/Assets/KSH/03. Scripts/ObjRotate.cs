using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{

    //회전가능여부 판단하는 변수
    public bool useVertical = false;
    public bool useHorizontal = false;
    //회전값
    float rotX = 0;
    float rotY = 0;
    //회전속력
    public float rotSpeed = 200;
    void Start()
    {

    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        if (useVertical == true)
        {
            rotX += -my * rotSpeed * Time.deltaTime;
        }

        if (useHorizontal == true)
        {
            rotY += mx * rotSpeed * Time.deltaTime;
        }
        //+-90도 이상 회전값을 갖지 않게 제한하기
        rotX = Mathf.Clamp(rotX, -90, 90);
        //물체를 회전하고 싶다.
        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
    }
}

