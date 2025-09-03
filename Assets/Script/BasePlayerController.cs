using UnityEngine;

public class BasePlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 jumpDirection;

    [Header("Deplacement")]
    public float moveSpeed;
    public float jumpForce;
    private bool isGrounded = true;
    public bool canJump = true;

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

        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
        {
            rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        }

        if (translation < 0)
        {
            transform.localScale = new Vector3(-0.15f, 0.15f, 0.15f);
        }
        else if (translation > 0)
        {
            transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
    }
}
