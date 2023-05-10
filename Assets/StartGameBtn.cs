using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;

    public GameStateSO gameStateSO;

    public void Start(){
        button = this.transform.GetComponentInParent<Button>();
        button.onClick.AddListener(delegate () { this.ButtonClicked(); });
    }
    
    public void InitGame() {
        gameStateSO.money = 1000;
        gameStateSO.point = 700;
        gameStateSO.studentList = new List<Student>();
        gameStateSO.professorList = new List<Professor>();
    }

    public void ButtonClicked()
    {
        Debug.Log("start btn clicked");
        InitGame();
        NextScene();
    }
       void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}