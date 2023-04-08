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
    private float targetY = 0f;
    private float moveTime = 0.5f;
    public bool isOpen = false;
    private bool skip = false;
    private bool readyToClose = false;
    private int _moveIndex;

    public string[] texts;

    public void RunPreMoveText(int moveIndex) {
        _moveIndex = moveIndex;
        StartCoroutine(NarrativeWindowProcess());
    }

    private IEnumerator NarrativeWindowProcess() {
        isOpen = true;
        skip = false;
        readyToClose = false;
        targetY = shownY;
        yield return StartCoroutine(MovePanel());
        string[] replicas = texts[_moveIndex].Split("\n");
        foreach (string s in replicas) {
            mainText.text += $"{s}\n";
            if (!skip)
                yield return new WaitForSeconds(0.5f);
        }
        readyToClose = true;
    }

    private void Update()
    {
        if (Mouse.current.IsPressed())
            if (readyToClose)
            {
                targetY = hiddenY;
                StartCoroutine(MovePanel());
            }
            else {
                skip = true;
            }
    }

    private IEnumerator MovePanel() {
        float timer = 0f;
        while (timer < moveTime) {
            panelTransform.position = Vector2.Lerp(panelTransform.position, new Vector2(panelTransform.position.x, targetY), timer / moveTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void ClosePanel() {
        targetY = hiddenY;
        MovePanel();
    }
}