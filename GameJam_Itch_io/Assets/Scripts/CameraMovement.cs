using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [Header("CameraShaking")]
    public bool gameOver;
    public AnimationCurve curve;
    public float durationShaking = 1f;

    [Header("SwitchView")]
    public Transform cameraFollower;
    Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    Vector3 targetPos;
    [SerializeField] private float speed;

    public bool switchView;

    TurnManager turnManager;
    CinemachineVirtualCamera cam;

    void Start()
    {
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
        cam.LookAt = cameraFollower;

        turnManager = FindAnyObjectByType<TurnManager>();

        startPos = cameraFollower.position;
        targetPos = endPos;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && turnManager.state == TurnState.PickCard)
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
        if (turnManager == null)
            return;

        if (turnManager.state == TurnState.Waiting)
        {
            switchView = true;
            turnManager.ChangeState(TurnState.Attack);
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
        }
        else if (cameraFollower.position == endPos)
        {
            targetPos = startPos;
            switchView = false;
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
