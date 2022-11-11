using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject portal;
    public bool type;

    // Start is called before the first frame update
    void Start() {
        // Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision c) {
        if (c.gameObject.CompareTag("portable")) {
            portal.transform.position = transform.position + c.gameObject.transform.forward;
            portal.transform.rotation = c.gameObject.transform.rotation;
            Debug.Log(c.gameObject.transform.rotation[0]);
            portal.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            if (type) {
                portal.transform.Rotate(new Vector3(0, 180, 0), Space.World);
            }
            // portal.transform.Rotate(c.gameObject.transform.forward * 90, Space.World);
        }

        Destroy(this.gameObject);
    }
}
