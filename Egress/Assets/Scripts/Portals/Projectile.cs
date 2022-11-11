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
            // move portal to wall
            // same position, move back by .2 to get it closer to wall
            portal.transform.position = transform.position - (c.gameObject.transform.forward * 0.2f);

            // make portal vertical
            portal.transform.rotation = c.gameObject.transform.rotation;
            portal.transform.Rotate(new Vector3(0, 0, 90), Space.Self);

            // if secondary portal, rotate 180 degrees, to have portals facing opposite directions
            if (type) {
                portal.transform.Rotate(new Vector3(0, 180, 0), Space.World);
            }
            // portal.transform.Rotate(c.gameObject.transform.forward * 90, Space.World);

            // Attach wall to portal
            Portal p = portal.GetComponent<Portal>();
            p.wall = c.gameObject;
        }

        Destroy(this.gameObject);
    }
}
