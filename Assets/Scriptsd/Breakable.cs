using System.Runtime.CompilerServices;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public Rigidbody rb;
    public bool isBroken;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isBroken == true)
        {
            rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Breaker")
        {
            if (!isBroken)
            {
                isBroken = true;

                // Report to ObjectiveManager using this object's tag (e.g. "Chair")
                if (ObjectiveManager.Instance != null)
                    ObjectiveManager.Instance.ReportDestruction(gameObject.tag);

                Invoke("DestroyPiece", 3);
            }
        }
    }

    void DestroyPiece()
    {
        Destroy(this.gameObject);
    }
}