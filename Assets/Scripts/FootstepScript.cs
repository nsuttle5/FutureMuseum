using UnityEngine;
using System.Collections;

public class FootstepScript : MonoBehaviour
{
    public GameObject footstep;
    public AudioSource footstepAudioSource;
    private bool isWalking = false;
    private bool isSprinting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        footstep.SetActive(false);
        if (footstepAudioSource == null)
        {
            footstepAudioSource = footstep.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            footstepAudioSource.pitch = 1.5f; // Increase pitch for sprinting
        }
        else
        {
            isSprinting = false;
            footstepAudioSource.pitch = 1.3f; // Slightly increased pitch for walking
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!isWalking)
            {
                isWalking = true;
                StopAllCoroutines();
                StartCoroutine(Footsteps());
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            isWalking = false;
            StopFootsteps();
        }
    }

    IEnumerator Footsteps()
    {
        while (isWalking)
        {
            footstep.SetActive(true);
            footstepAudioSource.Play();
            yield return new WaitForSeconds(isSprinting ? 0.5f : 0.7f); // Faster interval for walking
        }
    }

    void StopFootsteps()
    {
        isWalking = false;
        footstep.SetActive(false);
        footstepAudioSource.Stop();
        StopAllCoroutines();
    }
}
