using Controllers;
using Shop;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        private void Awake()
        {
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        /*  private void OnApplicationFocus(bool focus)
          {
              OnApplicationPause(!focus);
          }
  
          private void OnApplicationPause(bool pause)
          {
              if (pause)
              {
                  SaveGame();
              }
              else
              {
                  LoadGame();
              }
          }*/


        private static void SaveGame()
        {
            PlayerPrefs.SetString("DefaultBuff", JsonUtility.ToJson(DefaultBuff.grade));
            PlayerPrefs.SetString("StoppersBuff", JsonUtility.ToJson(BuyStopper.grades));
            PlayerPrefs.SetString("Statistic", JsonUtility.ToJson(Statistics.stats));
            PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(PlayerDataController.playerStats));
            PlayerPrefs.SetString("Settings", JsonUtility.ToJson(Setting.settings));
            PlayerPrefs.SetString("Fields", JsonUtility.ToJson(FieldManager.fields));
            PlayerPrefs.SetString("UpgradeCircle", JsonUtility.ToJson(UnlockCircles.upgrade));
            PlayerPrefs.SetString("Challenge", JsonUtility.ToJson(ChallengeManager.progress));
        }


        private static void LoadGame()
        {
            Debug.Log("Loading player data.");
            if (PlayerPrefs.HasKey("x2reward"))
                PlayerPrefs.DeleteAll();
            DefaultBuff.grade =
                JsonUtility.FromJson<CostAndGrade>(PlayerPrefs.GetString("DefaultBuff",
                    JsonUtility.ToJson(new CostAndGrade())));
            BuyStopper.grades =
                JsonUtility.FromJson<StopperGrades>(PlayerPrefs.GetString("StoppersBuff",
                    JsonUtility.ToJson(new StopperGrades(9))));
            Statistics.stats =
                JsonUtility.FromJson<Stats>(PlayerPrefs.GetString("Statistic", JsonUtility.ToJson(new Stats())));
            PlayerDataController.playerStats =
                JsonUtility.FromJson<PlayerStats>(PlayerPrefs.GetString("PlayerStats",
                    JsonUtility.ToJson(new PlayerStats())));
            Setting.settings =
                JsonUtility.FromJson<MyPlayerSettings>(PlayerPrefs.GetString("Settings",
                    JsonUtility.ToJson(new MyPlayerSettings())));
            FieldManager.fields =
                JsonUtility.FromJson<Fields>(PlayerPrefs.GetString("Fields", JsonUtility.ToJson(new Fields())));
            UnlockCircles.upgrade = JsonUtility.FromJson<UpgradeCircle>(
                PlayerPrefs.GetString("UpgradeCircle", JsonUtility.ToJson(new UpgradeCircle())));
            ChallengeManager.progress = JsonUtility.FromJson<ChallengeProgress>(
                PlayerPrefs.GetString("Challenge", JsonUtility.ToJson(new ChallengeProgress())));
            Debug.Log("Player data complete loading.");
        }
    }
}