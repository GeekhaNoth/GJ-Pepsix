using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Autodestruction());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet avec lequel il entre en collision a un certain tag
        if (other.gameObject.CompareTag("Mobs"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    IEnumerator Autodestruction()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
