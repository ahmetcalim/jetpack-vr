using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme : MonoBehaviour
{

    public string themeName;
    public List<TunnelPart> themeParts;
    public ThemeRuleSet themeRules;

    public TunnelPart GetRandomTunnelPart()
    {
        if (AppLogic.currentTunnelPart == null)
        {
            if(themeRules.head==null)
            {
                return themeParts[Random.Range(0, themeParts.Count)];
            }
            else
            {
                return themeRules.head;
            }
        }
        return null;
    }
    public void InitialiseThemeParts()
    {
        themeParts = new List<TunnelPart>();
        foreach(TunnelPart t in this.transform.GetComponentsInChildren<TunnelPart>(true))
        {
            themeParts.Add(t);
        }
    }
    public static List<Theme> GetAllThemes(Transform themesParentObj)
    {
        List<Theme> themes = new List<Theme>();
        foreach(Theme t in themesParentObj.GetComponentsInChildren<Theme>(true))
        {
            themes.Add(t);
        }

        if(themes.Count>0)
            return themes;
        else
        {
            Debug.LogError("There is no themes found in children transforms!!!");
            return null;
        }
    }


    

}
