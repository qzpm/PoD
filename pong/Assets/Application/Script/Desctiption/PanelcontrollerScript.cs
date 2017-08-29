using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelcontrollerScript : MonoBehaviour {
    public const int MAX_PAGE_NUM = 5;
    private int nowPage;
    private Transform transformPanel;

    public int NowPage
    {
        get
        {
            return nowPage;
        }
        set
        {
            nowPage = value;
        }
    }

    public Transform TransformPanel
    {
        get
        {
            return transformPanel;
        }
        set
        {
            transformPanel = value;
        }
    }

    private void Start()
    {
        transformPanel = GameObject.Find("Layout").GetComponent<Transform>();
        nowPage = 1;  // 最初は1ページから
    }

    public void Jump(int page)
    {
        NowPage = page;
        Vector3 pos = TransformPanel.localPosition;
        pos.x = (-1920) * (page - 1);          // 最初の座標が-300で1ページごとに-800
        TransformPanel.localPosition = pos;
    }
}
