using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBtn : MonoBehaviour
{
    // Start is called before the first frame update
    //public Button button;

    public GameStateSO gameStateSO;
    public CanvasGroup loadingScreen;
    [SerializeField]
    int numStudent = 15;
    [SerializeField]
    int numProf = 11;
    [SerializeField]
    int numSkill = 37;


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
        loadingScreen.alpha = 1;
        InitGame();
        StartCoroutine(Delay(1.5f));
    }

    public void Tutorial()
    {
        Debug.Log("Game Tutorial");
        Debug.Log("...WIP");
    }

    public void Setting()
    {
        Debug.Log("Game Settings");
        Debug.Log("...WIP");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
    void InitGame()
    {
        gameStateSO.newGame = true;
        gameStateSO.cur_sem = 1;
        gameStateSO.money = 1000;
        gameStateSO.point = 500;
        gameStateSO.studentList = new List<Student>();
        gameStateSO.professorList = new List<Professor>();
        gameStateSO.roomList = new List<Room>();
        gameStateSO.studentAvail = new bool[numStudent];
        for (int i =0; i< numStudent; i++) {
            gameStateSO.studentAvail[i] = true;
        }
        gameStateSO.profAvail = new bool[numProf];
        for (int i =0; i< numProf; i++) {
            gameStateSO.profAvail[i] = true;
        }

        List<Skill> skillList = new List<Skill>();
        for (int i =0; i<numSkill; i++) {
            skillList.Add(new Skill(i+1));
        }
        int[] progressLeftTemplate = new int[numSkill];
        for (int i =0; i<numSkill; i++) {
            progressLeftTemplate[i] = skillList[i].turnsToComplete;
        }
        gameStateSO.skillList = skillList;
        gameStateSO.progressLeftTemplate = progressLeftTemplate;
    }

    IEnumerator Delay(float sec)
    {
        Debug.Log("Waiting..");       
        yield return new WaitForSecondsRealtime(sec);

        loadingScreen.alpha = 0;
        NextScene();
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}