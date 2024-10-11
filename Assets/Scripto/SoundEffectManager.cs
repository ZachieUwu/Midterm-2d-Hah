using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static AudioClip shootSound, hitSound, explosionSound, goExplosionSound, switchSound;
    static AudioSource audioSRC;
    // Start is called before the first frame update
    void Start()
    {
        shootSound = Resources.Load<AudioClip>("Shoot");
        hitSound = Resources.Load<AudioClip>("Hit");
        explosionSound = Resources.Load<AudioClip>("Explosion");
        goExplosionSound = Resources.Load<AudioClip>("GO Explosion");
        switchSound = Resources.Load<AudioClip>("Switch");
        
        audioSRC = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Shoot":
                audioSRC.PlayOneShot (shootSound); 
            break;
            case "Hit":
                audioSRC.PlayOneShot (hitSound);
            break;
            case "Explosion":
                audioSRC.PlayOneShot (explosionSound);
            break;
            case "GO Explosion":
                audioSRC.PlayOneShot (goExplosionSound);
            break;
            case "Switch":
                audioSRC.PlayOneShot(switchSound);
            break;
        }
    }
}
