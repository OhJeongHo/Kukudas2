using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{

    //ȸ�����ɿ��� �Ǵ��ϴ� ����
    public bool useVertical = false;
    public bool useHorizontal = false;
    //ȸ����
    float rotX = 0;
    float rotY = 0;
    //ȸ���ӷ�
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
        //+-90�� �̻� ȸ������ ���� �ʰ� �����ϱ�
        rotX = Mathf.Clamp(rotX, -90, 90);
        //��ü�� ȸ���ϰ� �ʹ�.
        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
    }
}

