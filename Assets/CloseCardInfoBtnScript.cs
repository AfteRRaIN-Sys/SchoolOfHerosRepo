using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseCardInfoBtnScript : MonoBehaviour
{
    Button button;

    public void Start(){
        button = this.transform.GetComponentInParent<Button>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });
    }

    public void ButtonClicked()
    {
        Debug.Log("x btn clicked");
        GameObject.Find("DraftArea").GetComponent<DraftPanel>().CardInfoPopup.SetActive(false);
    }
}
