using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureEditor : MonoBehaviour
{
    //Ŭ�� ����
    bool isClick;
    //������ ������Ʈ
    GameObject selectobj;

    void Start()
    {
        
        

    }

    void Update()
    {
        //���콺 Ŭ���� ������ Ray�� �߻��Ѵ�
        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
            selectobj = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Ray�� �ε����� ��´�.
            if(Physics.Raycast(ray, out hit))
            {
                //���࿡ ���̿� �ε����� ������Ʈ�� Tag�� Furniture�̶��
                if(hit.transform.tag == "Furniture")
                {
                    //������ ������Ʈ�� ���̰� Ŭ���ϰ� �ִ� ���ӿ�����Ʈ�� �Ѵ�.
                    selectobj = hit.transform.gameObject;
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {

            isClick = false;

        }

        //Ŭ���ϴ� �� ���� ���ǿ� �ش��ϴ� �繰�� �ű�� ���� ��

        if(isClick == true && selectobj == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layer = 1 << LayerMask.NameToLayer("Ground");
            if(Physics.Raycast(ray, out hit, 100, layer))
            {
                selectobj.transform.position = hit.point;
            }

        }

    }
}
