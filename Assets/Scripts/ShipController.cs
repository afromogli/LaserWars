using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // ForwardSpeed = forwards/backwards
    // StrafeSpeed = left/right
    // HoverSpeed = up/down
    public float ForwardSpeed = 25f, StrafeSpeed = 25f, HoverSpeed = 5f;
    public float ForwardAcceleration = 5f, StrafeAcceleration = 2.5f, HoverAcceleration = 2.5f;

    private float currentForwardSpeed, currentStrafeSpeed, currentHoverSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, Input.GetAxis("Vertical") * ForwardSpeed, ForwardAcceleration * Time.deltaTime);
        currentStrafeSpeed = Mathf.Lerp(currentStrafeSpeed, Input.GetAxis("Horizontal") * StrafeSpeed, StrafeAcceleration * Time.deltaTime);
        currentHoverSpeed = Mathf.Lerp(currentHoverSpeed, Input.GetAxis("Hover") * HoverSpeed, HoverAcceleration * Time.deltaTime);

        currentForwardSpeed = Mathf.Min(currentForwardSpeed, ForwardSpeed);
        currentStrafeSpeed = Mathf.Min(currentStrafeSpeed, StrafeSpeed);
        currentHoverSpeed = Mathf.Min(currentHoverSpeed, HoverSpeed);

        transform.position += (currentForwardSpeed * transform.forward) + (currentStrafeSpeed * transform.right) + (currentHoverSpeed * transform.up);
    }
}
