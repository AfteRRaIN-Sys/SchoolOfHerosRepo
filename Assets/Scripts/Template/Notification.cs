using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    [SerializeField]
    float timeToStop = 2f;
    //[SerializeField]
    //float notificationInterval = 1f;
    [SerializeField]
    GameObject notificationWindow;
    [SerializeField]
    GameObject notificationBlock;

    public void Notify(string notification)
    {
        GameObject notifButton = Instantiate(notificationBlock, Vector3.zero, Quaternion.identity, notificationWindow.transform);
        TMP_Text notifText = notifButton.GetComponentInChildren<TMP_Text>();
        notifText.text = notification;
        StartCoroutine(Dismiss(notifButton));
    }
    /*
    IEnumerator DelayedNotification(string notification)
    {
        yield return new WaitForSeconds(notificationInterval);
        GameObject notifButton = Instantiate(notificationBlock, Vector3.zero, Quaternion.identity, notificationWindow.transform);
        TMP_Text notifText = notifButton.GetComponentInChildren<TMP_Text>();
        notifText.text = notification;
        StartCoroutine(Dismiss(notifButton));
    }
    */
    private IEnumerator Dismiss(GameObject notification)
    {
        yield return new WaitForSeconds(timeToStop);

        Destroy(notification);
    }
}
