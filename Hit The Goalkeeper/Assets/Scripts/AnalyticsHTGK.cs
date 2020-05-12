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

        Flurry.LogEvent("LevelSuccessTimed", dictionary, true);
        Flurry.EventRecordStatus status = Flurry.LogEvent("LevelSuccessTimed", dictionary, true);
        Debug.Log(status);
        Flurry.EndTimedEvent("LevelSuccess Timed" , dictionary);

    }

    public static void AnalyticsLevelFail(string level, string index)
    {
        var dictionary = new Dictionary<string, string>();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, string.Format("Level{0}Index{1}",
            level, index));

        dictionary.Add("Level", level);
        dictionary.Add("Index", index);
        Flurry.LogEvent("LevelFailedTimed", dictionary, true);
        Flurry.EventRecordStatus status = Flurry.LogEvent("LevelFailedTimed", dictionary, true);
        Debug.Log(status);
        Flurry.EndTimedEvent("LevelFailed Timed" , dictionary);
    }

    public static void AnalyticsLevelStart(string level, string index)
    {
        var dictionary = new Dictionary<string, string>();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, string.Format("Level{0}Index{1}",
            level, index));

        dictionary.Add("Level", level);
        dictionary.Add("Index", index);

        Flurry.LogEvent("GameStartTimed", dictionary, true);
        Flurry.EventRecordStatus status = Flurry.LogEvent("GameStartTimed", dictionary, true);
        Debug.Log(status);
        Flurry.EndTimedEvent("GameStart Timed" , dictionary);
    }
}