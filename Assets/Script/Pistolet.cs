using UnityEngine;

public class Pistolet : MonoBehaviour
{
    public float tailleXPistolet = 1.4f;
    public float tailleYPistolet = 0.23f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(tailleXPistolet, -tailleYPistolet, 1); // flip vertical
        }
        else
        {
            transform.localScale = new Vector3(tailleXPistolet, tailleYPistolet, 1); // normal
        }
    }
}
