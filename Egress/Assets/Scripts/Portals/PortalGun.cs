using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour {
    public GameObject yellowAmmo;
    public GameObject purpleAmmo;
    public float force = 10f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject inst = Instantiate(yellowAmmo, transform.position + transform.forward * 2, transform.rotation);
            Rigidbody prb = inst.GetComponent<Rigidbody>();
            prb.AddRelativeForce(new Vector3(0, 0, force), ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(1)) {
            GameObject inst = Instantiate(purpleAmmo, transform.position + transform.forward * 2, transform.rotation);
            Rigidbody prb = inst.GetComponent<Rigidbody>();
            prb.AddRelativeForce(new Vector3(0, 0, force), ForceMode.Impulse);
        }
    }
}
