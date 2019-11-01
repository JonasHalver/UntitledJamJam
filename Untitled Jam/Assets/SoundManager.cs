using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource low, medium, high, intense;

    [Range(1, 10)]
    public static int intensity = 1;
    [Range(1, 10)]
    public int localIntensity = 1;
    [Range(0f, 1f)]
    public float volume = 1;

    public float fadeSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //intensity = localIntensity;
        switch (intensity)
        {
            case 1:
            case 2:
                low.volume = Mathf.Lerp(low.volume, volume, fadeSpeed * Time.deltaTime);
                if (medium.volume > 0.05f)
                    medium.volume = Mathf.Lerp(medium.volume, 0, fadeSpeed * Time.deltaTime);
                if (high.volume > 0.05f)
                    high.volume = Mathf.Lerp(high.volume, 0, fadeSpeed * Time.deltaTime);
                if (intense.volume > 0.05f)
                    intense.volume = Mathf.Lerp(intense.volume, 0, fadeSpeed * Time.deltaTime);
                break;
            case 3:
            case 4:
            case 5:
                if (low.volume > 0.05f)
                    low.volume = Mathf.Lerp(low.volume, 0, fadeSpeed * Time.deltaTime);
                medium.volume = Mathf.Lerp(medium.volume, volume, fadeSpeed * Time.deltaTime);
                if (high.volume > 0.05f)
                    high.volume = Mathf.Lerp(high.volume, 0, fadeSpeed * Time.deltaTime);
                if (intense.volume > 0.05f)
                    intense.volume = Mathf.Lerp(intense.volume, 0, fadeSpeed * Time.deltaTime);
                break;
            case 6:
            case 7:
            case 8:
                if (low.volume > 0.05f)
                    low.volume = Mathf.Lerp(low.volume, 0, fadeSpeed * Time.deltaTime);
                if (medium.volume > 0.05f)
                    medium.volume = Mathf.Lerp(medium.volume, 0, fadeSpeed * Time.deltaTime);
                high.volume = Mathf.Lerp(high.volume, volume, fadeSpeed * Time.deltaTime);
                if (intense.volume > 0.05f)
                    intense.volume = Mathf.Lerp(intense.volume, 0, fadeSpeed * Time.deltaTime);
                break;
            case 9:
            case 10:
                if (low.volume > 0.05f)
                    low.volume = Mathf.Lerp(low.volume, 0, fadeSpeed * Time.deltaTime);
                if (medium.volume > 0.05f)
                    medium.volume = Mathf.Lerp(medium.volume, 0, fadeSpeed * Time.deltaTime);
                if (high.volume > 0.05f)
                    high.volume = Mathf.Lerp(high.volume, 0, fadeSpeed * Time.deltaTime);
                intense.volume = Mathf.Lerp(intense.volume, volume, fadeSpeed * Time.deltaTime);
                break;
        }   
    }
}
