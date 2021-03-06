using System;
using Controllers;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;

namespace Managers
{
    public class AnalyticManager : MonoBehaviour
    {
        public static bool Gold = false;

        private void Awake()
        {
            Debug.Log("Awake");
        }

        // Start is called before the first frame update
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitFirebase()
        {
            Debug.Log("Start Init");
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var _dependencyStatus = task.Result;
                if (_dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseApp _app = FirebaseApp.DefaultInstance;
                    try
                    {
                        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero)
                            .ContinueWithOnMainThread((fetchTask) =>
                            {
                                if (fetchTask.IsFaulted)
                                {
                                    Debug.LogError(fetchTask.Exception);
                                }

                                try
                                {
                                    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
                                        .ActivateAsync() /*.SetDefaultsAsync(GetDefault())*/
                                        .ContinueWithOnMainThread(task1 =>
                                        {
                                            if (task1.IsCompleted)
                                            {
                                            }

                                            if (task1.IsFaulted)
                                            {
                                                Debug.LogError(task1.Exception);
                                            }

                                            Debug.Log(
                                                $"Firebase config last fetch time {Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info.FetchTime}.");
                                            if (PlayerDataController.playerStats == null)
                                            {
                                                
                                            }
                                                
                                        });
                                }
                                catch (Exception _e)
                                {
                                    Debug.LogError(_e.Message);
                                }
                            });
                    }
                    catch (Exception _e)
                    {
                        Debug.LogError("Remote Config exception " + _e.Message);
                    }
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + _dependencyStatus);
                    Application.Quit();
                }
            });

            Debug.Log("End Init");
        }

        public static bool GetBool(string configName)
        {
            try
            {
                return Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(configName).BooleanValue;
            }
            catch (Exception _e)
            {
                Debug.Log("Remote config error. " + _e.Message);
                return true;
            }
        }

        public static long GetLong(string configName)
        {
            try
            {
                return Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(configName).LongValue;
            }
            catch (Exception _e)
            {
                Debug.Log("Remote config error. " + _e.Message + ".." + _e.StackTrace);
                return 0;
            }
        }

        public static double GetDouble(string configName)
        {
            try
            {
                return Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(configName).DoubleValue;
            }
            catch (Exception _e)
            {
                Debug.Log("Remote config error. " + _e.Message + ".." + _e.StackTrace);
                return 0f;
            }
        }

        public static string GetString(string configName)
        {
            try
            {
                return Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(configName).StringValue;
            }
            catch (Exception _e)
            {
                Debug.Log("Remote config error. " + _e.Message + ".." + _e.StackTrace);
                return "";
            }
        }

        public static void StartChallenge()
        {
            FirebaseAnalytics.LogEvent("start_challenge");
        }

        public static void CompleteChallenge()
        {
            FirebaseAnalytics.LogEvent("complete_challenge");
        }

        public static void OpenDonateShop()
        {
            FirebaseAnalytics.LogEvent("open_donate_shop");
        }

        public static void OpenNewField()
        {
            FirebaseAnalytics.LogEvent("open_new_field");
        }

        public static void OpenNewBall()
        {
            FirebaseAnalytics.LogEvent("open_new_ball");
        }

        public static void OpenStatistic()
        {
            FirebaseAnalytics.LogEvent("statistic");
        }

        public static void ChangeTheme()
        {
            FirebaseAnalytics.LogEvent("change_theme");
        }
    }
}