using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARGroundPosition : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }
}
