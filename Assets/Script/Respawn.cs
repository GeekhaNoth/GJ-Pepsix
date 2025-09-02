using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class Respawn : MonoBehaviour
{
    private Vector3 _lastCheckpoint;

    private void Start()
    {
        _lastCheckpoint = transform.position;
    }
    
    
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("CheckPoint"))
       {
           _lastCheckpoint = other.transform.position;
           Destroy(other.gameObject);
       }
       if (other.CompareTag("DeathZone")) transform.position = _lastCheckpoint;
   }
}
