using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[System.Serializable]
public class MarkerInfo
{
    public string imgName;
    public GameObject targetObj;
}

public class ImageDetect : MonoBehaviour
{
    //이미지이름, 해당 이미지에 나타날 오브젝트
    public MarkerInfo[] markerinfos;

    //AR Tracked Image Manager
    ARTrackedImageManager trackedManager;

    void Start()
    {

        trackedManager = GetComponent<ARTrackedImageManager>();
        //정보변화(이미지 인식여부)가 있을때 호출되는 함수 등록
        trackedManager.trackedImagesChanged += OnTrackedImageChanged;

    }

    private void OnDestroy()
    {
        //정보변화(이미지 인식여부)가 있을때 호출되는 함수 제거
        trackedManager.trackedImagesChanged -= OnTrackedImageChanged;
    }



    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs events)
    {


        //변경된 정보만큼 비교한다
        for (int i = 0; i < events.updated.Count; i++)
        {

            ARTrackedImage trImage = events.updated[i];

            for (int j = 0; j < markerinfos.Length; j++)
            {
                //인식된 이미지(1000won)와 MarkerInfos[0].imgName 이 같다면
                if (trImage.referenceImage.name == markerinfos[j].imgName)
                {
                    //만약에 인식된 이미지를 트래킹중이라면
                    if (trImage.trackingState == TrackingState.Tracking)
                    {
                        //MarkerInfos[0].targetObj를 활성화
                        markerinfos[j].targetObj.SetActive(true);
                        //이미지를 따라다니게
                        markerinfos[j].targetObj.transform.position = trImage.transform.position;
                        //이미지가 생겨나는 방향을 위로 향하게 맞춰준다.
                        markerinfos[j].targetObj.transform.up = trImage.transform.up;
                    }
                    else
                    {
                        //MarkerInfos[0].targetObj를 비활성화
                        markerinfos[j].targetObj.SetActive(false);
                    }
                }
            }
        }
    }

    void Update()
    {



    }
}
