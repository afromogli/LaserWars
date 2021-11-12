using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float LerpSpeed = 10;
    public Transform TargetObject;

    private Vector3 cameraOffset;
    private Vector3 newCameraPosition;


    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = this.transform.position - TargetObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        newCameraPosition = TargetObject.position + cameraOffset;
        transform.position = newCameraPosition;
        
        //transform.position = Vector3.Lerp(this.transform.position, newCameraPosition, LerpSpeed * Time.deltaTime);
    }
}
