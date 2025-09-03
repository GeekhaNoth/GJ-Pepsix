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
        manager._lastCheckpoint = transform.position;
    }
    
    
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (!_isActive) return;
       if (other.CompareTag("CheckPoint"))
       {
           manager._lastCheckpoint = other.transform.position;
           other.GetComponent<BoxCollider2D>().enabled = false;
       }
       if (other.CompareTag("DeathZone")) transform.position = manager._lastCheckpoint;

       if (other.CompareTag("HiddenZone"))
       {
           _isHiddenActive = !_isHiddenActive;
           other.transform.GetChild(0).gameObject.SetActive(_isHiddenActive);
       }
   }
   
}
