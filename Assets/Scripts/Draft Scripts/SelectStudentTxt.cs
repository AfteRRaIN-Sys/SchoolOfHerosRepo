using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectStudentTxt : MonoBehaviour
{
    // Start is called before the first frame update

    // public GameObject DraftArea;
    // public DraftPanel DraftPanel;
    public TMP_Text txt;

    Color gold = new Color(0.937255f, 0.6666667f, 0.2509804f, 1f);

    // Update is called once per frame
    void Update()
    {
        
        GameObject gm = GameObject.Find("DraftArea");
        int numSelected = gm.GetComponent<DraftPanel>().numSelected;
        int point = gm.GetComponent<DraftPanel>().point;
        int currentPhrase = gm.GetComponent<DraftPanel>().currentPhrase;
        int maxSlctStudent = gm.GetComponent<DraftPanel>().maxSlctStudent;

        if (currentPhrase == 0) {
            txt.text = $"Select Student [{numSelected}/{maxSlctStudent}]";
            if (numSelected >= maxSlctStudent) {
                txt.color = Color.red;
            }
            else {
                // txt.color = Color.white;
                txt.color = gold;
            }
        }
        else if (currentPhrase == 1) {
            txt.text = $"Purchase Professor, Available points : {point}";
            if (point <= 0) {
                txt.color = Color.red;
            }
            else {
                txt.color = gold;
            }
        }

        
    }
}
