/*
* FileName: UserGroundData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:53:59 
*/

using UnityEngine;
using UnityEditor;
using System;
using NUnit.Framework;
using System.Collections.Generic;

[Serializable]
public class UserSceneDataBean : BaseBean
{
    public string groundId;
    public int groundX;
    public int groundZ;

    public List<CampDataBean> listCampData;

    public List<GroundHexagonsBean> listGroundData;


    public UserSceneDataBean(int groundX, int groundZ, int playerNumber)
    {
        this.groundX = groundX;
        this.groundZ = groundZ;
        groundId = SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        listCampData = new List<CampDataBean>() ;
        for (int i = 0; i < playerNumber + 1; i++)
        {
            CampDataBean campData = new CampDataBean();
            if (i == 0)
            {
                campData.isPlayer = true;
            }
            listCampData.Add(campData);
        }
    }

    /// <summary>
    /// 获取玩家阵营数据
    /// </summary>
    /// <returns></returns>
    public CampDataBean GetPlayerCampData()
    {
        for (int i = 0; i < listCampData.Count; i++)
        {
            CampDataBean itemCampData = listCampData[i];
            if (itemCampData.isPlayer)
            {
                return itemCampData;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取玩家阵营ID
    /// </summary>
    /// <returns></returns>
    public string GetPlayerBelongId()
    {
        CampDataBean campData = GetPlayerCampData();
        if (campData != null)
            return campData.belongId;
        return null;
    }


    /// <summary>
    /// 设置地面数据
    /// </summary>
    /// <param name="listData"></param>
    public void SetGroundData(List<GroundHexagonsBean> listGroundData)
    {
        this.listGroundData = listGroundData;
    }
}