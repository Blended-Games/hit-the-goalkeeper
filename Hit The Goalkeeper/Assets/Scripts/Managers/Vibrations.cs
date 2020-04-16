using UnityEngine;
using MoreMountains.NiceVibrations;

public static class Vibrations
{
    public static void VibrationHeavy()
    {
        MMVibrationManager.Haptic(HapticTypes
            .HeavyImpact); //This will be the heavy vibrations for perfect conditions and such.
    }

    public static void VibrationMid()
    {
        MMVibrationManager.Haptic(HapticTypes
            .MediumImpact); //This will be the mid vibrations for a detailed feedbacks maybe good hits.   
    }

    public static void VibrationLight()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact); //This will be the light vibrations for simple feedbacks.
    }

    public static void VibrationSoft()
    {
        MMVibrationManager.Haptic(HapticTypes.SoftImpact); //This is the soft vibration for inputs maybe.
    }

    public static void VibrationSuccess()
    {
        MMVibrationManager.Haptic(HapticTypes.Success); //This is the success vibration for level successes.
    }

    public static void VibrationFail()
    {
        MMVibrationManager.Haptic(HapticTypes.Failure); //This is the fail vibration for level fail.
    }

    public static void VibrationWarning()
    {
        MMVibrationManager.Haptic(HapticTypes.Warning); //This is the warning vibration for level warnings.
    }

    public static void VibrationSelection()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection); //This is the selection vibration for menu selections.
    }
}