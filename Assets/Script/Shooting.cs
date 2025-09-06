using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public GameObject projectile;
    public float atkSpeed;
    private Coroutine fireCoroutine;
    private bool canShoot = true;
    private bool fireCancel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(Fire());
        }
        else if (Input.GetMouseButtonDown(0) && !canShoot && fireCoroutine == null)
        {
            fireCancel = true;
        }
        else if (Input.GetMouseButtonUp(0) && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
            StartCoroutine(FireRate());
        }
    }

    public IEnumerator Fire()
    {

        while (true)
        {
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            yield return new WaitForSeconds(atkSpeed);
        }
    }

    public IEnumerator FireRate()
    {
        canShoot = false;
        yield return new WaitForSeconds(atkSpeed);
        canShoot = true;
        if (fireCancel && Input.GetMouseButton(0))
        {
            fireCancel = false;
            fireCoroutine = StartCoroutine(Fire());
        }
    }
}
