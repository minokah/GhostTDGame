using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMenuUI : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasFade canvas;
    public GameObject entryTemplate;
    public RectTransform canvasRect;
    public GameObject contentContainer;
    public Button closeButton;
    public ProfileSectionUI profile;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasFade>();
        closeButton.onClick.AddListener(Hide);
    }

    public void Refresh()
    {
        // Clear the list first (implement filters later)
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
            data.icon.transform.Find("Icon").GetComponent<RectTransform>().anchoredPosition = new Vector2(entry.icon.Item1 * -80, entry.icon.Item2 * -80);

            // For all conditions, concatenate them and log progress
            string progress = "";
            if (entry.conditions.Count > 0)
            {
                foreach (var con in entry.conditions)
                {
                    string text = con.title;
                    if (text == null) text = "Completed";

                    progress += $"{text}: {Game.AchievementManager.GetProgress(con)}/{con.value}\n";
                }
                data.progress.text = progress;
            }
            // No conditions? Only list "Progress"
            else
            {
                // If the player already has the achievement, just set it to 1
                if (Game.ProfileManager.activeProfile.unlocks["achievements"].Contains(entry.id)) data.progress.text = "Completed: 1/1";
                else data.progress.text = "Completed: 0/1";
            }
            
            // now show rewards
            // ...

            // set coordinates
            tempClone.GetComponent<RectTransform>().localPosition = new Vector2(gridX * 655, gridY * -215);
            tempClone.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

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
        UI.windowActive = true;
        canvas.Show();
        Refresh();
    }

    public void Hide()
    {
        UI.windowActive = false;
        canvas.Hide();
    }
}
