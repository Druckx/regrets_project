using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject canvas;
    public Image canvasImage;

    public ObjectStatus eventHappened;

    public PlayerController playerController;

    public ParticleSystem effect;
    public GameObject model;

    private void Awake()
    {
        if (eventHappened.eventAlreadyHappened)
        {
            this.gameObject.SetActive(false);
        }

        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerExit(Collider other)
    {
        UnShow();
    }


    public void Show()
    {
        eventHappened.eventAlreadyHappened = true;
        canvas.SetActive(true);
        Disapear();
        // FADE IN
    }

    public void UnShow()
    {
        // FADE OUT Y DESPUES DESACTIVAR
        canvas.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void PlayerMovement(bool enabled)
    {
        playerController.CanDoAnyMovement(enabled);
    }

    public void Disapear()
    {
        effect.Play();
        
        StartCoroutine(FinishEffect());
    }

    IEnumerator FinishEffect()
    {
        yield return new WaitForSeconds(0.5f);
        model.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        effect.Stop();
    }

}
