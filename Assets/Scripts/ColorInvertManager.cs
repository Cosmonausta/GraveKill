using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInvertManager : MonoBehaviour
{
    [SerializeField]
    [Range(0, 2)]
    private float secondUntilBackToBlackLerpStart = 0.3f;

    [SerializeField]
    [Range(0, 1)]
    private float blackAddedPerFrame = 0.1f;

    public AudioClip lightning;
    public AudioClip gong;
    private AudioSource source;
    private float lowPitchRange = .55f;
    private float highPitchRange = .75f;

    private SpriteRenderer groundRender;

    IEnumerator currentColorInversionCR;

    private void Awake()
    {
        groundRender = GameObject.FindGameObjectWithTag("Ground").GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentColorInversionCR != null)
            {
                StopCoroutine(currentColorInversionCR);
            }

            currentColorInversionCR = ColorInversion();
            StartCoroutine(currentColorInversionCR);
        }
    }

    private IEnumerator ColorInversion()
    {
        groundRender.color = Color.white;

        source.pitch = Random.Range(lowPitchRange, highPitchRange);
        source.PlayOneShot(lightning);
        source.PlayOneShot(gong);

        yield return new WaitForSeconds(secondUntilBackToBlackLerpStart);

        while (groundRender.color != Color.black)
        {
            yield return groundRender.color = Color.Lerp(groundRender.color, Color.black, blackAddedPerFrame);
        }
    }
}
