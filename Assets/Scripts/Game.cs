using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public ProgressionManager ProgressionManager;
    public AchievementManager AchievementManager;
    public ProfileManager ProfileManager;
    public GameplayCameraController GameplayCameraController;

    public static Game Get() {
        return GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }
}