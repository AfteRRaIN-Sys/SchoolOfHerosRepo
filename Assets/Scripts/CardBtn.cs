using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class CardBtn : MonoBehaviour
{
    // Start is called before the first frame update
    Button button;
    void Start()
    {
        button = this.transform.GetComponentInParent<Button>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });
    }

    void ButtonClicked() {
        if (this.gameObject.GetComponentInParent<Button>().GetComponentInParent<Student>()){
            Debug.Log("clicked student");
            this.gameObject.GetComponentInParent<Button>().GetComponentInParent<StudentCard>().OnClick();
        }
        else if (this.gameObject.GetComponentInParent<Button>().GetComponentInParent<Professor>()){
            Debug.Log("clicked professor");
            this.gameObject.GetComponentInParent<Button>().GetComponentInParent<ProfessorCard>().OnClick();
        }
    }
    
}
