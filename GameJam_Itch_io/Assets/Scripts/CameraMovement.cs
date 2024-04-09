using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool startShaking;
    public AnimationCurve curve;
    public float duration = 1f;

    public bool moveCam;
    Vector3 targetPosition;
    Vector3 direction;
    public float moveSpeed = 2f;

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

        if (moveCam)
        {
            GoToNewRoom();
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPostition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPostition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPostition;
    }

    public void GoToNewRoom()
    {
        Vector3 startPosition = transform.position;
        direction = targetPosition - startPosition;
        direction.Normalize();
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (transform.position == targetPosition)
        {
            moveCam = false;
            targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 12f);
        }
    }
}
