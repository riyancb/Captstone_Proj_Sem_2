using UnityEngine;
using UnityEngine.SceneManagement;

public class CrocodilePatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 3f;

    private Transform target;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            if (target == pointA)
                target = pointB;
            else
                target = pointA;
        }

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5f * Time.deltaTime);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
        {
            SceneManager.LoadScene(0);
        }
    }
}