using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1

public class ToggleTable : MonoBehaviour
{
    Button button;

    public void Start(){
        button = this.transform.GetComponentInParent<Button>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });
    }

    public void ButtonClicked() {
        Debug.Log("toggle clicked");
        RawImage r = GameObject.Find("DraftArea").GetComponent<DraftPanel>().rawImage;
        r.gameObject.SetActive(!r.gameObject.activeSelf);
        // currentPhrase += 1;
    }
}
