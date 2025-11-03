using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class HingeDrag3D : MonoBehaviour
{
    private HingeJoint hinge;
    private Rigidbody rb;

    private float startDragAngle;
    private Vector3 pivotWorld;

    [SerializeField] float motorForce = 800f;
    [SerializeField] float motorSpeedMultiplier = 10f;
    [SerializeField] float maxMotorSpeed = 250f;

    Camera cam;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        // Đảm bảo quay quanh trục Z đúng
        hinge.axis = Vector3.forward;
    }

    void OnMouseDown()
    {
        pivotWorld = transform.TransformPoint(hinge.anchor);
        startDragAngle = GetCurrentAngle();
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(cam.transform.position, pivotWorld);

        Vector3 mouseWorld = cam.ScreenToWorldPoint(mousePos);

        Vector2 dir = (mouseWorld - pivotWorld);
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float currentAngle = GetCurrentAngle();
        float delta = Mathf.DeltaAngle(currentAngle, targetAngle);

        JointMotor motor = hinge.motor;
        motor.force = motorForce;

        motor.targetVelocity = Mathf.Clamp(delta * motorSpeedMultiplier,
                                           -maxMotorSpeed,
                                            maxMotorSpeed);

        hinge.motor = motor;
        hinge.useMotor = true;
    }

    void OnMouseUp()
    {
        hinge.useMotor = false;
    }

    float GetCurrentAngle()
    {
        return transform.localEulerAngles.z;
    }
}
