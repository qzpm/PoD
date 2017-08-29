using UnityEngine;
using System;

/// <summary>
/// GameObjectクラスの拡張
/// </summary>
public static class GameObjectExtension
{
	/// <summary>
	/// null safety
	/// </summary>
	/// <param name="self"></param>
	/// <param name="value"></param>
	public static void SafeActive(this GameObject self, bool value)
	{
		if (self != null) self.SetActive(value);
	}


	/// <summary>
	/// null safety
	/// </summary>
	/// <param name="self"></param>
	/// <param name="value"></param>
	public static void SafeActive(this MonoBehaviour self, bool value)
	{
		if (self != null) self.gameObject.SetActive(value);
	}

	/// <summary>
	/// null safety
	/// </summary>
	/// <param name="self"></param>
	/// <param name="value"></param>
	public static void SafeActive(this GameObject[] self, bool value)
	{
		if (self != null)
		{
			foreach( GameObject gmo in self ) gmo.SetActive(value);
		}
	}
}