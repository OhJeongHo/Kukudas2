using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureEditor : MonoBehaviour
{
    //클릭 상태
    bool isClick;
    //선택한 오브젝트
    GameObject selectobj;

    void Start()
    {
        
        

    }

    void Update()
    {
        //마우스 클릭한 지점에 Ray를 발사한다
        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
            selectobj = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Ray랑 부딪힌놈 담는다.
            if(Physics.Raycast(ray, out hit))
            {
                //만약에 레이와 부딪히는 오브젝트의 Tag가 Furniture이라면
                if(hit.transform.tag == "Furniture")
                {
                    //선택한 오브젝트를 레이가 클릭하고 있는 게임오브젝트로 한다.
                    selectobj = hit.transform.gameObject;
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {

            isClick = false;

        }

        //클릭하는 중 위에 조건에 해당하는 사물을 옮기고 싶을 때

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
