using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    GameObject prevSceneData;
    // Start is called before the first frame update
    void Start()
    {
        GameManager prev = prevSceneData.GetComponent<GameManager>();
        Debug.Log(prev.getMoney());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendData(GameObject prevScene)
    {
        prevSceneData = prevScene;
    }
}
