using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour{

	public delegate void PlayerAttack();
	public static event PlayerAttack OnPlayerAttack;	

	public delegate void PlayerInteract();
	public static event PlayerInteract OnPlayerInteract;

	public static void CallPlayerAttack()
	{
		if (OnPlayerAttack != null)
			OnPlayerAttack();
	}

	public static void CallPlayerInteract()
	{
		if (OnPlayerInteract != null) {
			OnPlayerInteract();
		}
	}

}
