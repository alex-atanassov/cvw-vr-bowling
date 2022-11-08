using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    private Animator animator;
    public TMP_Text popUpText;

    // Zero if not timed
    public float timer = 0f;
    private float elapsed = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        elapsed += Time.deltaTime;
    }

    public void PopUp()
    {
        Debug.Log("Popup triggered");
        animator.SetTrigger("pop");
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(timer);
        animator.SetTrigger("close");
    }
}
