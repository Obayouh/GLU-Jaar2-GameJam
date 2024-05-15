using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Behavior : MonoBehaviour
{
    private bool _portalIsSmall = false;

    private int _normalScale = 1;

    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime /4, transform.localScale.y, transform.localScale.z - Time.deltaTime /4);

            if (transform.localScale.x <= 0 && transform.localScale.z <= 0)
            {
                _portalIsSmall = true;
            }
        }

        if (_portalIsSmall)
        {
            transform.localScale = new Vector3(_normalScale, _normalScale, _normalScale);
            this.gameObject.SetActive(false);
            _portalIsSmall = false;
        }
    }
}
