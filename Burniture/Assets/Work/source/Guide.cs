/*****************************
* Program : Guide
* Writer : 정지훈
* FinalModifycation : 20170808
******************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Guide : MonoBehaviour {

    private int pageNum = 0;

    public GameObject guideModel;                                               // Background Plane Object
    public GameObject guideWordModel;                                           // GuideWord
    public GameObject[] formalButtons;                                          // System Buttons
    public Texture2D[] guideImg;                                                // Guide Image
    public Texture2D[] guideWord;                                               // Guide Image

    // Use this for initialization
    void Start () {
        guideModel.SetActive(false);                                 // Background active false
    }

    // button on
    public void OnButton ()                                             // Info 버튼 클릭시 실행
    {
        guideModel.gameObject.SetActive(true);                                     // Background Plane Object 활성화
        for (int i = 0; i < formalButtons.Length; i++)                            // 버튼들 비활성화
        {
            formalButtons[i].SetActive(false);
        }
        pageNum = 0;                                                    // 설명 페이지 인덱스 초기화
        guideModel.GetComponent<RawImage>().texture = guideImg[pageNum];
        guideWordModel.GetComponent<RawImage>().texture = guideWord[pageNum];
    }

    public void OnNext ()
    {
        if(guideImg.Length > pageNum)
        {
            pageNum++;
            guideModel.GetComponent<RawImage>().texture = guideImg[pageNum];
            guideWordModel.GetComponent<RawImage>().texture = guideWord[pageNum];
        }
    }

    public void OnPrev ()
    {
        if (0 < pageNum)
        {
            pageNum--;
            guideModel.GetComponent<RawImage>().texture = guideImg[pageNum];
            guideWordModel.GetComponent<RawImage>().texture = guideWord[pageNum];
        }
    }

    public void OnCancel()
    {
        guideModel.SetActive(false);
        for (int i = 0; i < formalButtons.Length; i++)
        {
            formalButtons[i].SetActive(true);
        }
    }
}
