using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    private int intCurrentPlayer = 0;
    private GameObject previousPlayer;
    private GameObject currentPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    /*void Awake()
     {
         players[1].GetComponent<Respawn>().enabled = false; //Pour je ne sais 
         players[2].GetComponent<Respawn>().enabled = false;
     }*/
    void Start()
    {
        currentPlayer = players[intCurrentPlayer]; //Met le pingoin 0 du tableau comme joueur actuel (dû à la ligne 6 du script)
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
        currentPlayer.GetComponent<BasePlayerController>().enabled = true; //Active les scripts du nouveau pingouin pour le rendre controlable et affecté par les trigget
        currentPlayer.GetComponent<ThrowPlayer>().enabled = true;
        currentPlayer.GetComponent<Respawn>().enabled = true;
        currentPlayer.GetComponent<Respawn>()._isActive = true;

        if (intCurrentPlayer == 0)
        {
            previousPlayer.GetComponent<Escalade>().enabled = false;
        }
        if (intCurrentPlayer == 1)
        {
            currentPlayer.GetComponent<Shooting>().enabled = true;
        }
        else if (intCurrentPlayer == 2)
        {
            previousPlayer.GetComponent<Shooting>().enabled = false;
            currentPlayer.GetComponent<Escalade>().enabled = true;
        }
    }
    
}
