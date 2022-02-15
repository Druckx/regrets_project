using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class BossEntrance : MonoBehaviour
{
    public BossController bossController;

    //Reference To Player
    public PlayerController player;

    //Stored current VA when inside
    public VIDE_Assign dialogue;

    //Reference to our diagUI script for quick access
    public VIDEUIManager1 diagUI;

    public ObjectStatus eventHappened;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            /*if (eventHappened.eventAlreadyHappened)
            {
                return;
            }*/
            //else
            //{
                diagUI.Interact(dialogue);
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            diagUI.Interact(dialogue);
            bossController.animatorController.Landing();
        }
    }

    public void StartFight()
    {
        bossController.isEntranceStateRunning = false;
        this.gameObject.SetActive(false);
    }
}
