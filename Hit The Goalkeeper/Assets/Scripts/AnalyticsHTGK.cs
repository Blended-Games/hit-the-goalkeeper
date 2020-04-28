using System.Collections.Generic;
using FlurrySDK;
using GameAnalyticsSDK;
using UnityEngine;

public static class AnalyticsHTGK
{
    public static void AnalyticsLevelSuccess(string level, string index)
    {
        var dictionary = new Dictionary<string, string>();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, string.Format("Level{0}Index{1}",
            level, index));

        dictionary.Add("Level", level);
        dictionary.Add("Index", index);

        Flurry.LogEvent("LevelSuccess", dictionary);
        Flurry.LogEvent("LevelSuccessTimed", dictionary, true);
        Debug.Log(Flurry.LogEvent("LevelSuccess", dictionary).ToString());
        Debug.Log(Flurry.LogEvent("LevelSuccessTimed", dictionary, true).ToString());
    }

    public static void AnalyticsLevelFail(string level, string index)
    {
        var dictionary = new Dictionary<string, string>();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, string.Format("Level{0}Index{1}",
            level, index));

        dictionary.Add("Level", level);
        dictionary.Add("Index", index);
        Flurry.LogEvent("LevelFailed", dictionary);
        Flurry.LogEvent("LevelFailedTimed", dictionary, true);
        Debug.Log(Flurry.LogEvent("LevelFailed", dictionary).ToString());
        Debug.Log(Flurry.LogEvent("LevelFailedTimed", dictionary, true).ToString());
    }

    public static void AnalyticsLevelStart(string level, string index)
    {
        var dictionary = new Dictionary<string, string>();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, string.Format("Level{0}Index{1}",
            level, index));

        dictionary.Add("Level", level);
        dictionary.Add("Index", index);

        Flurry.LogEvent("GameStartTimed", dictionary, true);
        Flurry.LogEvent("GameStart", dictionary);
        Debug.Log(Flurry.LogEvent("GameStart", dictionary).ToString());
        Debug.Log(Flurry.LogEvent("GameStartTimed", dictionary, true).ToString());
    }
}