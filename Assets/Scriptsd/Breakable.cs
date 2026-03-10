using System.Runtime.CompilerServices;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    public Rigidbody rb;

    public bool isBroken;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBroken==true)
        {
            rb.isKinematic = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Breaker")
        {
            isBroken = true;
        }
    }
}
