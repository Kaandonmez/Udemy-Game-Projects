using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    ParticleSystem particle;

    AudioSource audioSource;
    Rigidbody rb;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    //! Press L load next level
    //! Press C disable/enable collision
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        CheatSheet();
        //* Bu kısım L ve C keylerini cheat key olarak kullanmaya yarıyor. 
        //* L key: direk bölüm geçmeye, 
        //* C key ise collisionevent'i ortadan kaldırıyor.
    }

    void CheatSheet()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }



    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly object");
                break;
            case "Finish":
                Debug.Log("This is a Finish");
                StartSuccess();
                break;
            default:
                Debug.Log("you blew up!!");
                StartCrash();
                break;
        }
    }

    void StartSuccess()
    {
        isTransitioning = true;
        audioSource.Stop();
        successParticles.Play();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", 1f);
    }
    void StartCrash()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }
    void ReloadLevel()
    {
        GetComponent<Movement>().enabled = false;
        //SceneManager.LoadScene(0); //SceneManager.LoadScene("Menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }


}
