using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureMove : MonoBehaviour
{
    // Ŭ�� ����
    bool isClick;
    // ������ ������Ʈ
    GameObject selectObj;
    void Start()
    {
        



    }

    void Update()
    {
        // ���콺 Ŭ���� ������ Ray �߻��Ѵ� or Ŭ����!!
        if(Input.GetMouseButtonDown(0))
        {
            isClick = true;
            selectObj = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            // Ray�� �ε����� ��´�.
            if(Physics.Raycast(ray, out hit))
            {
                //���࿡ ���̿� �ε����� ������Ʈ�� �̸��� "Collider"���
                //if(hit.transform.name == "Collider")
                
                
                //�±��� �̸��� ""�̶��
                if(hit.transform.tag == "Furniture")
                {

                    selectObj = hit.transform.gameObject;
                    
                }                
            }    
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isClick = false;
        }

        // Ŭ���ϴ� ��
        if(isClick== true && selectObj != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            int layer = 1 << LayerMask.NameToLayer("Ground");
            // Ray�� �ε����� ��´�.
            if (Physics.Raycast(ray, out hit, 100, layer))
            {
                selectObj.transform.position = hit.point;
            }
        }
    }
}
