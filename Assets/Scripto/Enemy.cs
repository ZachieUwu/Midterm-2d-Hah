using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    public Color[] avColors;
    public int selectedColor;
    private SpriteRenderer enemyColor;

    public float distance;
    public Transform playerTarget;
    public float speed;

    private GameManager gameManager;
    private CinemachineImpulseSource impulseSource;

    public GameObject playerPrefab;
    private Music music;

    void Start()
    {
        enemyColor = GetComponent<SpriteRenderer>();
        impulseSource = GetComponent<CinemachineImpulseSource>();

        gameManager = FindObjectOfType<GameManager>();

        int randomColorIndex = Random.Range(0, avColors.Length);

        enemyColor.color = avColors[randomColorIndex];

    }

    void Update()
    {
        if (playerTarget != null)
        {
            distance = Vector2.Distance(transform.position, playerTarget.transform.position);
            Vector2 direction = playerTarget.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle + 180);
            transform.position = Vector2.MoveTowards(this.transform.position, playerTarget.transform.position, speed * Time.deltaTime);

            GetTarget();
        }
    }

    public bool SameColor(Color bulletColor)
    {
        return Mathf.Abs(bulletColor.r - enemyColor.color.r) < 0.01f &&
               Mathf.Abs(bulletColor.g - enemyColor.color.g) < 0.01f &&
               Mathf.Abs(bulletColor.b - enemyColor.color.b) < 0.01f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundEffectManager.PlaySound("GO Explosion");
            CameraShake.instance.CameraShaking(impulseSource);

            GameObject effect = Instantiate(playerPrefab, transform.position, Quaternion.identity);

            gameManager.GameOver();
            Destroy(collision.gameObject);

            PauseMusic();
            DisableSpawners();
        }
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void DisableSpawners()
    {
        EnemySpawn[] spawners = FindObjectsOfType<EnemySpawn>();
        foreach (EnemySpawn spawner in spawners)
        {
            Destroy(spawner.gameObject);
        }
    }

    void PauseMusic()
    {
        Music music = FindObjectOfType<Music>();
        if (music != null)
        {
            music.StopMusic();
        }
    }
}