using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    private float speed = 500;
   

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    private float speedBoost = 100000;

    Coroutine powerUpTimer;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
       
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * Time.deltaTime); 
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(focalPoint.transform.forward * speedBoost * Time.deltaTime);
            Debug.Log("That boy is going fast");
            
        }
         
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerUpTimer = StartCoroutine(PowerupCooldown(7));
            powerupIndicator.SetActive(true);
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown(int countLimit)
    {
        int i = 0;
        while (i < countLimit)
        {
            i++;
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Finished");
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup == true) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
