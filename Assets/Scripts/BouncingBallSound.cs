using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BouncingBallSound : MonoBehaviour
{
    public AudioClip bounceSound; // Assign this in the inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the ball
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Play the bouncing sound effect
        audioSource.PlayOneShot(bounceSound, GetVolumeBasedOnVelocity());
    }

    float GetVolumeBasedOnVelocity()
    {
        // Optional: adjust volume based on the speed of the ball
        float speed = GetComponent<Rigidbody>().velocity.magnitude;
        // Assuming that speed can be from 0 to 10, where 10 is the max speed.
        // This will convert the speed to a range of 0.1 to 1 for the volume.
        // You can adjust the min and max volume as needed.
        float minVolume = 0.1f;
        float maxVolume = 1.0f;
        return Mathf.Clamp((speed / 10) * (maxVolume - minVolume) + minVolume, minVolume, maxVolume);
    }
}
