/*
* FileName: GameData 
* Author: AppleCoffee 
* CreateTime: 2021-01-11-10:51:44 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface IGameDataView
{
	void GetGameDataSuccess<T>(T data, Action<T> action);

	void GetGameDataFail(string failMsg, Action action);
}