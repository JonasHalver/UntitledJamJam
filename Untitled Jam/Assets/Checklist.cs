using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Checklist : MonoBehaviour
{
    public List<Image> items = new List<Image>();
    public GameObject list;
    public KeyCode button;

    public int animationFrames;
    public float t = 0;

    public float revealPos, hidePos;
    bool isUp;
    public static Checklist checklist;

    int itemsPickedUp;

    public GameObject win;

    // Start is called before the first frame update
    void Start()
    {
        checklist = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(button))
            StartCoroutine(Move(!isUp));
        var ap = list.GetComponent<RectTransform>().anchoredPosition;
        ap.y = Mathf.Lerp(hidePos, revealPos, t);
        list.GetComponent<RectTransform>().anchoredPosition = ap;

        if (itemsPickedUp == 6)
        {
            win.SetActive(true);
        }

    }
    
    IEnumerator Move(bool up)
    {
        for (int i = 0; i < animationFrames; i++)
        {
            if (up)
                t += 1 / (float)animationFrames;
            else
                t -= 1 / (float)animationFrames;
            yield return null;
        }
        isUp = !isUp;
    }

    public void Pickup(string name)
    {
        switch (name)
        {
            case "Pipe":
                items[0].enabled = true;
                itemsPickedUp++;
                break;
            case "Bell":
                items[1].enabled = true;
                itemsPickedUp++;
                break;
            case "Phone":
                items[2].enabled = true;
                itemsPickedUp++;
                break;
            case "Glasses":
                items[3].enabled = true;
                itemsPickedUp++;
                break;
            case "Hat":
                items[4].enabled = true;
                itemsPickedUp++;
                break;
            case "Cane":
                items[5].enabled = true;
                itemsPickedUp++;
                break;
        }
    }

    public void WinClicked()
    {
        SceneManager.LoadScene(0);
    }
}
