using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArManager : MonoBehaviour
{

    //Ar Session
    public GameObject arSesstion;
    //Ar Session Origin
    public GameObject arSesstionOrigin;
    //indicator
    public GameObject indicator;
    public GameObject All;
    //강아지 기능 버튼 UI
    public GameObject ui;
    //TestCam
    public GameObject TestCam;



    //자동차
    //public GameObject car;
    //AR raycast Manager
    ARRaycastManager rayManager;




    void Awake()
    {
#if UNITY_EDITOR
        arSesstion.SetActive(false);
        arSesstionOrigin.SetActive(false);
        TestCam.SetActive(true);
        // All.SetActive(false);
#else
        arSesstion.SetActive(true);
        arSesstionOrigin.SetActive(true);
        //All.SetActive(true);
        TestCam.SetActive(false);
#endif

    }

    void Start()
    {
        rayManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
#if UNITY_EDITOR
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 100))
        {
            DetectedGround(hit.point);
        }
#else
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if(rayManager.Raycast(ray, hits, TrackableType.Planes))
        {
            DetectedGround(hits[0].pose.position);
        }

#endif
        else

        {
            indicator.SetActive(false);

        }
        if (Input.GetMouseButtonDown(0))
        {
            // 만약에 indicator가 활성화 되어있다면
            if (indicator.activeSelf)
            {
                //모든 사물 활성화(집, 벽, 강아지, 가구)
                All.SetActive(true);
                All.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                // indicator 비활성화
                indicator.SetActive(false);
                // UI 활성화
                ui.SetActive(true);
                // ArManager 비활성화
                enabled = false;
            }
        }
    }

    void DetectedGround(Vector3 hitPos)
    {
        indicator.SetActive(true);
        indicator.transform.position = hitPos;
        indicator.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
    }
}

