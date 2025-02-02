using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryScene : MonoBehaviour
{
    [Header("EnableAndDisableObjects")]
    public GameObject activableObjects;
    public List<GameObject> otherEntrances;

    public SceneController _sceneManager;

    public List<string> additiveScenes;
    public string actualScene;

    public AdditiveScenesInfo sceneInfo;

    public GameObject transitionCanvas;
    public Animator canvasAnimator;

    private WaitForSeconds wait = new WaitForSeconds(.5f);

    public PlayerController player;
    public PlayerAnimatorController playerAnimator;
    public float topTime = 1;
    private Vector3 movement;
    public float direction;
    public int musicIndex;

    private void Awake()
    {
        _sceneManager = FindObjectOfType<SceneController>();
        transitionCanvas = GameObject.Find("TransitionCanvas");
        canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();

        player = FindObjectOfType<PlayerController>();
        playerAnimator = FindObjectOfType<PlayerAnimatorController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActualiceSceneInfo();

            activableObjects.SetActive(true);               // Enable enemies

            for (int i = 0; i < otherEntrances.Count; i++)
            {
                otherEntrances[i].SetActive(false);         // Disable other entrances
            }

            foreach (string scene in additiveScenes)
            {
                _sceneManager.LoadSceneInAdditive(scene, OnSceneComplete);
            }

            StartCoroutine(WaitForFade());
            movement.Set(15f * direction, 0.0f, 0.0f);
            StartCoroutine(Move());

            _sceneManager.StartMusic(musicIndex);
        }
    }

    private void ActualiceSceneInfo()
    {
        sceneInfo.additiveScenes = additiveScenes;
        sceneInfo.actualScene = actualScene;
    }

    private void CanvasTransition()
    {
        // De negro a transparente

        canvasAnimator.SetBool("ToBlack", false);
    }

    private IEnumerator WaitForFade()
    {
        yield return wait;

        CanvasTransition();
    }

    IEnumerator Move()
    {
        player.CanDoAnyMovement(false);
        float timer = 0;
        
        player.useMovePlayerController = false;
        if (direction >= 1)
        {
            player.playerModel.transform.eulerAngles = player.fixedPlayerRotation;
        } else
        {
            player.playerModel.transform.eulerAngles = player.fixedPlayerRotationBack;
        }
        
        while (timer < topTime)
        {
            playerAnimator.Run(direction);
            player.rigidBody.velocity = movement;
            timer += Time.deltaTime;
            yield return null;
        }

        if (direction >= 1)
        {
            player.playerModel.transform.eulerAngles = player.playerRotation;
        } else
        {
            player.playerModel.transform.eulerAngles = player.playerRotationBack;
        }
        player.useMovePlayerController = true;
        player.CanDoAnyMovement(true);
        gameObject.SetActive(false);
    }

    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }
}
