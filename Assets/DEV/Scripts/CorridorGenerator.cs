using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorGenerator : MonoBehaviour
{
    
    public Vector3 currPosition;
    public Transform allTunnel;
    public NodeGraph nodeGraph;
    public ObstacleGenerator obstacleGenerator;
    public Rigidbody rb;
    public float gameSpeed;

    public void Create(KNode k,bool isHeadNode)
    {
        if (isHeadNode)
        {
            k.gameObject.transform.localPosition = new Vector3(currPosition.x, currPosition.y, currPosition.z + k.scaleFactor);
            currPosition = k.gameObject.transform.localPosition;
            nodeGraph.tailNode.nextNode = k;
            k.prevNode = nodeGraph.tailNode;
            nodeGraph.tailNode = k;
            nodeGraph.headNode = nodeGraph.headNode.nextNode;
            nodeGraph.tailNode.nextNode = null;
        }
        else
        {
            GameObject g = Instantiate(k.gameObject,allTunnel);            
            currPosition = new Vector3(currPosition.x, currPosition.y, currPosition.z + k.scaleFactor);
            g.transform.localPosition = currPosition;            
            KNode currentNode= g.GetComponent<KNode>();
            nodeGraph.tailNode.nextNode = currentNode;
            currentNode.prevNode = nodeGraph.tailNode;
            nodeGraph.tailNode = currentNode;
            nodeGraph.headNode = nodeGraph.headNode.nextNode;
            Destroy(nodeGraph.headNode.prevNode.gameObject);
            nodeGraph.tailNode.nextNode = null;
            obstacleGenerator.GenerateObstacle();
        }
    }

    void FixedUpdate()
    {
        MoveTunnel();
    }

    void MoveTunnel()
    {
        rb.velocity = -transform.forward * gameSpeed*10f;
        
    }


}
