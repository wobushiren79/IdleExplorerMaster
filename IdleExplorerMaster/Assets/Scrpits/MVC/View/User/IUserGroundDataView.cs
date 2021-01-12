/*
* FileName: UserGroundData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:53:59 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface IUserGroundDataView
{
	void GetUserGroundDataSuccess<T>(T data, Action<T> action);

	void GetUserGroundDataFail(string failMsg, Action action);
}