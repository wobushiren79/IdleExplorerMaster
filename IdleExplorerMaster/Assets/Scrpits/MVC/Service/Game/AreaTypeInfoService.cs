/*
* FileName: AreaTypeInfo 
* Author: AppleCoffee 
* CreateTime: 2021-01-08-14:54:31 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AreaTypeInfoService : BaseMVCService
{
    public AreaTypeInfoService() : base("area_type", "")
    {

    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<AreaTypeInfoBean> QueryAllData()
    {
        List<AreaTypeInfoBean> listData = BaseQueryAllData<AreaTypeInfoBean>();
        return listData; 
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <returns></returns>
    public AreaTypeInfoBean QueryData()
    {
        return null; 
    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<AreaTypeInfoBean> QueryDataById(long id)
    {
        return BaseQueryData<AreaTypeInfoBean>("link_id", "id", id + "");
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool UpdateData(AreaTypeInfoBean data)
    {
        bool deleteState = BaseDeleteDataById(data.id);
        if (deleteState)
        {
            bool insertSuccess = BaseInsertData(tableNameForMain, data);
            return insertSuccess;
        }
        return false;
    }
}