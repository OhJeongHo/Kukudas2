using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject[] pet;
    public GameObject[] cloud;
    public GameObject[] title;
    public GameObject[] end;
    float currtime = 0.0f;
    float alpha1 = 0.0f;
    float alpha2 = 0.0f;
    float alpha3 = 0.0f;
    float dir = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 강아지 이미지 등장
        iTween.MoveBy(pet[0], iTween.Hash( "y", 400, "time", 0.3, "easetype", iTween.EaseType.easeOutBack,
            "oncompletetarget", pet[1]));
        iTween.MoveBy(pet[1], iTween.Hash("delay", 0.3, "y", 500, "time", 0.3, "easetype", iTween.EaseType.easeOutBack,
            "oncompletetarget", pet[2]));
        iTween.MoveBy(pet[2], iTween.Hash("delay", 0.6, "y", 637, "time", 0.3, "easetype", iTween.EaseType.easeOutBack,
            "oncomplete", "OnCompleteAni"));
    }

    void OnCompleteAni()
    {
        for (int i = 0; i < cloud.Length; i++)
        {
            cloud[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currtime += Time.deltaTime;

        Cloud();

        // 화면을 터치할 때 - 구름이 화면을 덮고 2초후 씬 전환
        if (Input.GetMouseButtonDown(0))
        {
            iTween.MoveBy(end[0], iTween.Hash("x", 1677, "time", 2));
            iTween.MoveBy(end[1], iTween.Hash("x", -1545, "time", 2));
            Invoke("Next", 2);
        }
    }

    void Next()
    {
        SceneManager.LoadScene("SelectScene");
    }

    // 배경 이미지들 각각 등장, 글자 깜빡임
    void Cloud()
    {
        if (currtime >= 0.6 && currtime < 2)
        {
            alpha1 += Time.deltaTime;
            for (int i = 0; i < cloud.Length; i++)
            {
                cloud[i].GetComponent<Image>().color = new Color(1, 1, 1, alpha1);
            }
        }
        if (currtime >= 0.8 && currtime < 2)
        {
            alpha2 += Time.deltaTime;
            title[0].GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, alpha2);
        }
        if (currtime >= 1.2)
        {
            alpha3 += Time.deltaTime * dir;
            if (alpha3 <= 0 || alpha3 >= 1) dir *= -1;
            title[1].GetComponent<Text>().color = new Color(1, 1, 1, alpha3);
        }
    }
}
