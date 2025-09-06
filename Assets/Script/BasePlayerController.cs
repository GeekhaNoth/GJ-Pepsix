using UnityEngine;

public class BasePlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 jumpDirection;
    private bool isFlipped = false;
    private bool isTouchingWall;

    [Header("Deplacement")]
    public float moveSpeed;
    public float jumpForce;
    private float translation;
    [SerializeField] private bool isGrounded = true;
    public bool canJump = true;

    [Header("A ajuster")]
    public float taillePingouin;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpDirection = new Vector2(0, jumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        translation = Input.GetAxis("Horizontal");
        if (!isTouchingWall)
        {
            rb.linearVelocity = new Vector2(translation * moveSpeed, rb.linearVelocity.y);
        }


        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, taillePingouin, groundLayer); //Verifie si le pingouin touche le sol


        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
        {
            rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        }

        if (translation < 0)
        {
            transform.localScale = new Vector3(-0.18f, 0.18f, 0.18f);
            isFlipped = true;
        }
        else if (translation > 0)
        {
            transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);
            isFlipped = false;
        }
    }


    void OnDrawGizmos() //donne un retour visuel sur le raycast
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * taillePingouin);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (isFlipped && translation < 0)
            {
                isTouchingWall = true;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            else if (!isFlipped && translation > 0)
            {
                isTouchingWall = true;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");
        isTouchingWall = false;
    }
}
