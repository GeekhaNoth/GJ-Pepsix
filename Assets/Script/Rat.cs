using UnityEngine;
using System;

public class Rat : MonoBehaviour
{
    
    [SerializeField]private float moveSpeed;
    private bool direction = true;
    
    //Ce script est à mettre dans un empty avec collider en enfant du rat car il s'active à chaque collision et le sol ayant un collider cela faisait n'importe quoi, il faut donc modifier le collider de l'enfant pour qu'il ne touche pas le sol
    private void Update()
    {
        if (!direction) //Fais avancez le rat dans une direction ou l'autre en fonction de la bool
        
        {
            transform.parent.transform.position = new Vector3(transform.parent.transform.position.x + moveSpeed * Time.deltaTime, transform.parent.transform.position.y, transform.parent.transform.position.z);
        }
        else
        {
            transform.parent.transform.position = new Vector3(transform.parent.transform.position.x - moveSpeed * Time.deltaTime, transform.parent.transform.position.y, transform.parent.transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) //Quand le rat entre en collision avec quelque chose qui n'est pas le joueur il s'inverse et inverse la bool pour le faire changer de direction
        {
            if (direction)
            {
                transform.parent.transform.localScale = new Vector3(-0.15f, 0.15f, 0.15f);
            }
            else
            {
                transform.parent.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            }

            direction = !direction; 
        }
    }
}
