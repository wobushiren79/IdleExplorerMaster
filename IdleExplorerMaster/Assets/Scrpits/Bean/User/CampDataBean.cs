using UnityEditor;
using UnityEngine;
using System;

[Serializable]
public class CampDataBean
{
    //归属ID
    public string belongId;
    //阵营颜色
    public ColorBean campColor;
    //是否玩家操作
    public bool isPlayer = false;

    public CampDataBean()
    {
        belongId = "Camp_" + SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        SetCampColor(new Color(UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1)));
    }

    public void SetCampColor(Color color)
    {
        campColor = new ColorBean(color.r, color.g, color.b);
    }

    public Color GetCampColor()
    {
        return campColor.GetColor();
    }
}