using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class CoinManager : MonoBehaviour
{
    public AudioClip pickupSound;
    private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayPickupSound();
        }
    }

    private void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }
}