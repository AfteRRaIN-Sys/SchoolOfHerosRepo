using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfoTxtScript : MonoBehaviour
{

    void Start()
    {
        
        Debug.Log($"Card info {this.transform.parent.GetComponent<StudentCard>().student.id}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
