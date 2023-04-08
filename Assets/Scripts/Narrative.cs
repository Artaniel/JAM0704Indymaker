using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Narrative : MonoBehaviour
{
    public RectTransform panelTransform;
    public TextMeshProUGUI mainText;
    public RectTransform shownTransform;
    private Vector3 targetPosition;
    private float moveTime = 1f;
    public bool isOpen = false;
    private bool skip = false;
    private bool readyToClose = false;
    private int _moveIndex;

    public string[] texts;

    public void RunPreMoveText(int moveIndex) {
        mainText.text = "";
        _moveIndex = moveIndex;
        if (texts.Length>_moveIndex && texts[_moveIndex] != "")
            StartCoroutine(NarrativeWindowProcess());
    }

    private IEnumerator NarrativeWindowProcess() {
        skip = false;
        readyToClose = false;
        targetPosition = shownTransform.position;
        yield return StartCoroutine(MovePanel());
        isOpen = true;
        string[] replicas = texts[_moveIndex].Split(";");
        foreach (string s in replicas) {
            mainText.text += $"{s}\n";
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
}