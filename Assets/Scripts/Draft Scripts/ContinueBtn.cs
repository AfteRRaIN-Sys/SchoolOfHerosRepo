using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1

public class ContinueBtn : MonoBehaviour
{
    Button button;
    Image img;

    public Sprite enabled;
    public Sprite disabled;

    public void Start(){
        button = this.transform.GetComponentInParent<Button>();
        img = this.transform.GetComponentInParent<Image>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });
    }

    public void ButtonClicked() {
        Debug.Log("continue clicked");
        GameObject gm = GameObject.Find("DraftArea");
        int currentPhrase = gm.GetComponent<DraftPanel>().currentPhrase;
        gm.GetComponent<DraftPanel>().currentPhraseProperties = currentPhrase+1;
        // currentPhrase += 1;
    }

    void Update()
    {
        
        GameObject gm = GameObject.Find("DraftArea");
        int numSelected = gm.GetComponent<DraftPanel>().numSelected;
        int point = gm.GetComponent<DraftPanel>().point;
        int currentPhrase = gm.GetComponent<DraftPanel>().currentPhrase;
        int maxSlctStudent = gm.GetComponent<DraftPanel>().maxSlctStudent;

        if (currentPhrase == 0) {
            if (numSelected == maxSlctStudent) {
                // img.color = Color.green;
                img.sprite = enabled;
                button.enabled = true;
                // ColorBlock cb = button.colors;
                // cb.normalColor = new Color(74,219,94,255);
            }
            else {
                // img.color = Color.red;
                img.sprite = disabled;
                button.enabled = false;
            }
        }
    }
}
