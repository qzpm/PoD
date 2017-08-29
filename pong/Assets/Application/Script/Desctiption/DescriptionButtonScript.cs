using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DescriptionButtonScript : MonoBehaviour {

    PanelcontrollerScript panel;

    void Start()
    {
        panel = GameObject.Find("Layout").GetComponent<PanelcontrollerScript>();
        Debug.Log(panel);
    }

    public void OnClickRightButton()
    {
        Debug.Log("右ボタン");
        // 最後のページでなかったら右ボタン有効
        if (panel.NowPage < PanelcontrollerScript.MAX_PAGE_NUM)
        {
            panel.Jump(panel.NowPage + 1);
        }
    }

    public void OnClickLeftButton()
    {
        Debug.Log("左ボタン");
        // 最初のページでなかったら左ボタン有効
        if (panel.NowPage > 1)
        {
            panel.Jump(panel.NowPage - 1);
        }
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
