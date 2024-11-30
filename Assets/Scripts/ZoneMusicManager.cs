using UnityEngine;

public class ZoneMusicTrigger : MonoBehaviour
{
    public AudioClip enterMusic;  // Music to play when entering this zone.
    public AudioClip exitMusic;  // Music to play when leaving this zone.
    private bool isPlayerInZone = false;  // Track whether the player is in the zone.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInZone)
        {
            // Play the "enter music" when the player enters the zone.
            AudioManager.Instance.PlayMusic(enterMusic);
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPlayerInZone)
        {
            // Play the "exit music" when the player leaves the zone.
            AudioManager.Instance.PlayMusic(exitMusic);
            isPlayerInZone = false;
        }
    }
}
