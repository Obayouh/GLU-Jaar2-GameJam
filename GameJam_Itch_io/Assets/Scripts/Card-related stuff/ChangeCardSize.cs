using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCardSize : MonoBehaviour
{
    private float _newScaleSize = 0.5f;
    private float _originalScaleSize = 0.7837264f;

    void Update()
    {
        //Checks if this object has a child
        if (this.transform.childCount > 0)
        {
            //Get a new Vector3 with a new scale, set the transform of this object to the new Vector3
            Vector3 newCardScale = new(_newScaleSize, _newScaleSize, _newScaleSize);
            this.transform.localScale = newCardScale;
        }
        //Checks if there are no children in this object
        else if (this.transform.childCount <= 0)
        {
            //Create a new Vector3 with the original scale size and give it to this objects transform
            Vector3 originCardScale = new(_originalScaleSize, _originalScaleSize, _originalScaleSize);
            this.transform.localScale = originCardScale;
        }
    }
}
