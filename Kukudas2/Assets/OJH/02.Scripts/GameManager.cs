using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject hungryUi;
    public GameObject toiletUi;
    public GameObject activityUi;
    public GameObject cleanUi;
    public Text status;

    public static float toiletsetTime = 60f;
    public static float hungrysetTime = 180f;
    public static float activitySet = 50f;
    public static float toiletTime;
    public static float hungryTime = 180;
    public static float activity;
    public static int loyalty;
    public static float cleansetTime = 360f;
    public static float cleanTime = 360f;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    void Update()
    {
        DogStatus();
        HungryState();
        ToiletState();
        CleanState();
        ActivityState();
        StatusView();
    }
    public void DogStatus()
    {
        toiletTime += Time.deltaTime;
        hungryTime -= Time.deltaTime;
        cleanTime -= Time.deltaTime;
    }
    
    public void Activity(float index)
    {
        activity += index;
    }

    //public void InputMoney(int index)
    //{
    //    money += index;
    //}
    //public void OutputMoney(int index)
    //{
    //    money -= index;
    //}

    public void HungryState()
    {
        Slider slider = hungryUi.GetComponent<Slider>();
        slider.value = hungryTime / hungrysetTime;
    }

    public void ToiletState()
    {
        Slider slider = toiletUi.GetComponent<Slider>();
        slider.value = toiletTime / toiletsetTime;
    }

    public void ActivityState()
    {
        if (activityUi == null)
        {
            return;
        }
        Slider slider = activityUi.GetComponent<Slider>();
        slider.value = activity / activitySet;
    }

    public void CleanState()
    {
        if (cleanUi == null)
        {
            return;
        }
        Slider slider = cleanUi.GetComponent<Slider>();
        slider.value = cleanTime / cleansetTime;
    }


    void StatusView()
    {
        status.GetComponent<Text>().text = "포만감 : " + (int)((hungryTime/hungrysetTime)*100) +"%"
            + "\n화장실 : " + (int)((toiletTime/toiletsetTime)*100)+ "%"
            + "\n산책량 : " + activity +"미터"
            + "\n충성도 : " + loyalty;
    }
}
