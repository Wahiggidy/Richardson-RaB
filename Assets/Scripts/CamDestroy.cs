using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion; 

    void Start()
    {
        //Invoke("OnDestroy2", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (explosion != null)
        {
            var particle = explosion.GetComponent<ParticleSystem>();
            particle.Play();
            ParticleSystem.EmissionModule em = particle.emission;
            em.enabled = true;
        }
    }

}
