using UnityEngine;

public class RiverCurrent : MonoBehaviour
{
    public Vector3 currentDirection = Vector3.left;
    public float currentForce = 5f;

    void OnTriggerStay(Collider other)
    {
        if (other.transform.root.CompareTag("Boat"))
        {
            Rigidbody rb = other.transform.root.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(currentDirection * currentForce, ForceMode.Acceleration);
            }
        }
    }
}