using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("CameraShaking")]
    public bool startShaking;
    public bool gameOver;
    public AnimationCurve curve;
    public float durationShaking = 1f;

    [Header("Move")]
    Vector3 targetPosition;
    Vector3 direction;
    public float moveSpeed = 2f;
    public float durationMoving;

    void Start()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 12f);
    }

    void Update()
    {
        if (startShaking)
        {
            startShaking = false;
            StartCoroutine(Shaking());
        }
        else if (gameOver)
        {
            StartCoroutine(Shaking());
        }
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

    public void GoToNewRoom()
    {
        StartCoroutine(MoveCam());
    }

    IEnumerator MoveCam()
    {
        float elapsedTime = 0f;
        yield return new WaitForSeconds(1f);

        while (elapsedTime < durationMoving)
        {
            elapsedTime += Time.deltaTime;
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            yield return null;
        }

        if (elapsedTime >= durationMoving)
        {
            RoomManager roomManager = FindAnyObjectByType<RoomManager>();
            roomManager.canAddNewRoom = true;
        }
    }
}
