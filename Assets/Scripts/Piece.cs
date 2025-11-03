using System;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool IsSnapped = false;
    public Piece dependentPiece;

    public Pitch dependentPitch;

    [SerializeField] Material _woodSnap;
    [SerializeField] Material _woodNormal;
    [SerializeField] Material _woodHover;
    private Rigidbody _rb;
    private HingeJoint _hinge;
    private Renderer _renderer;

    public TransformModel CorrectTransform;


    private bool _isDragging = false;
    private float _dragSpeed = 200f; // tốc độ xoay của motor

    [Header("Snap Settings")]
    public float snapPositionThreshold = 0.2f;   // độ lệch vị trí cho phép
    public float snapRotationThreshold = 5f;    // độ lệch góc cho phép

    public event Action OnPieceSnappedEvent;

    public float offset;

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
    }

    void LoadComponents()
    {
        if(_renderer == null)
            _renderer = GetComponent<Renderer>();   
        if(_rb == null)
            _rb = GetComponent<Rigidbody>();
        if(_hinge == null && !IsSnapped)
            _hinge = GetComponent<HingeJoint>();
        if (IsSnapped)
            Snap();
    }

    private void OnMouseDown()
    {
        if (IsSnapped) return;
        _isDragging = true;

        // Bật motor khi kéo
        var motor = _hinge.motor;
        motor.force = 100f;
        _hinge.motor = motor;
        _hinge.useMotor = true;
        Hover();
    }

    private void OnMouseDrag()
    {
        if (!_isDragging || IsSnapped) return;

        // Lấy vị trí chuột
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distance;
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);

        //// Tính góc mục tiêu theo hướng chuột
        Vector3 dir = worldMouse - transform.position;
        dir.y = 0f;
        dir.Normalize();
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Debug.DrawLine(transform.position, transform.position + dir.normalized * 2, Color.red);
        // Hướng quay
        float currentAngle = transform.eulerAngles.y;
        float delta = Mathf.DeltaAngle(currentAngle, targetAngle);

        // Điều khiển motor
        var motor = _hinge.motor;
        motor.targetVelocity = Mathf.Clamp(delta * _dragSpeed, -500f, 500f);
        _hinge.motor = motor;

        TrySnap();
    }

    private void OnMouseUp()
    {
        if (IsSnapped)
            return;

        Normal();
        // Tắt motor khi thả
        _hinge.useMotor = false;
        _rb.angularVelocity = Vector3.zero;
        _isDragging = false;

    }

    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180)
            angle -= 360;
        return angle;
    }

    public void Snapped()
    {
        if(_woodSnap)
            _renderer.material = _woodSnap;
    }

    public void Hover()
    {
        if(_woodHover)
            _renderer.material = _woodHover;
    }

    public void Normal()
    {
        if(_woodNormal)
            _renderer.material = _woodNormal;
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

        if(_hinge != null)
            _hinge.useMotor = false;
        _rb.isKinematic = true;

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