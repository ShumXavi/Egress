//Tyler Chrnko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakeParent : MonoBehaviour
{
	//This code is to make it so the player doesn't need to follow the moving platforms movement


	public GameObject tracker;

	public Vector3 StoredScale;



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
		//Debug.Log("apple");
    }

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log("Ride");
		if (other.gameObject.CompareTag("Player"))
		{

			GameObject obj = other.gameObject;
			obj.transform.SetParent(tracker.transform, true);

		}
		if (other.gameObject.CompareTag("Box"))
		{
			if (!other.gameObject.GetComponent<PortalPhysObject>().held)
			{
				GameObject obj = other.gameObject;
				StoredScale = obj.transform.lossyScale;
				tracker.transform.localScale = StoredScale;
				obj.transform.SetParent(tracker.transform, true);
			}
		}
	}
    private void OnTriggerStay(Collider other)
    {
		if(other.transform.parent == null)
        {
			if (other.gameObject.CompareTag("Player"))
			{
				GameObject obj = other.gameObject;
				obj.transform.SetParent(tracker.transform, true);

			}
            if (other.gameObject.CompareTag("Box"))
            {
                if (!other.gameObject.GetComponent<PortalPhysObject>().held)
                {
					GameObject obj = other.gameObject;
					StoredScale = obj.transform.lossyScale;
					tracker.transform.localScale = StoredScale;
					obj.transform.SetParent(tracker.transform, true);
				}
            }
		}

	}
    void OnTriggerExit(Collider other)
	{
		Debug.Log("Bye");

		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Grape");
			other.transform.SetParent(null);
		}
		if (other.gameObject.CompareTag("Box"))
		{
			if (!other.gameObject.GetComponent<PortalPhysObject>().held)
			{
				other.gameObject.transform.SetParent(null);
				other.gameObject.transform.localScale = StoredScale;
			}
            else
            {
				//other.gameObject.transform.localScale = StoredScale;
			}
		}
	}
	private void OnCollisionExit(Collision collision)
	{
			

	}
	
}
