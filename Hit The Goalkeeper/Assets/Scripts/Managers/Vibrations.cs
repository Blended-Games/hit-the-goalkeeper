using UnityEngine;

public static class Vibrations
{
    public static void VibrationHeavy()
    {
         iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy); //This will be the heavy vibrations for perfect conditions and such.
    }

    public static void VibrationMid()
    {
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);//This will be the mid vibrations for a detailed feedbacks maybe good hits.   
    }

    public static void VibrationLight()
    {
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight); //This will be the light vibrations for simple feedbacks.
    }
    
    public static void VibrationSuccess()
    {
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);//This is the success vibration for level successes.
    }

    public static void VibrationFail()
    {
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Failure); //This is the fail vibration for level fail.
    }

    public static void VibrationWarning()
    { 
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Warning); //This is the warning vibration for level warnings.
    }

    public static void VibrationSelection()
    {
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.SelectionChange); //This is the selection vibration for menu selections.
    }
}