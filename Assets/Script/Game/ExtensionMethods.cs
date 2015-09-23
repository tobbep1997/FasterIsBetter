using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
//	public static void SetCharacterSpeed(this CharacterController cc,float speed)
//	{
//		cc.moveSpeed = speed;//This is a test its not acualy used
//	}
	public static bool IsBigger(this Vector2 v1,Vector2 v2)
	{
		float vf1 = (v1.x + v1.y)/2;
		float vf2 = (v2.x + v2.y)/2;
		if (vf1 > vf2) {
			return true;
		}
		else
			return false;
	}
	public static bool AnyInstanceIsEqual<T>(this T[] inputObj,T checkObj)
		where T : Object
	{
		for (int i = 0; i < inputObj.Length; i++) {
			if (inputObj[i] == checkObj) {
				return true;
			}
		}
		return false;
	}
	public static bool AnyInstanceIsEqual<T>(this System.Collections.Generic.List<T> inputList, T checkObject)
		where T : Object
	{
		foreach (T item in inputList) {
			if (item == checkObject) {
				return true;
			}
		}
		return false;
	}
}
