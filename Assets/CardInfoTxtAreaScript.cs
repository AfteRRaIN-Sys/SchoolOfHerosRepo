using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfoTxtAreaScript : MonoBehaviour
{

    public List<string> infoTxt;
    public TMP_Text txt;

    public void Start() {
        // txt = this.GetComponentInParent<TMP_Text>();
        // txt = this.GetComponentInParent<InfoTxt>();
    }
    
    public List<string> infoTxtProperties {
        get {
            return infoTxt;
        }
        set {
            infoTxt = value;
            this.ChangeInfo();       
        }
    }

    public void ChangeInfo() {
        string tmp = "";
        foreach (string e in infoTxt) {
            tmp += $"{e}\n\n";
        }
        Debug.Log("txttt" + this.txt.text);
        this.txt.text = tmp;
    }
}
