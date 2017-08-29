using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonScript : MonoBehaviour {
    public void GoSkillSelect()
    {
        SceneManager.LoadScene("SkillSelect");
    }

    public void GoGameDescription()
    {
        SceneManager.LoadScene("DescriptionScene");
    }



}
