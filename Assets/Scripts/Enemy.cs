
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Enemy Shooting")]
    float shotCounter;
    [SerializeField] float minShotCD = 0.2f;
    [SerializeField] float maxShotCD = 1f;
    public float projSpeed = 10f;
    [SerializeField] GameObject elaserPrefab;
    [SerializeField] float projLife = 3f;

    [Header("Enemy FX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDur = 1f;

    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0, 1)] float explosionSFXVol = 0.75f;
    [SerializeField] [Range(0, 1)] float shootSoundVol = 0.75f;
    [SerializeField] AudioClip shootSound;

    void Start()
    {
        shotCounter = Random.Range(minShotCD, maxShotCD);
    }

    void Update()
    {
        ShootWithCD();
    }

    private void ShootWithCD()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minShotCD, maxShotCD);
        }
    }

    private void Fire()
    {
        GameObject elaser = Instantiate(elaserPrefab, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity) as GameObject;
        elaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVol);

        Destroy(elaser, projLife);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        HitProcess(damageDealer);
    }

    private void HitProcess(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamaged();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().Addscore(scoreValue);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(gameObject, explosionDur);
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionSFXVol);
        Destroy(this.gameObject);
    }
}
