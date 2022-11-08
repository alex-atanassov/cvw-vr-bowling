using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSound : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource audioSource;

    public float maxSpeed = 0.5f;
    public AnimationCurve volumeCurve;
    public AnimationCurve pitchCurve;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if(audioSource.isPlaying)
        {
            var speed = rb.velocity.magnitude;
            // Debug.Log("speed= " + speed);

            // normalize speed into 0-1
            var scaledVelocity = Remap(Mathf.Clamp(speed, 0, maxSpeed), 0, maxSpeed, 0, 1);

            // set volume based on volume curve
            audioSource.volume = volumeCurve.Evaluate(scaledVelocity);

            // set pitch based on pitch curve
            audioSource.pitch = pitchCurve.Evaluate(scaledVelocity);
        }
    }


    // https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Floor")
        {
            //Debug.Log("ASD");
            audioSource.Play();
        }
            
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag == "Floor")
        {
            //Debug.Log("DEF");
            audioSource.Stop();
        }
    }
}