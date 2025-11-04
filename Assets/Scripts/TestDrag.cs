using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class TestDrag : MonoBehaviour
{
    public TestDrag dependent;
    public Rigidbody rb;
    private HingeJoint hinge;
    private Camera cam;

    private bool isDragging = false;
    private float dragDepth;
    private Vector3 dragOffset;

    public bool IsSnapped = false;

    [Header("Physics Drag Settings")]
    public float moveSpeed = 5f;
    public float maxVelocity = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
        cam = Camera.main;

        rb.useGravity = true;
        rb.linearDamping = 5f; // tương đương drag
    }

    private void Start()
    {
        HingeSetup();
    }

    void HingeSetup()
    {
        if(dependent != null)
            hinge.connectedBody = dependent.rb;
        hinge.anchor = Vector3.zero;
        hinge.axis = Vector3.forward;
        hinge.enableCollision = true;
    }

    private void OnMouseDown()
    {
        if (IsSnapped)
            return;
        Debug.Log($"Mouse Down {gameObject.name}");
        isDragging = true;

        Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);
        dragDepth = screenPoint.z;

        dragOffset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        if (IsSnapped)
            return;
        isDragging = false;
        rb.linearVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (IsSnapped)
            return;
        if (!isDragging) return;

        Vector3 targetPos = GetMouseWorldPosition() + dragOffset;
        Vector3 diff = targetPos - transform.position;
        diff.Normalize();

        // Set velocity theo hướng tới điểm kéo
        rb.linearVelocity = diff * moveSpeed;

        // Giới hạn tốc độ để không bị văng quá mạnh
        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = dragDepth;
        return cam.ScreenToWorldPoint(mousePos);
    }
}
