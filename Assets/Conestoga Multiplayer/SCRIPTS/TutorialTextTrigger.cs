using UnityEngine;
using System.Collections;

public class TutorialTextTrigger : MonoBehaviour
{
    public GameObject tutorialText;

    public float displayTime = 7f;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Boat"))
        {
            StartCoroutine(ShowText());
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator ShowText()
    {
        tutorialText.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        tutorialText.SetActive(false);
    }
}