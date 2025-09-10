using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerChange : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    private int intCurrentPlayer = 0;
    private GameObject previousPlayer;
    public GameObject currentPlayer;
    public GameObject pistolet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    /*void Awake()
     {
         players[1].GetComponent<Respawn>().enabled = false; //Pour je ne sais 
         players[2].GetComponent<Respawn>().enabled = false;
     }*/
    void Start()
    {
        //currentPlayer = players[intCurrentPlayer]; //Met le pingoin 0 du tableau comme joueur actuel (dû à la ligne 6 du script)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !players[intCurrentPlayer].GetComponent<ThrowPlayer>().isGrabbingSomeone) //Permet de passer de pingouin vers l'arrière du tableau
        {
            previousPlayer = players[intCurrentPlayer];
            intCurrentPlayer--;
            if (intCurrentPlayer < 0) intCurrentPlayer = 2;
            ChangePlayer();
        }

        if (Input.GetKeyDown(KeyCode.E) && !players[intCurrentPlayer].GetComponent<ThrowPlayer>().isGrabbingSomeone) //Permet de passer de pingouin vers l'avant du tableau
        {
            previousPlayer = players[intCurrentPlayer];
            intCurrentPlayer++;
            if (intCurrentPlayer > 2) intCurrentPlayer = 0;
            ChangePlayer();
        }
    }

    private void ChangePlayer() //S'occupe de changer de pingouin à controler
    {
        currentPlayer = players[intCurrentPlayer];
        GameObject Camera = previousPlayer.transform.GetChild(0).gameObject; //Fais passer la camera qui est en enfant du pingouin controllé en enfant du futur pingouin controllé
        Camera.transform.parent = currentPlayer.transform;
        Camera.transform.position = new Vector3(currentPlayer.transform.position.x, currentPlayer.transform.position.y, -10); //Repositionne la camera correctement 
        previousPlayer.GetComponent<BasePlayerController>().enabled = false; //Desactive les scripts de l'ancien pingouin pour qu'il ne soit plus controllable ni affecté par les trigger
        previousPlayer.GetComponent<ThrowPlayer>().enabled = false;
        previousPlayer.GetComponent<Respawn>().enabled = false;
        previousPlayer.GetComponent<Respawn>()._isActive = false;
        GetPower(previousPlayer);
        currentPlayer.GetComponent<BasePlayerController>().enabled = true; //Active les scripts du nouveau pingouin pour le rendre controlable et affecté par les trigget
        currentPlayer.GetComponent<ThrowPlayer>().enabled = true;
        currentPlayer.GetComponent<Respawn>().enabled = true;
        currentPlayer.GetComponent<Respawn>()._isActive = true;
        GetPower(currentPlayer);
    }


    public void ResetPlayers()
    {
        intCurrentPlayer = 0;
        System.Array.Clear(players, 0, players.Length);
        players = GameObject.FindGameObjectsWithTag("Player");
        currentPlayer = players[intCurrentPlayer];
    }

    private void GetPower(GameObject Penguin)
    {
        if (Penguin.GetComponent<Escalade>() != null) Penguin.GetComponent<Escalade>().enabled = !Penguin.GetComponent<Escalade>().enabled;
        else if (Penguin.GetComponent<Shooting>() != null)
        {
            Penguin.GetComponent<Shooting>().enabled = !Penguin.GetComponent<Shooting>().enabled;
            pistolet.GetComponent<Pistolet>().enabled = !pistolet.GetComponent<Pistolet>().enabled;
        }
    }
}
