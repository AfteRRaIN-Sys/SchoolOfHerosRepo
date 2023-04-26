// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class DescBox : MonoBehaviour
// {
//     public Text descText;
//     public Unit player;
    
//     // Start is called before the first frame update
//     public void Start()
//     {
//         descText.gameObject.SetActive(false);

//         string list = "";
//         for(int i=0;i<10;i++){
//             if(player.skills[i]==1)
//                 list += i.ToString() + ", ";
//         }
//         descText.text = player.name + "\nATK =  "+player.damage + "\nSkills : " + list;
//     }

//     // Update is called once per frame
//     public void OnMouseOver()
//     {
//         string list = "";
//         for(int i=0;i<10;i++){
//             if(player.skills[i]==1)
//                 list += i.ToString() + ", ";
//         }
//         descText.text = player.name + "\nATK =  "+player.damage + "\nSkills : " + list;

//         descText.gameObject.SetActive(true);
//     }

//         public void OnMouseExit()
//     {
//         descText.gameObject.SetActive(false);
//     }
// }
