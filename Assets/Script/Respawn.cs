using System;
using System.Collections;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class Respawn : MonoBehaviour
{
    public GameManager manager;
    public bool _isActive = false;
    
    private bool _isHiddenActive = false;
    private void Start()
    {
        if (!_isActive) return;
        manager._lastCheckpoint = transform.position; //Là où le joueur apparait au debut devient le 1er checkpoint
        manager._firstCheckpoint = transform.position;
    }
    
    
   private void OnTriggerEnter2D(Collider2D other)
   {
        //Pour une raison obscur, au demarrage du jeu ce script est actif pendant 1 frame sur les pingouin non controlés, cette ligne evite qu'il active des checkpoint ou des mort pendant la frame où ils sont actifs 
       if (other.CompareTag("CheckPoint")) //Si le joueur trigger un checkpoint, l'active comme nouveau point de respawn et desactive le collider pour eviter que le joueur revienne en arrière et reactive des checkpoint precedents
       {
           manager._lastCheckpoint = other.transform.position;
           other.GetComponent<BoxCollider2D>().enabled = false;
       }
       if (other.CompareTag("DeathZone"))
       {
           transform.position = manager._lastCheckpoint; //Si le joueur trigger une deathzone il reapparait au dernier checkpoint
           manager._nmbrOfDeath++;
       }

       if (other.CompareTag("HiddenZone")) //Si le joueur trigger un faux-mur ou faux-sol, fait apparaitre ou disparaitre la piece secrète en fonction de son etat
       {
           _isHiddenActive = !_isHiddenActive;
           other.transform.GetChild(0).gameObject.SetActive(_isHiddenActive);
       }
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
       if (other.collider.CompareTag("Mobs")) 
       {
           transform.position = manager._lastCheckpoint; //Si le joueur rentre en collision avec un mob il respawn au dernier checkpoint
           manager._nmbrOfDeath++;
       }
       if (other.collider.CompareTag("Pepsi"))
       {
           other.gameObject.SetActive(false);
           manager._nmbrPepsiUnlock++;
           if (other.gameObject.name == "FinalPepsi") //Nommer le pepsi de fin niveau ainsi pour lancer la fin du niveau
           {
               manager.FinishLevel();
               manager.Camera.GetComponent<CameraScript>().enabled = true;
               manager.FinishMenu.SetActive(true);
               manager._textPepsi.text = manager._nmbrPepsiUnlock.ToString() + "/" + manager._nmbrOfPespi;
               manager._textDeath.text = manager._nmbrOfDeath.ToString();
           }
       }
   }
   
}
