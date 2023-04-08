using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Narrative : MonoBehaviour
{
    public RectTransform panelTransform;
    public RectTransform shownTransform;
    private Vector3 targetPosition;
    private float moveTime = 1f;
    public bool isOpen = false;
    private bool skip = false;
    private bool readyToClose = false;
    private int _moveIndex;
    public GameObject UIRecordPrefab;

    public NarrativeContent[] content;

    public void RunPreMoveText(int moveIndex) {
        _moveIndex = moveIndex;
        if (content.Length>_moveIndex && content[_moveIndex] != null)
            StartCoroutine(NarrativeWindowProcess());
    }

    private IEnumerator NarrativeWindowProcess() {
        Wipe();
        skip = false;
        readyToClose = false;
        targetPosition = shownTransform.position;
        yield return StartCoroutine(MovePanel());
        isOpen = true;
        string[] replicas = content[_moveIndex].stringArray;
        for (int i = 0; i < replicas.Length; i++)
        {
            GameObject record = Instantiate(UIRecordPrefab);
            record.transform.SetParent(panelTransform);
            record.GetComponentInChildren<TextMeshProUGUI>().text = replicas[i];
            if (content[_moveIndex].spriteArray!= null && content[_moveIndex].spriteArray.Length>i)
                record.GetComponentInChildren<Image>().sprite = content[_moveIndex].spriteArray[i];
            if (!skip)
                yield return new WaitForSeconds(0.5f);
        }
        readyToClose = true;
    }

    private void Update()
    {
        if (isOpen)
            if (Mouse.current.leftButton.wasPressedThisFrame)
                if (readyToClose)
                {
                    targetPosition+= Vector3.up*800;
                    StartCoroutine(MovePanel());
                    isOpen = false;
                }
                else
                {
                    skip = true;
                }
    }

    private IEnumerator MovePanel() {
        float timer = 0f;
        Vector3 savedPos = panelTransform.position;
        while (timer < moveTime) {
            panelTransform.position = Vector3.Lerp(savedPos, targetPosition, timer / moveTime);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    private void Wipe() {
        for (int i = panelTransform.childCount-1; i >= 0; i--) {
            Destroy(panelTransform.GetChild(i).gameObject);
        }
    }
}