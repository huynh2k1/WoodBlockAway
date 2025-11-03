using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollMenuCtrl : MonoBehaviour, IEndDragHandler
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
        Debug.Log("Target: " + targetPos);
        
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

    public void OnEndDrag(PointerEventData eventData)
    {
        ////nếu khoảng cách vị trí hiện tại - vị trí ban đầu > ngưỡng vuốt
        //if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        //{
        //    if (eventData.position.x > eventData.pressPosition.x)
        //    {
        //        Debug.Log("Vuốt sang phải");
        //        Previous();
        //    }
        //    else
        //    {
        //        Debug.Log("Vuốt sang trái");
        //        Next();
        //    }
        //}
        //else
        //{
        //    MovePage();
        //}
    }
}
