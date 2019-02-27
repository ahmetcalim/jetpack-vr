using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VR_ControllerManager : MonoBehaviour
{

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public GameObject triggerPrefab;
    public Transform playerTransform;
    public List<MeshRenderer> joystickRenderers;
    public Player player;
    public PowerUpController powerUpController;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)TrackedObj.index); }
    }
    public float Angle { get; set; }
    public float CameraAngle { get; set; }
    public float TurnConstant { get; set; } = 10f;
    public GameObject CollidingObject { get; set; }
    public Vector3 TargetTransform { get; set; }
    public SteamVR_TrackedObject TrackedObj { get; set; }
    void Awake()
    {
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    private void SetCollidingObject(Collider col)
    {
        if (CollidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        CollidingObject = col.gameObject;
    }
    private void FixedUpdate()
    {
        if (Player.IsGameRunning())
        {
            Turn();
        }
    }
    private void Turn()
    {
        float x = Controller.GetAxis(triggerButton).x;
        triggerPrefab.transform.localRotation = Quaternion.Euler(-x * 15, 0, 0);
        Angle = this.transform.rotation.eulerAngles.z;
        playerTransform.GetComponent<Rigidbody>().velocity = new Vector3(playerTransform.GetComponent<Rigidbody>().velocity.x, playerTransform.GetComponent<Rigidbody>().velocity.y, playerTransform.GetComponent<Rigidbody>().velocity.z);
        if ((Angle >= 30f && Angle < 150))
        {
            TurnConstant = (((Angle - 30)))/2;
            playerTransform.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * Time.deltaTime * TurnConstant * PowerUpController.BulletTimeMultipleValue, ForceMode.Impulse);
        }
        if (Angle >= 210f && Angle < 330f)
        {
            TurnConstant = ((Mathf.Abs((330f - Angle))))/2;
            playerTransform.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * Time.deltaTime * TurnConstant * PowerUpController.BulletTimeMultipleValue, ForceMode.Impulse);
        }   
    }
}


