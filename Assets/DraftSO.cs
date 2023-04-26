using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class DraftSO : ScriptableObject
{
    [SerializeField]
    public List<Student> studentList { get; set; }
    
    [SerializeField]
    public List<Professor> professorList { get; set; }

    

}