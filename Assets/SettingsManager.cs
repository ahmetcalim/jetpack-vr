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
    public List<Button> buttons;
    private void Start()
    {
        renderScaleTxt.text = renderScales[currentRenderScaleIndex].ToString();
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
