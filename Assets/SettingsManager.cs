using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    public List<float> renderScales;
    public Text renderScaleTxt;
    public static int currentRenderScaleIndex;
    public static float currenRenderScale = 1;
    public static bool isShadowsEnable;
    public List<Button> buttons;
    public Animator shadowSwitchBtnAnimator;
    public Scrollbar scrollbarMusicVolume;
    public Scrollbar scrollbarSoundEffects;
    public static float musicVolume;
    public static float effectVolume;
    public static bool IsShadowsEnable()
    {
        return isShadowsEnable;
    }
    public void ChangeShadowsSetting()
    {
        isShadowsEnable = !isShadowsEnable;
        shadowSwitchBtnAnimator.SetBool("isShadowsEnable", IsShadowsEnable());
    }
   
    private void Start()
    {
        scrollbarSoundEffects.value = PlayerPrefs.GetFloat("effectVolume");
        scrollbarMusicVolume.value = PlayerPrefs.GetFloat("musicVolume");
        renderScaleTxt.text = renderScales[currentRenderScaleIndex].ToString();
    }
    public void SetVolume()
    {
        musicVolume = scrollbarMusicVolume.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        effectVolume = scrollbarSoundEffects.value;
        PlayerPrefs.SetFloat("effectVolume", effectVolume);
    }
    public void ChangeRenderQuality(int btnIndex)
    {

        currentRenderScaleIndex += btnIndex;
        renderScaleTxt.text = renderScales[currentRenderScaleIndex].ToString();
        currenRenderScale = renderScales[currentRenderScaleIndex];
        if (currentRenderScaleIndex == 0)
        {
            buttons[0].enabled = false;
        }
        else
        {
            buttons[0].enabled = true;
        }
        if (currentRenderScaleIndex == renderScales.Count-1)
        {
            buttons[1].enabled = false;
        }
        else
        {
            buttons[1].enabled = true;
        }
    }
}
