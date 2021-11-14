using UnityEngine;

public class ShipController : MonoBehaviour
{
    // ForwardSpeed = forwards/backwards
    // StrafeSpeed = left/right
    // HoverSpeed = up/down
    public float ForwardSpeed = 15f, StrafeSpeed = 5f, HoverSpeed = 5f;
    public float ForwardAcceleration = 2.5f, StrafeAcceleration = 2.5f, HoverAcceleration = 2.5f;

    private float currentForwardSpeed, currentStrafeSpeed, currentHoverSpeed;

    public float LookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float RollSpeed = 90f, RollAcceleration = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Confined; // keep cursor on screen
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), RollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * LookRateSpeed * Time.deltaTime, mouseDistance.x * LookRateSpeed * Time.deltaTime, rollInput * RollSpeed * Time.deltaTime, Space.Self);

        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, Input.GetAxis("Vertical") * ForwardSpeed, ForwardAcceleration * Time.deltaTime);
        currentStrafeSpeed = Mathf.Lerp(currentStrafeSpeed, Input.GetAxis("Horizontal") * StrafeSpeed, StrafeAcceleration * Time.deltaTime);
        currentHoverSpeed = Mathf.Lerp(currentHoverSpeed, Input.GetAxis("Hover") * HoverSpeed, HoverAcceleration * Time.deltaTime);

        transform.position += (currentForwardSpeed * transform.forward) + (currentStrafeSpeed * transform.right.normalized) + (currentHoverSpeed * transform.up.normalized);

    }
}
