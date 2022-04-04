using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject parent;

    Vector3 rot;
    Vector3 lPos;
    void Start()
    {

        rot = transform.localRotation.eulerAngles;
        lPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position + lPos;
    }
}
