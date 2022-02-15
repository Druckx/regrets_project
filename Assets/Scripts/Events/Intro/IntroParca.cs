using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroParca : MonoBehaviour
{
    public ParticleSystem effect;
    public GameObject model;

    public void Disapear()
    {
        effect.Play();
        model.SetActive(false);
    }

    IEnumerator FinishEffect()
    {
        yield return new WaitForSeconds(1f);
        effect.Stop();
    }
}
