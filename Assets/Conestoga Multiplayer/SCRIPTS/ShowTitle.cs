using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowTitle : MonoBehaviour
{
    public Image titleImage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat"))
        {
           
            StartCoroutine(TitleSequence());
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator TitleSequence()
    {
        // Fade in
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            titleImage.color = new Color(1, 1, 1, t);
            yield return null;
        }

        // Stay visible
        yield return new WaitForSeconds(5f);

        // Fade out
        t = 1;

        while (t > 0)
        {
            t -= Time.deltaTime;
            titleImage.color = new Color(1, 1, 1, t);
            yield return null;
        }
    }
}