using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureMove : MonoBehaviour
{
    // 클릭 상태
    bool isClick;
    // 선택한 오브젝트
    GameObject selectObj;
    void Start()
    {
        



    }

    void Update()
    {
        // 마우스 클릭한 지점에 Ray 발사한다 or 클릭중!!
        if(Input.GetMouseButtonDown(0))
        {
            isClick = true;
            selectObj = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            // Ray랑 부딪힌놈 담는다.
            if(Physics.Raycast(ray, out hit))
            {
                //만약에 레이와 부딪히는 오브젝트의 이름이 "Collider"라면
                //if(hit.transform.name == "Collider")
                
                
                //태그의 이름이 ""이라면
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

        // 클릭하는 중
        if(isClick== true && selectObj != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            int layer = 1 << LayerMask.NameToLayer("Ground");
            // Ray랑 부딪힌놈 담는다.
            if (Physics.Raycast(ray, out hit, 100, layer))
            {
                selectObj.transform.position = hit.point;
            }
        }
    }
}
