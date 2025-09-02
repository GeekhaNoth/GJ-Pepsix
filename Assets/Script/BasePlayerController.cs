using UnityEngine;

public class BasePlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 jumpDirection;

    [Header("Deplacement")]
    public float moveSpeed;
    public float jumpForce;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpDirection = new Vector2(0, jumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * moveSpeed;
        transform.Translate(translation, 0, 0 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        }
    }
}
