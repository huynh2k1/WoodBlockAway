using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public int ID;

    [SerializeField] Button _btn;
    [SerializeField] Text _txtLevel;
    [SerializeField] GameObject _lockObject;

    public event Action<int> OnClickThisEvent;

    private void Awake()
    {
        _btn.onClick.AddListener(HandleOnClickEvent);
    }

    void HandleOnClickEvent()
    {
        OnClickThisEvent?.Invoke(ID); 
    }

    public void Initialize(int index)
    {
        ID = index;
        CheckUnlock();
        UpdateTextLevel();
    }

    void UpdateTextLevel()
    {
        _txtLevel.text = (ID + 1).ToString(); 
    }

    public void CheckUnlock()
    {
        if (PlayerPrefData.CurLevelUnlock >= ID)
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }

    void Unlock()
    {
        _lockObject.SetActive(false);
        InteractableButton(true);   
    }

    void Lock()
    {
        _lockObject.SetActive(true);
        InteractableButton(false);
    }

    void InteractableButton(bool isInteractable)
    {
        _btn.interactable = isInteractable;
        _btn.transform.GetComponent<Image>().raycastTarget = isInteractable;
        _lockObject.SetActive(!isInteractable);
    }
}
