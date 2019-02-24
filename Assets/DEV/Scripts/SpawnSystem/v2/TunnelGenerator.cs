using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator : MonoBehaviour
{
    private List<Theme> themes;
    private Theme currentTheme;

    public GameObject cube;

    public void Start()
    {
        themes = new List<Theme>();
    }


    public void GenerateTunnel(Player currPlayer,Transform themesParentObj,int size)
    {
        Vector3 currPosition = currPlayer.transform.position;
        themes = Theme.GetAllThemes(themesParentObj);
        currentTheme = themes[Random.Range(0, themes.Count)];
        currentTheme.InitialiseThemeParts();
       
        //pick random tunnelpart from theme 
        for(int i = 0; i < size; i++)
        {
            TunnelPart t= currentTheme.GetRandomTunnelPart();
            t.transform.gameObject.SetActive(true);
            Debug.Log("Benim bulduğum tünel: " + t.transform.name);
            AppLogic.nextAttachmentPoint=new Vector3(AppLogic.nextAttachmentPoint.x, AppLogic.nextAttachmentPoint.y, AppLogic.nextAttachmentPoint.z + t.transform.GetComponent<Renderer>().bounds.extents.z);
            t.transform.position = AppLogic.nextAttachmentPoint;
            cube.transform.position = t.transform.position;


            currPlayer.playerPawnTransform.transform.position = t.transform.GetComponent<Renderer>().bounds.center;
           




            

            //bir sonrakinin attach edileceği konumu bul
        }

    }

}
