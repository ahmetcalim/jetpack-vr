using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelPart : MonoBehaviour
{

    private GameObject partAsset;
    public TunnelPart nextTunnelPart;
    public TunnelPart previousTunnelPart;
    public Theme tunnelPartTheme;
    public ETunnelPartType tunnelPartType;


    private void Awake()
    {   

        this.partAsset = this.gameObject;

        


    }

    private void Start()
    {
        if (isContinuingPart())
        {
            checkNextTunnelNull();
        }
    }

    private void checkNextTunnelNull()
    {
        if (nextTunnelPart == null)
        {
            Debug.LogError("You MUST define the next part before press play!");
        }
    }
    private bool isContinuingPart()
    {
        return this.tunnelPartType == ETunnelPartType.LONG_CONTINUOUS_HEAD || this.tunnelPartType == ETunnelPartType.LONG_CONTINUOUS_PART;
    }
}
