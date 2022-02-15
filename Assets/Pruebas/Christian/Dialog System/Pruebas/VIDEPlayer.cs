using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class VIDEPlayer : MonoBehaviour
{

    //This script handles player movement and interaction with other NPC game objects

    public string playerName = "VIDE User";

    //Reference to our diagUI script for quick access
    public VIDEUIManager1 diagUI;
    //public QuestChartDemo questUI;

    //Stored current VA when inside a trigger
    public VIDE_Assign inTrigger;

    //DEMO variables for item inventory
    //Crazy cap NPC in the demo has items you can collect
    public List<string> demo_Items = new List<string>();
    public List<string> demo_ItemInventory = new List<string>();

    //Reference To Player
    public PlayerController player;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
    }

    void OnTriggerExit()
    {
        inTrigger = null;
    }

    void Update()
    {

        //Only allow player to move and turn if there are no dialogs loaded
        if (VD.isActive)
        {
            player.CanDoAnyMovement(false);
        }
        else
        {
            player.CanDoAnyMovement(true);
        }


        //Interact with NPCs when pressing UpArrow
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryInteract();
        }

    }

    //Casts a ray to see if we hit an NPC and, if so, we interact
    void TryInteract()
    {
        /* Prioritize triggers */

        if (inTrigger)
        {
            diagUI.Interact(inTrigger);
            return;
        }

        /* If we are not in a trigger, try with raycasts */

        RaycastHit rHit;

        if (Physics.Raycast(transform.position, transform.right, out rHit, 2))
        {
            //Lets grab the NPC's VIDE_Assign script, if there's any
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            else return;

            if (assigned.alias == "QuestUI")
            {
                //questUI.Interact(); //Begins interaction with Quest Chart
            }
            else
            {
                diagUI.Interact(assigned); //Begins interaction
            }
        }
    }
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Vector3 direction = transform.TransformDirection(Vector3.right) * 2;
        Gizmos.DrawRay(transform.position, direction);
    }

#endif

}
