using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpPlats : MonoBehaviour
{
    // Start is called before the first frame update
    private float emissionMaxIntensity;
    private float emissionBaseIntensity;
    private Color baseColor;
    private float duration;
    private Renderer ren;
    private Color colorStore;
    public bool disappear;

    public AnimationCurve intensity;
    void Start()
    {
        emissionMaxIntensity = 500000;
        emissionBaseIntensity = 50f; 
        duration = .4f;
        ren = GetComponent<Renderer>();
        ren.material.EnableKeyword("_EmissionIntensity");
        colorStore = ren.material.GetColor("_EmissiveColor");


    }

    IEnumerator LightUp()
    {
        float elapsed = 0f;
        Color currentColor = ren.material.GetColor("_EmissiveColor");
        Color newEmissiveColor = currentColor;
        Invoke("Reset2", duration);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float intense = intensity.Evaluate(elapsed / duration);
            //ren.material.SetFloat("_EmissionIntensity", Mathf.Pow(emissionBaseIntensity, intense));
            newEmissiveColor = currentColor * Mathf.Pow(emissionBaseIntensity, intense);
            ren.material.SetColor("_EmissiveColor", newEmissiveColor);

            //Debug.Log(ren.material.GetFloat("_EmissionIntensity"));
            yield return null;
        }



       
    }

    private void Reset2()
    {
        ren.material.SetColor("_EmissiveColor", colorStore);
    }
    
    
    
    public void InitiateCoroutine()
    {
        StartCoroutine(LightUp());
    }

}
