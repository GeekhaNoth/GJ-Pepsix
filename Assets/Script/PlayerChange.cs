using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    private int intCurrentPlayer = 0;
    private GameObject previousPlayer;
    private GameObject currentPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayer = players[intCurrentPlayer];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            previousPlayer = players[intCurrentPlayer];
            intCurrentPlayer--;
            if (intCurrentPlayer < 0) intCurrentPlayer = 2;
            ChangePlayer();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            previousPlayer = players[intCurrentPlayer];
            intCurrentPlayer++;
            if (intCurrentPlayer > 2) intCurrentPlayer = 0;
            ChangePlayer();
        }
    }

    private void ChangePlayer()
    {
        currentPlayer = players[intCurrentPlayer];
        GameObject Camera = previousPlayer.transform.GetChild(0).gameObject;
        Camera.transform.parent = currentPlayer.transform;
        Camera.transform.position = new Vector3(currentPlayer.transform.position.x, currentPlayer.transform.position.y, -10);
        previousPlayer.GetComponent<BasePlayerController>().enabled = false;
        previousPlayer.GetComponent<Respawn>().enabled = false;
        currentPlayer.GetComponent<BasePlayerController>().enabled = true;
        currentPlayer.GetComponent<Respawn>().enabled = true;
    }
    
}
