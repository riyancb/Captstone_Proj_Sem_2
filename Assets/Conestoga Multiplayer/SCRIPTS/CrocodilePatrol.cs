using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CrocodilePatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 3f;

    public Image hitFlash;
    public AudioSource splashSound;

    private Transform target;

    void Start()
    {
        target = pointB;

        if (hitFlash != null)
        {
            hitFlash.color = new Color(1, 0, 0, 0);
        }
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
            target = (target == pointA) ? pointB : pointA;
        }

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rot,
                5f * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat"))
        {
            StartCoroutine(GameOverSequence());
        }
    }

    IEnumerator GameOverSequence()
    {
        if (splashSound != null)
            splashSound.Play();

        if (hitFlash != null)
            hitFlash.color = new Color(1, 0, 0, 0.6f);

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}