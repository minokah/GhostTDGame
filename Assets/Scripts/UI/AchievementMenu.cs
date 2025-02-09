using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchievementMenu : MonoBehaviour
{
    Game Game;
    CanvasFade canvas;
    public GameObject entryTemplate;
    public RectTransform canvasRect;
    public GameObject contentContainer;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        canvas = GetComponent<CanvasFade>();
    }

    public void Refresh()
    {
        // clear list
        foreach (Transform obj in contentContainer.transform)
        {
            if (obj.name != "Entry Template") Destroy(obj.gameObject);
        }

        int gridX = 0;
        int gridY = 0;
        Dictionary<string, AchievementEntry> entries = Game.AchievementManager.achievementEntries;

        foreach (AchievementEntry entry in entries.Values)
        {
            GameObject tempClone = Instantiate(entryTemplate);
            tempClone.name = entry.id;
            tempClone.transform.SetParent(contentContainer.transform);

            // set entry data
            AchievementMenuEntry data = tempClone.GetComponent<AchievementMenuEntry>();
            data.name.text = entry.name;
            data.description.text = entry.description;
            data.progress.text = "Progress: 0/0";
            data.icon.rectTransform.anchoredPosition = new Vector2(entry.icon.Item1 * -80, entry.icon.Item2 * -80);
            
            // display first two rewards (as the preview only supports 2 right now)
            // hide rewards unavailable
            if (entry.rewards.Count < 2) data.reward2.gameObject.SetActive(false);
            if (entry.rewards.Count < 1) data.reward1.gameObject.SetActive(false);
            
            // now show rewards
            // ...

            // set coordinates
            tempClone.GetComponent<RectTransform>().GetComponent<RectTransform>().localPosition = new Vector2(gridX * 750, gridY * -210);
            tempClone.GetComponent<RectTransform>().GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            gridX++;
            if (gridX == 2)
            {
                gridX = 0;
                gridY++;
            }
            
            tempClone.SetActive(true);
        }
    }

    public void Show()
    {
        canvas.visible = true;
        Refresh();
    }

    public void Hide()
    {
        canvas.visible = false;
    }
}
