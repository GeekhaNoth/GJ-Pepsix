using System;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Transform _playerPosition;

   private void Start()
   {
      _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      _playerPosition.position = transform.position;
   }
}
