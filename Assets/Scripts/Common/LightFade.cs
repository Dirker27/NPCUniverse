using UnityEngine;
using System.Collections;

public class LightFade : MonoBehaviour {

    public int fadeRate = 1;
    public int lowerThreshold = 0;
    public int upperThreshold = 5;

    public int fadeTarget = 0;

	// Use this for initialization
	void Start () {
        // TODO: enforce lowerThreshold < upperThreshold

        fadeTarget = lowerThreshold;
	}
	
	// Update is called once per frame
	void Update () {
        Light light = GetComponent<Light>();
        int fadeDirection = fadeTarget > light.intensity ? 1 : -1;

        float newIntensity = light.intensity + (Time.deltaTime * fadeRate * fadeDirection);

        if (newIntensity < lowerThreshold)
        {
            fadeTarget = upperThreshold;
        }
        else if (newIntensity > upperThreshold)
        {
            fadeTarget = lowerThreshold;
        }

        light.intensity = newIntensity;
	}
}
