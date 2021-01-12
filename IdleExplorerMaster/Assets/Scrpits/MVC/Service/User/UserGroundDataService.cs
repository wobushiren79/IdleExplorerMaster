/*
* FileName: UserGroundData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:53:59 
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class UserGroundDataService : BaseDataStorage<UserGroundDataBean>
{
    protected string saveFileName;

    public UserGroundDataService()
    {
        saveFileName = "UserGroundData";
    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<UserGroundDataBean> QueryAllData()
    {
        return null;
    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UserGroundDataBean QueryDataById(string groundId)
    {
        return BaseLoadData("UserGroundData_" + groundId);
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    public void UpdateData(UserGroundDataBean data)
    {
        saveFileName = "UserGroundData_" + data.groundId;
        BaseSaveData(saveFileName, data);
    }
}