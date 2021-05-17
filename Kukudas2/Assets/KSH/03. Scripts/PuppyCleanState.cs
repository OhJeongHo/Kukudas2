using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PuppyCleanState : MonoBehaviour
{

    public float puppyCleanValue = 50;
    float pvc;
    GameObject germ;
    public GameObject puppyCleanUi;

    //만약 강아지랑 세균이 충돌을 하게되면
    //세균은 없어지고
    //강아지의 CleanState의 수치 1이 줄어든다.
    //강아지의 CleanState가 0이 되면
    //"강아지가 더럽습니다. 목욕을 시키세요." 문구 출력

    void Start()
    {
        //germ = GameObject.Find("Germ");
        //초기 청결도 설정
        pvc = puppyCleanValue;
        Slider slider = puppyCleanUi.GetComponent<Slider>();
        slider.value = 1;
    }

    void Update()
    {
        Slider slider = puppyCleanUi.GetComponent<Slider>();
        //slider.value = pvc / puppyCleanValue;
        slider.value -= Time.deltaTime * 0.003f;
        //print("강아지의 청결도 : " + slider.value);

        if (slider.value <= 0)
        {
            print("강아지가 더럽습니다. 목욕을 시키세요.");
            slider.value = 0;
        }
    }

    public void DamagedAction(float damage)
    {
        pvc -= damage;
        print("현재 청결도 : " + pvc);
    }
}

