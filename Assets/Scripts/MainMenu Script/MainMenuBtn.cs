using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;

    public GameStateSO gameStateSO;

    /*
    public void Start(){
        
        button = this.transform.GetComponentInParent<Button>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });

    }

    public void ButtonClicked()
    {
        Debug.Log("start btn clicked");
        InitGame();
        NextScene();
    }
    */
    public void StartGame()
    {
        Debug.Log("Starting Game");
        InitGame();
        NextScene();
    }

    public void Tutorial()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
    void InitGame()
    {
        gameStateSO.money = 1000;
        gameStateSO.point = 700;
        gameStateSO.studentList = new List<Student>();
        gameStateSO.professorList = new List<Professor>();
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}