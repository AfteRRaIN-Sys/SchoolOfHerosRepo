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
        gameStateSO.money = 1000;
        gameStateSO.point = 700;
        gameStateSO.studentList = new List<Student>();
        gameStateSO.professorList = new List<Professor>();
        gameStateSO.studentAvail = new bool[15];
        for (int i =0; i<15;i++) {
            gameStateSO.studentAvail[i] = true;
        }
        gameStateSO.profAvail = new bool[7];
        for (int i =0; i<7;i++) {
            gameStateSO.profAvail[i] = true;
        }

        List<Skill> skillList = new List<Skill>();
        for (int i =0; i<37; i++) {
            skillList.Add(new Skill(i+1));
        }
        int[] progressLeftTemplate = new int[37];
        for (int i =0; i<37; i++) {
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