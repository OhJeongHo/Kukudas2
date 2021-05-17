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
    //������ ��� ��ư UI
    public GameObject ui;
    //TestCam
    public GameObject TestCam;



    //�ڵ���
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
            // ���࿡ indicator�� Ȱ��ȭ �Ǿ��ִٸ�
            if (indicator.activeSelf)
            {
                //��� �繰 Ȱ��ȭ(��, ��, ������, ����)
                All.SetActive(true);
                All.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                // indicator ��Ȱ��ȭ
                indicator.SetActive(false);
                // UI Ȱ��ȭ
                ui.SetActive(true);
                // ArManager ��Ȱ��ȭ
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

