using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    public GameObject[] cloud;
    public GameObject dogpage;
    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveBy(cloud[0], iTween.Hash("x", -100, "time", 2));
        iTween.MoveBy(cloud[1], iTween.Hash("x", 100, "time", 2));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickDog1()
    {
        dogpage.SetActive(true);
    }

    public void OnClickNo()
    {
        dogpage.SetActive(false);
    }
    public void OnClickOk()
    {
        iTween.MoveBy(cloud[0], iTween.Hash("x", 100, "time", 2));
        iTween.MoveBy(cloud[1], iTween.Hash("x", -100, "time", 2));
        Invoke("LoadScene", 2);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("OJH_GroundScene");
    }
}
