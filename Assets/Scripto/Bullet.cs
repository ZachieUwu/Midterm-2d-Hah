using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bullet : MonoBehaviour
{
    private Color bulletColor;
    private GameManager gameManager;
    public int scoreValue = 1;

    private CinemachineImpulseSource impulseSource;

    public GameObject explosionPrefab;
    public GameObject hitPrefab;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void SetBulletColor(Color color)
    {
        bulletColor = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && enemy.SameColor(bulletColor))
            {
                SoundEffectManager.PlaySound("Explosion");
                CameraShake.instance.CameraShaking(impulseSource);

                GameObject effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                SetParticleColor(effect, enemy.GetComponent<SpriteRenderer>().color);
                Destroy(collision.gameObject);
                Destroy(gameObject);

                gameManager.AddScore(scoreValue);
            }

            else
            {
                SoundEffectManager.PlaySound("Hit");
                CameraShake.instance.CameraShaking(impulseSource);

                GameObject effect = Instantiate(hitPrefab, transform.position, Quaternion.identity);
                SetParticleColor(effect, enemy.GetComponent<SpriteRenderer>().color);
                Destroy(gameObject);
            }
        }
    }

    void SetParticleColor(GameObject particleEffect, Color enemyColor)
    {
        var particleSystem = particleEffect.GetComponent<ParticleSystem>();
        var mainModule = particleSystem.main;
        mainModule.startColor = enemyColor;
    }
}
