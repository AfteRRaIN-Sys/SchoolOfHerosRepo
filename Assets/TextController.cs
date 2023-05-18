using System;
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

    [SerializeField]
    GameStateSO gameStateSO;

    string[] allPrologues = {"Once upon the time, the world has been invaded by the powerful beings....",
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

    string[] allBetweenlogues = {"'We won!!' cry the heroes, the crownd cheer as they returned as victorious",
                            "But you know that this is just the begining",
                            ".....",
                            "'I must start preparing for the next battle'",
                            "(Click to continue)"};

    string[] allEpilogues = {"Heroes are all celebrating thier hard fought victory against higher beings.",
                             "All their studies and training has finally been fruitful.",
                             "The heroes now reunion with thier mentor and begin the victory feast together.",
                             "The world has the peace it deserve. Everyone is happy. Such a 'happy ending'",
                             "...",
                             "Ofcourse, there still a long way to go for these awe-spiring heroes and many more new faces.",
                             "They still has to train nevertheless",
                            "As of now they has been titled 'Heroes of the World'.",
                            "They are the hope to shape this world better and better",
                            "The triumphant of heroes is the sign of the world's 'golden age'",
                            "....",
                            "...",
                            "..",
                            "'Or is it...?",
                            "This is the end of our demo. Thanks for playing!",
                            "(Click to exit)"};
    int dialoguePointer;
    void Start()
    {
        introTextBox.text = "";
        dialoguePointer = 0;
    }
    public void Continue ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("Continue Prologue..");
            if (dialoguePointer < allPrologues.Length)
            {
                if (dialoguePointer == 5)
                {
                    introTextBox.text = "";
                }
                introTextBox.text += allPrologues[dialoguePointer] + System.Environment.NewLine;
                dialoguePointer++;
            }
            else
            {
                CheckGameState();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if(gameStateSO.cur_sem == 2)
            {
                Debug.Log("Continue Epilogue..");
                if (dialoguePointer < allEpilogues.Length)
                {
                    if (dialoguePointer == 5)
                    {
                        introTextBox.text = "";
                    }
                    introTextBox.text += allEpilogues[dialoguePointer] + System.Environment.NewLine;
                    dialoguePointer++;
                }
                else
                {
                    CheckGameState();
                    SceneManager.LoadScene(0);
                }
            }
            else if (gameStateSO.cur_sem == 1)
            {
                Debug.Log("Continue After boss 1 logue");
                if (dialoguePointer < allBetweenlogues.Length)
                {
                    if (dialoguePointer == 5)
                    {
                        introTextBox.text = "";
                    }
                    introTextBox.text += allBetweenlogues[dialoguePointer] + System.Environment.NewLine;
                    dialoguePointer++;
                }
                else
                {
                    CheckGameState();
                    SceneManager.LoadScene(2);
                }
            }
            
        }
        
    }

    public void Skip()
    {
        Debug.Log("Skip Prologue.");
        
        CheckGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    private void CheckGameState()
    {
        Debug.Log("Current semester: " + gameStateSO.cur_sem.ToString());
        Debug.Log("Current points: " + gameStateSO.point.ToString());
        Debug.Log("Number of students: " + gameStateSO.studentAvail.Length.ToString());
        Debug.Log("Number of professors: " + gameStateSO.profAvail.Length.ToString());
    }
}



