using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float boost = 1f;
    [SerializeField] float RotateRight = 1f;
    [SerializeField] float RotateLeft = 1f;
    [SerializeField] ParticleSystem mainBoosting1;
    [SerializeField] ParticleSystem mainBoosting2;
    [SerializeField] ParticleSystem mainBoosting3;
    [SerializeField] ParticleSystem leftBoosting;
    [SerializeField] ParticleSystem rightBoosting;

    ParticleSystem particle;


    [SerializeField] AudioClip mainEngine;    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        audioSource = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {

        //ProccesInput();
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            StartThrusting();
        }
        else
        {
            StopThrustAudio();
        }

    }

    void StopThrustAudio()
    {
        audioSource.Stop();
        mainBoosting1.Stop();
        mainBoosting2.Stop();
        mainBoosting3.Stop();
    }

    void StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosting1.isPlaying)
        {
            mainBoosting1.Play();
            mainBoosting2.Play();
            mainBoosting3.Play();

        }
        //transform.Translate(0, boost * Time.deltaTime, 0);
        rb.AddRelativeForce(Vector3.up * boost * Time.deltaTime);
        GetComponent<Rigidbody>().useGravity = true;
    }

    void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            StartTurnRight();

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartTurnLeft();

        }
        else
        {
            StopTurnAudio();
        }
    }

    void StopTurnAudio()
    {
        if (rightBoosting.isPlaying)
        {
            rightBoosting.Stop();
        }
        else if (leftBoosting.isPlaying)
        {
            leftBoosting.Stop();
        }
    }

    void StartTurnLeft()
    {
        ApplyRotation(-RotateLeft);
        if (!rightBoosting.isPlaying)
        {
            rightBoosting.Play();
        }
    }

    void StartTurnRight()
    {
        ApplyRotation(RotateRight);
        if (!leftBoosting.isPlaying)
        {
            leftBoosting.Play();
        }
    }

    void ApplyRotation(float Rotationinfo)
    {
        rb.freezeRotation = true;
        transform.Rotate(0, 0, -Rotationinfo * Time.deltaTime);
        GetComponent<Rigidbody>().useGravity = true;
        rb.freezeRotation = false;

    }
}
