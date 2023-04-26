using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    public RectTransform notificationWindow;
    public TMP_Text notificationText;
    public float timeToStop = 2f;

    public void Notify(string notif)
    {
        
        StopAllCoroutines();
        notificationText.text = notif;
        //notificationWindow.rect.size(notificationText.preferredWidth, notificationText.preferredHeight);
        notificationWindow.gameObject.SetActive(true);
        StartCoroutine(StartTimer());      
    }


    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToStop);

        HideMessages();
    }

    private void HideMessages()
    {
        notificationWindow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
