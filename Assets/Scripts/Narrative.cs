using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Narrative : MonoBehaviour
{
    public RectTransform panelTransform;
    public TextMeshProUGUI mainText;
    public float hiddenY = 1000f;
    public float shownY = 0f;
    public RectTransform shownTransform;
    private RectTransform targetTransform;
    private float moveTime = 1f;
    public bool isOpen = false;
    private bool skip = false;
    private bool readyToClose = false;
    private int _moveIndex;

    public string[] texts;

    public void RunPreMoveText(int moveIndex) {
        _moveIndex = moveIndex;
        if (texts.Length>_moveIndex && texts[_moveIndex] != "")
            StartCoroutine(NarrativeWindowProcess());
    }

    private IEnumerator NarrativeWindowProcess() {
        skip = false;
        readyToClose = false;
        targetTransform = shownTransform;        
        yield return StartCoroutine(MovePanel());
        isOpen = true;
        string[] replicas = texts[_moveIndex].Split(";");
        foreach (string s in replicas)
            Debug.Log(s);
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
                    targetTransform.Translate(Vector3.up*800);
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
            panelTransform.position = Vector3.Lerp(savedPos, targetTransform.position, timer / moveTime);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}