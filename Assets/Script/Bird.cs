using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool direction = true;

    // Update is called once per frame
    private void Update()
    {
        if (direction)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
        if (!other.gameObject.CompareTag("Player"))
        {
            if (direction)
            {
                transform.localScale = new Vector3(0.15f, -0.15f, 0.15f);
            }
            else
            {
                transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            }
            direction = !direction;
        }
    }
}
