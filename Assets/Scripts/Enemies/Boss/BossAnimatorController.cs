using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorController : MonoBehaviour
{
    private Animator animator;

    public GameObject originalModel;
    public GameObject rollModel;

    private BoxCollider colliderPunch;

    private const string JUMP = "Jump";
    private const string ATTACK = "Attack";
    private const string RUN = "Run";
    private const string VELOCITY = "Velocity";
    private const string DEFEATED = "Defeat";
    private const string ROAR = "Roar";
    private const string LAND = "Land";


    public bool endAttackAnimation;

    public AudioClip[] audioList;
    public AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        colliderPunch = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        endAttackAnimation = false;
    }

    public void Jump()
    {
        animator.SetTrigger(JUMP);
    }

    public void FinishJump()
    {
        //animator.SetBool(JUMP, false);
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK);
    }

    public void Run()
    {
        animator.SetBool(RUN, true);
    }

    public void Idle()
    {
        animator.SetBool(RUN, false);
    }

    public void Defeated()
    {
        animator.SetTrigger(DEFEATED);
    }

    public void Roar()
    {
        animator.SetTrigger(ROAR);
        audioSource.clip = audioList[0];
        audioSource.Play();
    }

    private void ChangeVel()
    {
        animator.SetFloat(VELOCITY, 0.5f);
    }

    public void EndAnimation()
    {
        endAttackAnimation = true;
    }

    public void Landing()
    {
        animator.SetTrigger(LAND);
    }

    public void ChangeModel()
    {
        if (originalModel.activeInHierarchy)
        {
            originalModel.SetActive(false);
            rollModel.SetActive(true);
        }
        else
        {
            originalModel.SetActive(true);
            rollModel.SetActive(false);
        }
    }

    public void EnableCollider()
    {
        colliderPunch.enabled = true;
    }

    public void DisableCollider()
    {
        colliderPunch.enabled = false;
    }
}
