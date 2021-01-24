using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public Level_Loader_Sc loader;

    public void Select (int LevelNum)
    {
        loader.LoadNextLevel(LevelNum);
        print(LevelNum);
    }
}
