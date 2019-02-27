using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    public SteamVR_TrackedObject leftController;
    public SteamVR_TrackedObject rightController;
    public PowerUpController powerUpController;
    public Player player;
    public List<Image> throttleLoadingBar;
    public Text throttlePowerTxt;
    public List<Image> dashBoardImageElements;
    public List<Text> dashBoardTextElements;
    public GameObject playerTransform;
    public AudioSource jetpackThrottle;

    public SteamVR_Controller.Device ControllerL
    {
        get { return SteamVR_Controller.Input((int)leftController.index); }
    }
    public SteamVR_Controller.Device ControllerR
    {
        get { return SteamVR_Controller.Input((int)rightController.index); }
    }
    public bool IsDashboardVisible { get; set; } = true;
    public float AccelerationY { get; set; }
    public static float YMovementConstant_1 { get; set; } = 44;
    public static float YMovementConstant_2 { get; set; } = 100;
    public static float YMovementConstant_3 { get; set; } = 3;
    public float Timer { get; set; }
    public float AngleXController { get; set; }

    void FixedUpdate()
    {
        if (Player.IsGameRunning())
        {
            CheckTouchpadInput();
            CheckTriggerInput();
            CheckGripInput();
        }
    }
    private void Start()
    {
       
    }
    private void CheckTouchpadInput()
    {
        if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            UsePowerup(leftController);
        }
        if (ControllerR.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            UsePowerup(rightController);
        }
    }
    private void UsePowerup(SteamVR_TrackedObject controller)
    {
     
        switch (controller.powerUpSlot.tag)
        {
            case "Phase":
                if (!powerUpController.IsPhaseActive())
                {
                    powerUpController.UsePhase();
                    controller.powerUpSlot.tag = "Untagged";
                    controller.powerUpSlot.GetComponent<Image>().sprite = null;
                }
                break;
            case "Rocket":
                powerUpController.UseRocket();
                controller.powerUpSlot.tag = "Untagged";
                controller.powerUpSlot.GetComponent<Image>().sprite = null;
                break;
            case "BulletTime":
                powerUpController.UseBulletTime();
                controller.powerUpSlot.tag = "Untagged";
                controller.powerUpSlot.GetComponent<Image>().sprite = null;
                break;
            default:
                break;
        }
    }
    private void CheckTriggerInput()
    {
        AngleXController = (leftController.transform.rotation.x + rightController.transform.rotation.x) / -2;
        if (ControllerL.GetHairTrigger() && ControllerR.GetHairTrigger())
        {
            if (!jetpackThrottle.isPlaying)
            {
              
            }
            if (Timer <=1f)
            {
                
                Timer += Time.deltaTime;
                jetpackThrottle.volume += Timer/50f;
                if (throttleLoadingBar[(int)(Timer * 10f)].enabled == false)
                {
                    throttleLoadingBar[(int)(Timer * 10f)].enabled = true;
                }
                throttlePowerTxt.text = "%" + ((int)(Timer * 100f)).ToString();

            }
            if (AngleXController > 0f)
            {
                Up(1);
            }
            else
            {
                Up(-1);
            }
        }
        else
        {
            if (Timer >= 0f)
            {
                
                Timer -= Time.deltaTime;
                jetpackThrottle.volume -= Timer/50f;
                if (throttleLoadingBar[(int)(Timer*10f) + 1].enabled == true)
                {
                    throttleLoadingBar[(int)(Timer*10f) + 1].enabled = false;
                }
                if (Timer<=0.01f)
                {
                    throttleLoadingBar[0].enabled = false;
                }
                throttlePowerTxt.text = "%" + ((int)(Timer * 100f) + 1).ToString();
            }
        }
        if (ControllerL.GetHairTrigger())
        {
            if (ControllerL.velocity.magnitude > 3f)
            {
                Debug.Log("SOLU SALLADIM KIRDIM BEBEYİM");
            }
        }
        if (ControllerR.GetHairTrigger())
        {
            if (ControllerR.velocity.magnitude > 3f)
            {
                Debug.Log("SAĞI SALLADIM KIRDIM BEBEYİM");
            }
        }
    }
    private void CheckGripInput()
    {
        if (ControllerL.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            ChangeDashboardVisibleAfterFrame();
        }
        
    }
    private void ChangeDashboardVisibility(List<Text> texts, List<Image> images, float visibilityAmount)
    {
        foreach (var item in images)
        {
            item.color = new Color(1f, 1f, 1f, visibilityAmount);
        }
        foreach (var item in texts)
        {
            item.color = new Color(1f, 1f, 1f, visibilityAmount);
        }
    }
    public void ChangeDashboardVisibleAfterFrame()
    {
        if (IsDashboardVisible == true)
        {
            StartCoroutine(DashboardVisible(false));
            ChangeDashboardVisibility(dashBoardTextElements, dashBoardImageElements, .05f);
        }
        else
        {
            StartCoroutine(DashboardVisible(true));
            ChangeDashboardVisibility(dashBoardTextElements, dashBoardImageElements, 1f);
        }
    }
    IEnumerator DashboardVisible(bool state)
    {
        yield return new WaitForSeconds(.1f);
        IsDashboardVisible = state;
    }
    public void Up(float side)
    {
        if (Player.IsGameRunning())
        {
            
            AccelerationY = Mathf.Pow(YMovementConstant_3 * (Time.deltaTime + 0.03f), YMovementConstant_1 / YMovementConstant_2);
            if (side < 0)
            {
                AccelerationY *= 4f;
            }
            switch (side)
            {
                case 1:
                    playerTransform.GetComponent<Rigidbody>().velocity += Vector3.up *AccelerationY * PowerUpController.BulletTimeMultipleValue;
                    break;
                case -1:
                   playerTransform.GetComponent<Rigidbody>().velocity += Vector3.down * AccelerationY * PowerUpController.BulletTimeMultipleValue;
                    break;
                default:
                    break;
            }
            
        }
    }
}
