using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Networking;

public class LightFlickering : MonoBehaviour
{
    private Light2D myLight;
    public float minRange = 1f;
    public float maxRange = 5f;

    private void Start()
    {
        myLight = GetComponent<Light2D>();
    }
    private void Update()
    {
        StartCoroutine(LightFlick());
    }
    private IEnumerator LightFlick()
    {
        yield return new WaitForSeconds(3);
        myLight.pointLightOuterRadius = Random.Range(minRange, maxRange);
        StartCoroutine(LightFlick());
    }
}
