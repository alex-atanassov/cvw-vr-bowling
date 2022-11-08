using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miss : MonoBehaviour
{
    public float thrust = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Side")
            GetComponent<Rigidbody>().AddForce(0, 0, thrust);
    }
}
