using UnityEngine;
using System;

public class Rat : MonoBehaviour
{
    
    [SerializeField]private float moveSpeed;
    private bool direction = true;
    
    // Update is called once per frame
    private void Update()
    {
        if (!direction)
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
        if (!other.gameObject.CompareTag("Player"))
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
