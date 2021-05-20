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
    float currtime = 0;
    float alpha1 = 0;
    float alpha2 = 0;
    float alpha3 = 0;
    float dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveBy(pet[0], iTween.Hash( "y", 400, "time", 0.3, "easetype", iTween.EaseType.easeOutBack,
            "oncompletetarget", pet[1]));
        iTween.MoveBy(pet[1], iTween.Hash("delay", 0.3, "y", 500, "time", 0.3, "easetype", iTween.EaseType.easeOutBack,
            "oncompletetarget", pet[2]));
        iTween.MoveBy(pet[2], iTween.Hash("delay", 0.6, "y", 637, "time", 0.3, "easetype", iTween.EaseType.easeOutBack,
            "oncomplete", "OnCompleteAni"));
    }

    void OnCompleteAni()
    {
        print("½ÇÇà");
        cloud[0].SetActive(true);
        cloud[1].SetActive(true);
        cloud[2].SetActive(true);
        cloud[3].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        currtime += Time.deltaTime;
        Cloud();
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
    void Cloud()
    {
        if (currtime >= 0.6)
        {
            alpha1 += Time.deltaTime;
            cloud[0].GetComponent<Image>().color = new Color(1, 1, 1, alpha1);
            cloud[1].GetComponent<Image>().color = new Color(1, 1, 1, alpha1);
            cloud[2].GetComponent<Image>().color = new Color(1, 1, 1, alpha1);
            cloud[3].GetComponent<Image>().color = new Color(1, 1, 1, alpha1);
        }
        if (currtime >= 0.8)
        {
            alpha2 += Time.deltaTime;
            title[0].GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, alpha2);
            //title[0].GetComponent<Text>().color = new Color(237/255f, 116/255f, 161/255f, alpha2);
            // title[1].GetComponent<Text>().color = new Color(255/255f, 234/255f, 83/255f, alpha2);
        }
        if (currtime >= 1.2)
        {
            alpha3 += Time.deltaTime * dir;
            if (alpha3 <= 0 || alpha3 >= 1) dir *= -1;
            title[1].GetComponent<Text>().color = new Color(1, 1, 1, alpha3);
        }
    }
}
