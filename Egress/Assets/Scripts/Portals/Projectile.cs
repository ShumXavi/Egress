using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject portal;
    public GameObject otherPortal;
    public bool type;
    public float defaultLifetime = 5;
    float lifetime;

    Portal portalObject;
    Portal otherPortalObject;

    // Start is called before the first frame update
    void Start() {
        lifetime = defaultLifetime;
        // Destroy(this.gameObject, 5f);

        portalObject = portal.GetComponent<Portal>();
        otherPortalObject = otherPortal.GetComponent<Portal>();
    }

    // Update is called once per frame
    void Update() {
        if (lifetime <= 0) {
            Destroy(this.gameObject);
        }

        lifetime -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision c) {
        if (c.gameObject.CompareTag("portable")) {
            // Attach wall to portal
            portalObject.wall = c.gameObject;

            // move portal to wall
            // same position, move back by .2 to get it closer to wall
            portal.transform.position = transform.position - (c.gameObject.transform.forward * 0.1f);
            //portal.transform.position = transform.position;

            portalObject.adjustPosition();

            SurfaceTypes wallType = c.gameObject.GetComponent<SurfaceType>().type;
            SurfaceTypes otherWallType = otherPortalObject.wall != null
                ? otherPortalObject.wall.GetComponent<SurfaceType>().type
                : SurfaceTypes.NONE;

            portalObject.SetRotation(type, wallType, otherWallType);
            otherPortalObject.SetRotation(!type, otherWallType, wallType);
        } else if (c.gameObject.CompareTag("reflective")) {
            // add reflective force
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.Reflect(transform.position, c.gameObject.transform.forward), ForceMode.Impulse);

            // reset lifetime of object
            lifetime = defaultLifetime;
            return;
        }

        Destroy(this.gameObject);
    }
}
