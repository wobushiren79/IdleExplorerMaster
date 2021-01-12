/*
* FileName: UserGroundData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:53:59 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface IUserSceneDataView
{
	void GetUserSceneDataSuccess<T>(T data, Action<T> action);

	void GetUserSceneDataFail(string failMsg, Action action);
}