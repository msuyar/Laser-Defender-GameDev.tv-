using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0.2f;
    [SerializeField] float minFireRate = 0.8f;
 
    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        
    }
    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, 
                                              transform.position, 
                                              Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifeTime);
            float randomRate = Random.Range(firingRate - firingRateVariance,
                                            firingRate + firingRateVariance);
            randomRate = Mathf.Clamp(randomRate, minFireRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(randomRate);
        }
        
    }
}
