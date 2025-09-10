using UnityEngine;

public class Escalade : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool escalade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical");
        if (escalade && translation != 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, translation);
        }
        else if (escalade && translation == 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            escalade = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            escalade = false;
        }
    }
}
