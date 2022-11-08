using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PinFalling : MonoBehaviour
{
    public TMP_Text text;
    private int score = 0;
    private int fallen = 0;
    public GameObject[] spawnPoints;
    public GameObject pinPrefab;

    public UnityEvent restorePinsTextEvent;

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object exited the trigger");
        if (other.tag == "Pin")
        {
            IncreaseScore(other);
            other.tag = "FallenPin";
        }
    }

    public void IncreaseScore(Collider other)
    {
        score += 1;
        UpdateText();

        fallen += 1;
        Destroy(other.gameObject, 3);
        if(fallen >= 10)
        {
            restorePinsTextEvent.Invoke();
            Invoke("Respawn", 5);
            fallen = 0;
        }
    }

    private void UpdateText()
    {
        text.text = score.ToString();
    }

    private void Respawn()
    {
        foreach (GameObject point in spawnPoints)
        {
            Debug.Log(point.transform.position);
            Instantiate(pinPrefab, point.transform.position, Quaternion.identity);
        }
    }
}
