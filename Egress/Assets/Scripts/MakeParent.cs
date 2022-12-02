//Tyler Chrnko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakeParent : MonoBehaviour
{
	//This code is to make it so the player doesn't need to follow the moving platforms movement

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Player")
		{
			collision.gameObject.transform.SetParent(transform);
			//sets it so player is transforming to the speed of the platform on collision
		}
	}
	private void OnCollisionExit(Collision collision)
	{
			if (collision.gameObject.name == "Player")
			{
				collision.gameObject.transform.SetParent(null);
			//when player jumps off no longer moving with platform
			}
	}
	
}
