using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager: MonoBehaviour 
{

    public static DataManager instance;
    enum Model
    {
        Model_1, Model_2, Model3, Model4 
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    //public Model currModel;
}
