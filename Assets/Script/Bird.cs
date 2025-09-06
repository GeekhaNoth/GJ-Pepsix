using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool direction = true;

    // Update is called once per frame
    private void Update()
    {
        if (direction)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
        }
    }
}
