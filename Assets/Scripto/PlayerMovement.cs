using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public Transform BulletSpawn;
    public float shootInterval;
    public float fireForce;

    public GameObject bulletParticlePrefab;

    //Vector2 mousePosition;

    ColorBullet colors;
    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        colors = GetComponent<ColorBullet>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        playerSpriteRenderer.color = colors.color;

        StartCoroutine(AutoShoot());
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            colors.ColorBull();
            playerSpriteRenderer.color = colors.color;
            SoundEffectManager.PlaySound("Switch");
        }

        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    //private void FixedUpdate()
    //{
        //Vector2 aimDirection = mousePosition - rb.position;
       // float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //rb.rotation = aimAngle;
   // }

    IEnumerator AutoShoot()
    {
        while (true)
        {
            ShootBull();
            yield return new WaitForSeconds(shootInterval);
            SoundEffectManager.PlaySound("Shoot");
        }
    }

    void ShootBull()
    {

        GameObject bullet = Instantiate(bulletPrefab, BulletSpawn.position, BulletSpawn.rotation);

        bullet.GetComponent<SpriteRenderer>().color = colors.color;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetBulletColor(colors.color);

        GameObject bulletEffect = Instantiate(bulletParticlePrefab, BulletSpawn.position, BulletSpawn.rotation);
        ParticleSystem.MainModule mainModule = bulletEffect.GetComponent<ParticleSystem>().main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(colors.color);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(BulletSpawn.up * fireForce, ForceMode2D.Impulse);

        Destroy(bullet, 2f);
    }
}