using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTunnelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GlassTunnel")
        {
            if (gameObject.name == "Tunnel_Collision")
            {
                Debug.Log("Açıldı");
                Player.isGlassTunnelActive = true;
                FindObjectOfType<Player>().ActivateGlassTunnel(false);
            }
            if ((gameObject.name == "HOSPITAL_COLUMN" || gameObject.name == "LAB_COLUMN") && Player.isGlassTunnelActive == true)
            {
                Debug.Log("Kapandı");
                Player.isGlassTunnelActive = false;
                FindObjectOfType<Player>().ActivateGlassTunnel(true);
            }
            
        }
    }
}
