using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerChange GM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GM.currentPlayer.transform.position;
    }
}
