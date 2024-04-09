using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool startShaking;
    public AnimationCurve curve;
    public float duration = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        if (startShaking)
        {
            startShaking = false;
            StartCoroutine(Shaking());
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
        transform.Translate(Vector3.forward);
    }
}
