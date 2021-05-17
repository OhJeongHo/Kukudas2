using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public PortalController portalController;
    private void OnTriggerEnter(Collider other)
    {
        portalController.Teleport();
    }
}
