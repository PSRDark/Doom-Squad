using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    public Vector3 direction = Vector3.left;
    public LayerMask MasksToRay;
    public GameObject bullet;
    public float TtoShoot;
    private float t;
    public int weaponDamage;

    void Update ()
    {

        t -= Time.deltaTime;

        if (t <= 0 && Input.GetKey(KeyCode.Space))
        {
            t = TtoShoot;
            if (Physics2D.Raycast(transform.position, direction, 1000f, MasksToRay))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1000f, MasksToRay);

                IChecker.IDamageable(hit.collider).TakeDamage(weaponDamage);

                GameObject insbullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;

                LineRenderer bulletRenderer = insbullet.GetComponent<LineRenderer>();
                bulletRenderer.SetPosition(0, transform.position);
                bulletRenderer.SetPosition(1, hit.point);

                Destroy(insbullet, 0.1f);
            }
            else
            {
                GameObject insbullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;

                LineRenderer bulletRenderer = insbullet.GetComponent<LineRenderer>();
                bulletRenderer.SetPosition(0, transform.position);
                bulletRenderer.SetPosition(1, direction * 1000);

                Destroy(insbullet, 0.1f);
            }
        }
	}
}
