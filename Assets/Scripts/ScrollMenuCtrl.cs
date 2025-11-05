using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollMenuCtrl : MonoBehaviour
{
    [SerializeField] int maxPage;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float pageStep;
    [SerializeField] float tweenTime;
    float targetPos;
    public int currentPage;
    float dragThreshould;

    [SerializeField] Button _btnNext;
    [SerializeField] Button _btnPrev;

    private void Awake()
    {
        _btnNext.onClick.AddListener(Next);
        _btnPrev.onClick.AddListener(Previous);

        currentPage = 1;
        targetPos = levelPagesRect.anchoredPosition.x;

        dragThreshould = Screen.width / 20;

        CheckActiveBtnNext();
        CheckActiveBtnPrev();
    }

    private void OnEnable()
    {
        currentPage = 1;
        levelPagesRect.anchoredPosition = Vector2.zero;


        CheckActiveBtnNext();
        CheckActiveBtnPrev();
    }

    void Next()
    {
        if(currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    void Previous()
    {
        if(currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        
        targetPos = pageStep * (currentPage - 1);
        levelPagesRect.DOKill();
        levelPagesRect.DOAnchorPosX(targetPos, tweenTime).SetEase(Ease.Linear);

        CheckActiveBtnNext();
        CheckActiveBtnPrev();
    }

    void CheckActiveBtnNext()
    {
        bool isActive = (currentPage >= maxPage) ? false : true;
        _btnNext.gameObject.SetActive(isActive);
    }

    void CheckActiveBtnPrev()
    {
        bool isActive = (currentPage <= 1) ? false : true;
        _btnPrev.gameObject.SetActive(isActive);
    }

}
