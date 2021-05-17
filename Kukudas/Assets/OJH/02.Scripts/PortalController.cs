using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform land1, land2;
    public Transform playerRoot, playerCam;
    public Transform portalCam;

    public RenderTexture renderTex;

    // Start is called before the first frame update
    void Start()
    {
        renderTex.width = Screen.width;
        renderTex.height = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffset = playerCam.position - land1.position;

        portalCam.position = land2.position + playerOffset;
        portalCam.rotation = playerCam.rotation;
    }

    public void Teleport()
    {
        var playerLand = land1;
        land1 = land2;
        land2 = playerLand;

        playerRoot.position = portalCam.position;
    }
}
