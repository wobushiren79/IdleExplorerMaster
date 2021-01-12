/*
* FileName: UserGroundData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:53:59 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UserSceneDataModel : BaseMVCModel
{
    protected UserSceneDataService serviceUserGroundData;

    public override void InitData()
    {
        serviceUserGroundData = new UserSceneDataService();
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns></returns>
    public List<UserSceneDataBean> GetAllUserSceneDataData()
    {
        List<UserSceneDataBean> listData = serviceUserGroundData.QueryAllData();
        return listData;
    }

    /// <summary>
    /// 根据ID获取数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UserSceneDataBean GetUserSceneDataDataById(string groundId)
    {
        return serviceUserGroundData.QueryDataById(groundId);
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <param name="data"></param>
    public void SetUserSceneDataData(UserSceneDataBean data)
    {
        serviceUserGroundData.UpdateData(data);
    }

}