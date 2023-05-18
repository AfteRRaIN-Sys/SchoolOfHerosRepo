using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1
using TMPro;

public class ToggleTable : MonoBehaviour
{
    Button button;
    Image toggleBtnImage;
    public Button toggleSkillTreeButton;

    public GameObject content;

    public Sprite x;
    public Sprite eye;

    public void Start(){
        button = this.transform.GetComponentInParent<Button>();
        toggleBtnImage = this.transform.GetComponentInParent<Image>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });
    }

    public void ButtonClicked() {
        Debug.Log("toggle clicked");
        // RawImage r = GameObject.Find("DraftArea").GetComponent<DraftPanel>().rawImage;
        // r.gameObject.SetActive(!r.gameObject.activeSelf);

        Image table = GameObject.Find("DraftArea").GetComponent<DraftPanel>().decisionTable;
        table.gameObject.SetActive(!table.gameObject.activeSelf);

        toggleSkillTreeButton.gameObject.SetActive(!toggleSkillTreeButton.gameObject.activeSelf);

        content.GetComponentInChildren<TMP_Text>().text = GameObject.Find("DraftArea").GetComponent<DraftPanel>().allSkillInfoText;

        CanvasGroup slctStudentTxt = GameObject.Find("Background").GetComponentInChildren<CanvasGroup>();
        // Debug.Log($"clicked : txt : {slctStudentTxt.text}");
        if (slctStudentTxt.alpha == 0f) {
            slctStudentTxt.alpha = 1f;
        } else {
            slctStudentTxt.alpha = 0f;
        }

        if (table.gameObject.activeSelf){
            toggleBtnImage.sprite = x;
            toggleBtnImage.color = Color.green;

        } else {
            toggleBtnImage.sprite = eye;
            toggleBtnImage.color = Color.white;
        }
        

        // currentPhrase += 1;
    }
}
