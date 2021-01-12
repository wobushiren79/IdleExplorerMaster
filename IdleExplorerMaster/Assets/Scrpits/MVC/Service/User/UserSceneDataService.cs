/*
* FileName: UserGroundData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:53:59 
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class UserSceneDataService : BaseDataStorage<UserSceneDataBean>
{
    protected string saveFileName;

    public UserSceneDataService()
    {
        saveFileName = "UserSceneData_";
    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<UserSceneDataBean> QueryAllData()
    {
        return null;
    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UserSceneDataBean QueryDataById(string groundId)
    {
        return BaseLoadData(saveFileName + groundId);
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    public void UpdateData(UserSceneDataBean data)
    {
        saveFileName = saveFileName + data.groundId;
        BaseSaveData(saveFileName, data);
    }
}