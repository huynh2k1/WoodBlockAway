using NaughtyAttributes;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Piece[] pieces;

    [SerializeField] Pitch _pitchPrefab;

    #region EDITOR

    [Button]
    public void SaveCorrectTransform()
    {
        if (pieces == null || pieces.Length == 0)
            return;
        foreach (var piece in pieces)
        {
            piece.SaveCorrectTransform();
        }
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
    #endregion

    private void Start()
    {
        SpawnPitch();
    }

    private void OnEnable()
    {
        foreach(var p in pieces)
        {
            p.OnPieceSnappedEvent += CheckCompleteLevel;
        }
    }

    private void OnDestroy()
    {
        foreach (var p in pieces)
        {
            p.OnPieceSnappedEvent -= CheckCompleteLevel;
        }
    }

    #region PITCH LOGIC
    void SpawnPitch()
    {
        foreach (var p in pieces)
        {
            if (p.IsSnapped)
                continue;
            Pitch pitch = Instantiate(_pitchPrefab);
            p.dependentPitch = pitch;
            pitch.transform.position = p.transform.position;
            pitch.transform.SetParent(p.transform, true);
        }
    }

    #endregion

    public void CheckCompleteLevel()
    {
        if (IsAllPieceSnapped())
        {
            Debug.Log("WIN!!!");
            GameControl.I.WinGame();
        }
    }

    bool IsAllPieceSnapped()
    {
        foreach(var p in pieces)
        {
            if (!p.IsSnapped)
                return false;
        }
        return true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
