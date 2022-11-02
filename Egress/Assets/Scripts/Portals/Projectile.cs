using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject portal;

    // Start is called before the first frame update
    void Start() {
        // Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision c) {
        if (c.gameObject.CompareTag("portable")) {
            portal.transform.position = transform.position;
            portal.transform.rotation = c.gameObject.transform.rotation;
            portal.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            // portal.transform.Rotate(c.gameObject.transform.forward * 90, Space.World);
        }

        Destroy(this.gameObject);
    }
}
