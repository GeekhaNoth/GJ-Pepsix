using UnityEngine;

public class ThrowPlayer : MonoBehaviour
{
    [Header("Taille Joueur")]
    //public float playerLargeur;
    //public float playerHauteur;
    public float grabRange;
    private GameObject[] players;
    private float closestPlayer;
    private int playerIndex = 0;


    [Header("Throw Relative")]
    private Rigidbody2D grabbedRb;
    public bool isGrabbingSomeone = false;
    public GameObject throwHoldingSpot;
    public KeyCode touchePourLancer = KeyCode.F;
    
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrabbingSomeone && GetComponent<Shooting>() != null && GetComponent<Shooting>().enabled == false) GetComponent<Shooting>().enabled = true;
        if (Input.GetKeyDown(touchePourLancer) && !isGrabbingSomeone)
        {
            closestPlayer = Mathf.Infinity;
            float distancePlayer;
            int i = 0;
            foreach (GameObject player in players)
            {
                distancePlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distancePlayer < closestPlayer && player != this.gameObject)
                {
                    closestPlayer = distancePlayer;
                    playerIndex = i;
                }
                i++;
            }
            if (closestPlayer < grabRange)
            {
                players[playerIndex].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                players[playerIndex].transform.position = throwHoldingSpot.transform.position;
                players[playerIndex].transform.SetParent(this.transform);
                isGrabbingSomeone = true;
                this.GetComponent<BasePlayerController>().canJump = false;
            }
        }

        if (isGrabbingSomeone)
        {
            if (GetComponent<Shooting>() != null) GetComponent<Shooting>().enabled = false;
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 throwDirection = (mousePos - transform.position);

                players[playerIndex].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                players[playerIndex].transform.SetParent(this.transform.parent);
                grabbedRb = players[playerIndex].GetComponent<Rigidbody2D>();
                grabbedRb.AddForce(throwDirection * 2, ForceMode2D.Impulse);
                this.GetComponent<BasePlayerController>().canJump = true;
                isGrabbingSomeone = false;
            }
        }
    }
}
