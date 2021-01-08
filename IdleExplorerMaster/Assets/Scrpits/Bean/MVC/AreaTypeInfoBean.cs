/*
* FileName: AreaTypeInfo 
* Author: AppleCoffee 
* CreateTime: 2021-01-08-14:54:31 
*/

using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class AreaTypeInfoBean : BaseBean
{
    public int area_type;

    public string model_name;

    public AreaTypeEnum GetAreaType()
    {
        return EnumUtil.GetEnum<AreaTypeEnum>(area_type);
    }
}