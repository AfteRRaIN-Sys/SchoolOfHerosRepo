using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{
    [SerializeField]
    TMP_Text introTextBox;
    string[] allDialogues = {"Once upon the time, the world has been invaded by the powerful beings....",
                             "They, are very very powerful, and their intelligence exceed any other beings...",
                             "They, want to seize control over this pathetic world...",
                             "They even send the warning messeges to the world for more 'entertaining' means...",
                             "...",
                             "Of course, the world's goverment won't just sit around and do nothing.",
                             "They build this 'School of Heroes' to fight against this disaster.",
                            "They recruit people all over the world. Gender, age, doesn't matter..",
                            "They started training and training in the hope to turn these 'Heroes' to have the abilities, knowledge and intelligence.",
                            "And hope to overcome the higher beings from the another world...",
                            "....",
                            "...",
                            "..",
                            "This is your story as the founder of the 'School of Heroes'...!",
                            "(Click to continue)"};
    int dialoguePointer;
    void Start()
    {
        introTextBox.text = "";
        dialoguePointer = 0;
    }
    public void Continue ()
    {
        Debug.Log("Clicked");
        if(dialoguePointer < allDialogues.Length)
        {
            if(dialoguePointer == 5)
            {
                introTextBox.text = "";
            }
            introTextBox.text += allDialogues[dialoguePointer] + System.Environment.NewLine;
            dialoguePointer++;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}



