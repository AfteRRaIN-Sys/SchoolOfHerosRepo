using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DraftPanel : MonoBehaviour
{

    [SerializeField]
    DraftSO draftSO;
    [SerializeField]
    GameStateSO gameStateSO;

    public GameObject gameStateObj;


    // prefab
    public GameObject DraftArea;
    public GameObject CardInfoPopup;
    public Sprite studentCardSprite;
    public Sprite professorCardSprite;
    public GameObject CardPrefab;

    public RawImage rawImage;
    public Image decisionTable;
    
    public List<GameObject> studentCardObjList;

    public List<Student> students;
    public List<StudentCard> studentCards;

    public List<GameObject> professorCardObjList;

    public List<Professor> professors;
    public List<ProfessorCard> professorCards;

    public int maxSlctStudent;
    public int numSelected;
    
    public int currentPhrase;

    public int point;

    public int currentPhraseProperties {
        get {
            return currentPhrase;
        }
        set {
            if (value <= 2) {
                currentPhrase = value;
            }
            this.ChangePhrase();       
        }
    }

    // public TMP_Text nameText;

    public void Awake() {

        currentPhrase = 0;
        numSelected = 0;
        maxSlctStudent = 3;
        point = gameStateSO.point;
        //Debug.Log(point);
        //Debug.Log(gameStateSO.cur_sem);

        gameStateObj = GameObject.Find("GameState");
        // gameStateSO.slctTable = gameStateObj.GetComponent<GameState>().DraftStudentGenerator();
        
        students = gameStateObj.GetComponent<GameState>().DraftStudentGenerator();
        
        professors = gameStateObj.GetComponent<GameState>().DraftProfGenerator();
        this.ChangePhrase();
    }
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        int c = 0;
        
        int left_points = gameStateSO.point;
        if (currentPhrase == 0) {
            for (int i = 0; i<this.studentCards.Count;i++){
                if (this.studentCards[i].isSelected) {
                    c++;
                } else {
                    if (numSelected >= maxSlctStudent) {
                        studentCardObjList[i].GetComponent<Button>().interactable = false;
                        // studentCardObjList[i].GetComponentInChildren<Image>().color = Color.black;
                    }
                    else {
                        studentCardObjList[i].GetComponent<Button>().interactable = true;
                        // studentCardObjList[i].GetComponentInChildren<Image>().color = Color.red;
                    }
                }
            }
        } else {
            for (int i = 0; i<this.professorCards.Count;i++){
                if (this.professorCards[i].isSelected) {
                    c++;
                    left_points -= professorCards[i].professor.cost;
                } else {
                    if (point < professorCards[i].professor.cost) {
                        professorCardObjList[i].GetComponent<Button>().enabled = false;
                        professorCardObjList[i].GetComponentInChildren<Image>().color = new Color(1f, 0.1f, 0.1f, 0.7f);
                    }
                    else {
                        professorCardObjList[i].GetComponent<Button>().enabled = true;
                        professorCardObjList[i].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 0.4f);;
                    }
                }
            }
        }
        point = left_points;
        numSelected = c;
    }

    public void ChangePhrase(){
        if (currentPhrase == 0) {
            for (int i =0; i<5; i++) {
                // Student tmp = this.gameObject.AddComponent<Student>();
                GameObject studentCardObj = Instantiate(CardPrefab, new Vector3(0,0,0), Quaternion.identity);
                studentCardObj.transform.SetParent(DraftArea.transform, false);
                studentCardObj.GetComponent<Image>().sprite = studentCardSprite;

                // Student student = Student.CreateComponent(studentCardObj, $"student_{i}", i);
                // students.Add(student);
                Student student = studentCardObj.AddComponent<Student>();
                student.CopyStudent(students[i]);
                students[i] = student;
                StudentCard tmp2 = StudentCard.CreateComponent(studentCardObj, student);
                Debug.Log(tmp2.student.id);
                studentCards.Add(tmp2);
                
                // add script to btn
                // studentCardObj.GetComponent<Button>().gameObject.AddComponent<StudentCardBtn>();

                TMP_Text tmp = studentCardObj.GetComponentInChildren<TMP_Text>();
                tmp.text = student.name;
                tmp.fontSize = 30f;
                tmp.color = Color.white;

                studentCardObjList.Add(studentCardObj);
            }

        } else if (currentPhrase == 1) {
            Debug.Log($"Prof Phrase");

            // Sprite ProfCardBackGround = Resources.Load<Sprite>("Assets/Card_Shirts_Lite/PNG/Card_shirt_01") as Sprite;

            for (int i=0; i<this.studentCardObjList.Count;i++) {
                studentCardObjList[i].SetActive(false);
            }
            
            for (int i =0; i<professors.Count; i++) {
                
                GameObject professorCardObj = Instantiate(CardPrefab, new Vector3(0,0,0), Quaternion.identity);
                professorCardObj.GetComponent<Image>().sprite = professorCardSprite;
                professorCardObj.transform.SetParent(DraftArea.transform, false);

                // Professor professor = Professor.CreateComponent(professorCardObj, $"Professor_{i}", i);
                // professors.Add(professor);
                Professor professor = professorCardObj.AddComponent<Professor>();
                professor.CopyProfessor(professors[i]);
                professors[i] = professor;
                ProfessorCard tmp2 = ProfessorCard.CreateComponent(professorCardObj, professor);
                Debug.Log(tmp2.professor.id);
                professorCards.Add(tmp2);

                // add script to btn
                // professorCardObj.GetComponent<Button>().gameObject.AddComponent<StudentCardBtn>();

                // add txt to each studentCardObj
                // print($"GetComponentInChildren<TMP_Text>().text : {}");
                TMP_Text tmp = professorCardObj.GetComponentInChildren<TMP_Text>();
                tmp.text = professor.name;
                tmp.fontSize = 30f;
                tmp.color = Color.white;

                professorCardObjList.Add(professorCardObj);
            }
        }

        else if (currentPhrase == 2) {

            // slctStudents
            List<Student> slctStudents = new List<Student>();
            foreach(StudentCard s in studentCards) {
                if (s.isSelected) {
                    gameStateSO.studentList.Add(s.student);
                }
            }

            List<Professor> slctProfessors = new List<Professor>();
            foreach(ProfessorCard s in professorCards) {
                if (s.isSelected) {
                    gameStateSO.professorList.Add(s.professor);
                }
            }

            foreach(Student s in slctStudents){
                Debug.Log($"select s : {s.name}");
            }

            foreach(Professor p in slctProfessors){
                Debug.Log($"select p : {p.name}");
            }

            foreach (Student s in slctStudents) {
                gameStateSO.studentAvail[s.id] = false; 
            }
            foreach (Professor p in slctProfessors) {
                Debug.Log("p.id:" + p.id.ToString());
                Debug.Log("array lenght: " + gameStateSO.profAvail.Length.ToString());
                gameStateSO.profAvail[p.id] = false; 
            }

            gameStateSO.point = point;
            
            NextScene();
        }
    }

    public List<Skill> getAllSkillList() {
        return gameStateSO.skillList;
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
