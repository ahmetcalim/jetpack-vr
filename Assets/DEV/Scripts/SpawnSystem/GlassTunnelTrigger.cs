using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTunnelTrigger : MonoBehaviour
{
    public int index;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GlassTunnel")
        {
            if (index == 1)
            {
                Debug.Log("Açıldı");
                Player.IsGlassTunnelActive = true;
                FindObjectOfType<Player>().ActivateGlassTunnel(false);
            }
            if (index==0 && Player.IsGlassTunnelActive == true)
            {
                Debug.Log("Kapandı");
                Player.IsGlassTunnelActive = false;
                FindObjectOfType<Player>().ActivateGlassTunnel(true);
            }
            
        }
    }
}
