using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private Scrollbar scrollBar;
    [SerializeField] private Transform[] circleContents;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private float swipeTime = 0.2f;
    [SerializeField] private float swipeDistance = 50f;
    [SerializeField] private float circleContentScale = 1.2f;

    private float[] scrollPageValues;
    [Range(0f, 1f)]private float valueDistance = 0;
    private int curPage = 0;
    private int maxPage = 0;
    private float startTouchX;
    private float endTouchX;
    private bool isSwipeMode = false;
    

    void Awake()
    {
        InitValue();
    }

    void Start()
    {
        leftButton.onClick.AddListener(() => LeftRightButton(-1));
        rightButton.onClick.AddListener(() => LeftRightButton(1));
    }

    void OnEnable()
    {
        
    }

    void Update()
    {
        UpdateInput();
        UpdateCircleContent();
    }

    private void InitValue()
    {
        content.anchoredPosition3D = Vector3.zero;

        scrollPageValues = new float[transform.childCount];

        valueDistance = 1f / (scrollPageValues.Length - 1f);

        for(int i = 0; i < scrollPageValues.Length; i++)
            scrollPageValues[i] = valueDistance * i;

        maxPage = transform.childCount;

        scrollBar.value = 0;
    }

    public void SetScrollBarValue(int idx)
    {
        curPage = idx;
        scrollBar.value = scrollPageValues[idx];
        content.anchoredPosition3D = new Vector3(-180f * idx, 0, 0);
    }

    private void LeftRightButton(int value)
    {
        int page = curPage + value;
        if(page > maxPage - 1 || page < 0) return;
        
        curPage = page;
        StartCoroutine(OnSwipeOneStep(page));
    }

    private void UpdateInput()
    {
        if(isSwipeMode == true) return;

        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouchX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }
            
        #endif
    }

    private void UpdateSwipe()
    {
        if(Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(curPage));
            return;
        }

        bool isRight = startTouchX < endTouchX ? true : false;

        if (isRight == true)
        {
            if(curPage == 0) return;
            curPage--;
        }
        else
        {
            if(curPage == maxPage - 1) return;
            curPage++;
        }
        StartCoroutine(OnSwipeOneStep(curPage));
    }

    private void UpdateCircleContent()
    {
        for(int i = 0; i < scrollPageValues.Length; i++)
        {
            circleContents[i].localScale = Vector2.one;
            Color color = circleContents[i].GetComponent<Image>().color;
            color = Color.white;

            if(scrollBar.value < scrollPageValues[i] + (valueDistance / 2) &&
                scrollBar.value > scrollPageValues[i] - (valueDistance / 2))
            {
                circleContents[i].localScale = Vector2.one * circleContentScale;
                color = Color.black;
            }
        }
    }

    private IEnumerator OnSwipeOneStep(int idx)
    {
        float start = scrollBar.value;
        float current = 0;
        float percent = 0;

        isSwipeMode = true;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;

            scrollBar.value = Mathf.Lerp(start, scrollPageValues[idx], percent);
            yield return null;
        }
        isSwipeMode = false;
    }


}
