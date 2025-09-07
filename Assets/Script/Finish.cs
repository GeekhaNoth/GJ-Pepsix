using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject finishCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finishCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        finishCanvas.SetActive(true);
    }
}
