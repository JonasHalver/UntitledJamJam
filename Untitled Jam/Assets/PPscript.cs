using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPscript : MonoBehaviour
{
    ColorGrading cg;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<ColorGrading>(out cg);
    }

    // Update is called once per frame
    void Update()
    {
        cg.saturation.value = PlayerHealth.health - 100;
    }
}
