using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTunnelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GlassTunnel")
        {
            Player.isGlassTunnelActive = !Player.isGlassTunnelActive;
            other.gameObject.GetComponent<MeshCollider>().isTrigger = !Player.isGlassTunnelActive;
        }
        if (other.tag == "NormalFloor")
        {
            
        }
    }
}
