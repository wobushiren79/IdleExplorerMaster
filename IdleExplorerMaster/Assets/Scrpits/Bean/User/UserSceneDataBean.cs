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

    public string playerId;
    public List<string> enemyId;

    public UserSceneDataBean(int groundX, int groundZ)
    {
        this.groundX = groundX;
        this.groundZ = groundZ;
        groundId = SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        playerId = "Player_" + SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
    }
}