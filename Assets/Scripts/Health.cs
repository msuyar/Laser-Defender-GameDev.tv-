using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer ;
    [SerializeField] int health = 50;
    [SerializeField] int scoreAdded = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() 
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();     
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null)
        {
            //Take damage
            // tell damage dealer that it hit something
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }    
    }

    void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        // plays damage taken sound effect
        audioPlayer.PlayDamageTakenClip();
        //Debug.Log(health);
        if(health <= 0)
        {          
            Die();
        }
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
    public int GetHealth()
    {
        return health;
    }
    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.AddScore(scoreAdded);
        }
        else if(isPlayer)
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }
}
