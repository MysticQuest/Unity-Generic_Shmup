using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    Camera mainCam;


    [Header("Player")]
    public float moveForce = 1;
    public int health = 200;
    float paddingX = 0.04f;
    float paddingY = 0.03f;

    [SerializeField] [Range(0, 1)] float explosionSFXVol = 0.75f;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0, 1)] float shootSoundVol = 0.75f;
    [SerializeField] AudioClip shootSound;

    [Header("Projectile")]
    public float projSpeed = 10f;
    public float fireCD = 1f;
    public GameObject laser1Prefab;

    bool coolDown = false;

    /*float xMin;
    float xMax;
    float yMin;
    float yMax;*/

    public void Start()
    {
        mainCam = Camera.main;
        body = GetComponent<Rigidbody2D>();
    }


    public void Update()
    {
        ConfineToScreen();

        Move();
        Shoot();
    }

    private void ConfineToScreen()
    {
        /*Camera mainCam = Camera.main;
        xMin = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + paddingX;
        xMax = mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - paddingX;
        yMin = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + paddingY;
        yMax = mainCam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - paddingY;*/

        var pos = mainCam.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0 + paddingX, 1 - paddingX);
        pos.y = Mathf.Clamp(pos.y, 0 + paddingY, 1 - paddingY);
        transform.position = mainCam.ViewportToWorldPoint(pos);
    }

    public void Move()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        var direction = (mousePos - transform.position).normalized;
        body.AddForce(direction * moveForce * Time.deltaTime);

        //Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = mousePos;

        //alternative transform movement test

        /*float inputX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * Time.deltaTime;
        Vector2 mousePos = new Vector2(inputX, inputY);
        transform.position = mousePos;*/

        //rigidbody delay movement test - needs vector3 for mousePos
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && coolDown == false)
        {
            coolDown = true;
            StartCoroutine(RepeatingFire());
        }
    }

    IEnumerator RepeatingFire()
    {

        GameObject laser = Instantiate(laser1Prefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVol);
        Destroy(laser, 2);
        yield return new WaitForSeconds(fireCD);
        coolDown = false;
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
        FindObjectOfType<Level>().LoadGameOver();
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionSFXVol);
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }
}
