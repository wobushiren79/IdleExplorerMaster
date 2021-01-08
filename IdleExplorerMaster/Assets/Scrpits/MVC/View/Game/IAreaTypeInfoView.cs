/*
* FileName: AreaTypeInfo 
* Author: AppleCoffee 
* CreateTime: 2021-01-08-14:54:31 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface IAreaTypeInfoView
{
	void GetAreaTypeInfoSuccess<T>(T data, Action<T> action);

	void GetAreaTypeInfoFail(string failMsg, Action action);
}