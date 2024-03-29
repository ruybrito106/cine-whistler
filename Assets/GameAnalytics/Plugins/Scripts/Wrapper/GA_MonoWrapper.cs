﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Utilities;

namespace GameAnalyticsSDK.Wrapper
{
    public partial class GA_Wrapper
    {
#if (UNITY_STANDALONE || UNITY_WP_8_1 || UNITY_SAMSUNGTV) && (!UNITY_EDITOR)

        private static void configureAvailableCustomDimensions01(string list)
        {
            IList<object> iList = GA_MiniJSON.Deserialize(list) as IList<object>;
            ArrayList array = new ArrayList();
            foreach(object entry in iList)
            {
                array.Add(entry);
            }
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureAvailableCustomDimensions01((string[])array.ToArray(typeof(string)));
        }

        private static void configureAvailableCustomDimensions02(string list)
        {
            IList<object> iList = GA_MiniJSON.Deserialize(list) as IList<object>;
            ArrayList array = new ArrayList();
            foreach(object entry in iList)
            {
                array.Add(entry);
            }
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureAvailableCustomDimensions02((string[])array.ToArray(typeof(string)));
        }

        private static void configureAvailableCustomDimensions03(string list)
        {
            IList<object> iList = GA_MiniJSON.Deserialize(list) as IList<object>;
            ArrayList array = new ArrayList();
            foreach(object entry in iList)
            {
                array.Add(entry);
            }
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureAvailableCustomDimensions03((string[])array.ToArray(typeof(string)));
        }

        private static void configureAvailableResourceCurrencies(string list)
        {
            IList<object> iList = GA_MiniJSON.Deserialize(list) as IList<object>;
            ArrayList array = new ArrayList();
            foreach(object entry in iList)
            {
                array.Add(entry);
            }
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureAvailableResourceCurrencies((string[])array.ToArray(typeof(string)));
        }

        private static void configureAvailableResourceItemTypes(string list)
        {
            IList<object> iList = GA_MiniJSON.Deserialize(list) as IList<object>;
            ArrayList array = new ArrayList();
            foreach(object entry in iList)
            {
                array.Add(entry);
            }
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureAvailableResourceItemTypes((string[])array.ToArray(typeof(string)));
        }

        private static void configureSdkGameEngineVersion(string unitySdkVersion)
        {
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureSdkGameEngineVersion(unitySdkVersion);
        }

        private static void configureGameEngineVersion(string unityEngineVersion)
        {
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureGameEngineVersion(unityEngineVersion);
        }

        private static void configureBuild(string build)
        {
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureBuild(build);
        }

        private static void configureUserId(string userId)
        {
            GameAnalyticsSDK.Net.GameAnalytics.ConfigureUserId(userId);
        }

        private static void initialize(string gamekey, string gamesecret)
        {
            GameAnalyticsSDK.Net.GameAnalytics.AddCommandCenterListener(unityCommandCenterListener);
            GameAnalyticsSDK.Net.GameAnalytics.Initialize(gamekey, gamesecret);
        }

        private static void setCustomDimension01(string customDimension)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetCustomDimension01(customDimension);
        }

        private static void setCustomDimension02(string customDimension)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetCustomDimension02(customDimension);
        }

        private static void setCustomDimension03(string customDimension)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetCustomDimension03(customDimension);
        }

        private static void addBusinessEvent(string currency, int amount, string itemType, string itemId, string cartType, string fields)
        {
            GameAnalyticsSDK.Net.GameAnalytics.AddBusinessEvent(currency, amount, itemType, itemId, cartType/*, GA_MiniJSON.Deserialize(fields) as IDictionary<string, object>*/);
        }

        private static void addResourceEvent(int flowType, string currency, float amount, string itemType, string itemId, string fields)
        {
            GameAnalyticsSDK.Net.GameAnalytics.AddResourceEvent((GameAnalyticsSDK.Net.EGAResourceFlowType)flowType, currency, amount, itemType, itemId/*, GA_MiniJSON.Deserialize(fields) as IDictionary<string, object>*/);
        }

        private static void addProgressionEvent(int progressionStatus, string progression01, string progression02, string progression03, string fields)
        {
            GameAnalyticsSDK.Net.GameAnalytics.AddProgressionEvent((GameAnalyticsSDK.Net.EGAProgressionStatus)progressionStatus, progression01, progression02, progression03/*, GA_MiniJSON.Deserialize(fields) as IDictionary<string, object>*/);
        }

        private static void addProgressionEventWithScore(int progressionStatus, string progression01, string progression02, string progression03, int score, string fields)
        {
            GameAnalyticsSDK.Net.GameAnalytics.AddProgressionEvent((GameAnalyticsSDK.Net.EGAProgressionStatus)progressionStatus, progression01, progression02, progression03, score/*, GA_MiniJSON.Deserialize(fields) as IDictionary<string, object>*/);
        }

        private static void addDesignEvent(string eventId, string fields)
        {
            GameAnalyticsSDK.Net.GameAnalytics.AddDesignEvent(eventId/*, GA_MiniJSON.Deserialize(fields) as IDictionary<string, object>*/);
        }

        private static void addDesignEventWithValue(string eventId, float value, string fields)
        {
            GameAnalyticsSDK.Net.GameAnalytics.AddDesignEvent(eventId, value/*, GA_MiniJSON.Deserialize(fields) as IDictionary<string, object>*/);
        }

        private static void addErrorEvent(int severity, string message, string fields)
        {
            GameAnalyticsSDK.Net.GameAnalytics.AddErrorEvent((GameAnalyticsSDK.Net.EGAErrorSeverity)severity, message/*, GA_MiniJSON.Deserialize(fields) as IDictionary<string, object>*/);
        }

        private static void setEnabledInfoLog(bool enabled)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetEnabledInfoLog(enabled);
        }

        private static void setEnabledVerboseLog(bool enabled)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetEnabledVerboseLog(enabled);
        }

        private static void setManualSessionHandling(bool enabled)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetEnabledManualSessionHandling(enabled);
        }

        private static void setEventSubmission(bool enabled)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetEnabledManualSessionHandling(enabled);
        }

        private static void gameAnalyticsStartSession()
        {
            GameAnalyticsSDK.Net.GameAnalytics.StartSession();
        }

        private static void gameAnalyticsEndSession()
        {
            GameAnalyticsSDK.Net.GameAnalytics.EndSession();
        }

        private static void setFacebookId(string facebookId)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetFacebookId(facebookId);
        }

        private static void setGender(string gender)
        {
            switch(gender)
            {
                case "male":
                    GameAnalyticsSDK.Net.GameAnalytics.SetGender(GameAnalyticsSDK.Net.EGAGender.Male);
                    break;
                case "female":
                    GameAnalyticsSDK.Net.GameAnalytics.SetGender(GameAnalyticsSDK.Net.EGAGender.Female);
                    break;
            }
        }

        private static void setBirthYear(int birthYear)
        {
            GameAnalyticsSDK.Net.GameAnalytics.SetBirthYear(birthYear);
        }

        private static string getCommandCenterValueAsString(string key, string defaultValue)
        {
            return GameAnalyticsSDK.Net.GameAnalytics.GetCommandCenterValueAsString(key, defaultValue);
        }

        private static bool isCommandCenterReady ()
        {
            return GameAnalyticsSDK.Net.GameAnalytics.IsCommandCenterReady();
        }

        private static string getConfigurationsContentAsString()
        {
            return GameAnalyticsSDK.Net.GameAnalytics.GetConfigurationsAsString();
        }
#endif
    }
}
