using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLogic : MonoBehaviour
{
    public TunnelGenerator tunnelGenerator;
    public Player player;
    public Transform themesParentObj;
    public int initialTunnelSize;
    public GameObject bottomCollider;


    public static TunnelPart currentTunnelPart;
    public static Vector3 nextAttachmentPoint;


    // Start is called before the first frame update
    void Start()
    {
        
        tunnelGenerator.GenerateTunnel(player, themesParentObj, initialTunnelSize);
        SetBottomColliderPosition(10f);
    }

    private void SetBottomColliderPosition(float threshold)
    {
        Vector3 playerBottomColliderPosition = new Vector3(player.transform.position.x, player.transform.position.y - threshold, player.transform.position.z);
        bottomCollider.transform.position = playerBottomColliderPosition;
    }
}
