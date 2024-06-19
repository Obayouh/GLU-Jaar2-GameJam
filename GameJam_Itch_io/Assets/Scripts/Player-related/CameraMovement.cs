using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum ViewState
{
    cards,
    game
}

public class CameraMovement : MonoBehaviour
{
    [Header("CameraShaking")]
    public AnimationCurve curve;
    public float durationShaking = 1f;

    [Header("SwitchView")]
    public Transform cameraFollower;
    Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    Vector3 targetPos;
    [SerializeField] private float speed;

    public bool switchView;

    public ViewState state;

    TurnManager turnManager;
    CinemachineVirtualCamera cam;

    void Start()
    {
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
        cam.LookAt = cameraFollower;

        turnManager = FindAnyObjectByType<TurnManager>();

        startPos = cameraFollower.position;
        targetPos = endPos;

        state = ViewState.game;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && turnManager.ReturnState() == TurnState.PickCard)
        {
            switchView = true;
        }

        if (switchView)
        {
            SwitchView();
        }
    }

    public void CheckState()
    {
        if (turnManager.ReturnState() == TurnState.Waiting)
        {
            switchView = true;
            turnManager.ChangeState(TurnState.Attack);
        }
        else if (turnManager.ReturnState() == TurnState.PickNewCard)
        {
            //Nothing
        }
        else
        {
            if (cameraFollower.position != startPos)
            {
                switchView = true;
                targetPos = startPos;
            }
        }
    }

    public void SwitchView()
    {
        cameraFollower.position = Vector3.MoveTowards(cameraFollower.position, targetPos, speed * Time.deltaTime);
        if (cameraFollower.position == startPos)
        {
            targetPos = endPos;
            switchView = false;
            state = ViewState.cards;
        }
        else if (cameraFollower.position == endPos)
        {
            targetPos = startPos;
            switchView = false;
            state = ViewState.game;
        }
    }

    public void StartShaking()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        Vector3 startPostition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < durationShaking)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / durationShaking);
            transform.position = startPostition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPostition;
    }
}
