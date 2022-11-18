using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour {
    public GameObject ammo;
    public GameObject[] portals;
    public float force = 10f;

    bool[] active = {false, false};

    /**
     * Link the two portals together
     * @param unlink - Optional param to unlink portals instead
     */
    void LinkPortals(int unlink = -1) {
        Portal p = portals[0].GetComponent<Portal>();
        Portal y = portals[1].GetComponent<Portal>();

        p.linkedPortal = y;
        y.linkedPortal = p;

        // give option to unlink portals
        if (unlink > -1) {
            p.linkedPortal = null;
            y.linkedPortal = null;
        }

        if (unlink == 0) {
            p.transform.position = new Vector3(100, 100, 100);
        }

        if (unlink == 1) {
            y.transform.position = new Vector3(100, 100, 100);
        }
    }

    /**
     * Create a projectile that will spawn a portal
     * @param type - Which portal should be spawned. Integer, either 0 or 1
     */
    void CreateProjectile(int type) {
        // instantiate slightly in front to bypass wall for now.
        GameObject inst = Instantiate(ammo, transform.position + transform.forward * 2, transform.rotation);

        // set type of ammo
        Projectile p = inst.GetComponent<Projectile>();
        p.portal = portals[type];
        p.type = (type == 1);

        Rigidbody prb = inst.GetComponent<Rigidbody>();
        prb.AddRelativeForce(new Vector3(0, 0, force), ForceMode.Impulse);

        if (active[(type + 1) % 2] && !active[type]) {
            LinkPortals();
        }
        active[type] = true;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // purple
        if (Input.GetMouseButtonDown(0)) {
            CreateProjectile(0);
        }

        // yellow
        if (Input.GetMouseButtonDown(1)) {
            CreateProjectile(1);
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            LinkPortals(0);
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            LinkPortals(1);
        }
    }
}
