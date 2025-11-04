using System;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Piece : MonoBehaviour
{
    public bool IsSnapped = false;
    public TransformModel CorrectTransform;

    [Header("Snap Settings")]
    public float snapPositionThreshold = 0.2f;   // độ lệch vị trí cho phép
    public float snapRotationThreshold = 5f;    // độ lệch góc cho phép

    [Header("References")]
    public MatSO matData;
    public Piece dependentPiece;
    public Pitch dependentPitch;


    //Components
    public Rigidbody rb { get; set; }
    private HingeJoint hinge;
    private Renderer _renderer;
    private Camera cam;

    //Dragable
    [Header("Physics Drag Settings")]
    public float moveSpeed = 5f;
    public float maxVelocity = 8f;

    private bool isDragging = false;
    private float dragDepth;
    private Vector3 dragOffset;

    public event Action OnPieceSnappedEvent;

    private void Awake()
    {
        Initialize();
    }

    #region EDITOR
    public void SaveCorrectTransform()
    {
        CorrectTransform.Position = transform.position;
        CorrectTransform.Rotation = transform.eulerAngles;
    }
    #endregion

    void Initialize()
    {
        LoadComponents();
        HingeSetup();
    }

    void LoadComponents()
    {
        cam = Camera.main;

        if (_renderer == null)
            _renderer = GetComponent<Renderer>();   
        if(rb == null)
            rb = GetComponent<Rigidbody>();
        if(hinge == null && !IsSnapped)
            hinge = GetComponent<HingeJoint>();
        if (IsSnapped)
            Snap();
    }

    void HingeSetup()
    {
        if (dependentPiece != null)
            hinge.connectedBody = dependentPiece.rb;
        if (hinge == null)
            return;
        hinge.anchor = Vector3.zero;
        hinge.axis = Vector3.forward;
        hinge.enableCollision = true;
    }

    private void OnMouseDown()
    {
        if (IsSnapped) return;
        isDragging = true;


        Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);
        dragDepth = screenPoint.z;

        dragOffset = transform.position - GetMouseWorldPosition();
        Hover();
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
        TrySnap();
    }

    private void OnMouseUp()
    {
        if (IsSnapped)
            return;

        Normal();
        // Tắt motor khi thả
        isDragging = false;
        rb.linearVelocity = Vector3.zero;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = dragDepth;
        return cam.ScreenToWorldPoint(mousePos);
    }

    public void Snapped()
    {
        if(matData)
            _renderer.material = matData.woodSnap;
    }

    public void Hover()
    {
        if(matData)
            _renderer.material = matData.woodHover;
    }

    public void Normal()
    {
        if(matData)
            _renderer.material = matData.woodNormal;
    }

    private void TrySnap()
    {
        if (dependentPiece.IsSnapped == false)
            return;
        float distance = Vector3.Distance(transform.position, CorrectTransform.Position);
        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, CorrectTransform.Rotation.y));

        if (distance <= snapPositionThreshold && angleDiff <= snapRotationThreshold)
        {
            Snap();
        }
    }

    private void Snap()
    {
        IsSnapped = true;

        PitchEffect();
        Snapped();

        rb.isKinematic = true;

        transform.position = CorrectTransform.Position;
        transform.rotation = Quaternion.Euler(CorrectTransform.Rotation);

        OnPieceSnappedEvent?.Invoke();
        Debug.Log($"✅ {gameObject.name} snapped!");
    }


    void PitchEffect()
    {
        if (dependentPitch == null)
            return;
        dependentPitch.PitchEffect();
    }
}


[Serializable]
public class TransformModel
{
    public Vector3 Position;
    public Vector3 Rotation;
}