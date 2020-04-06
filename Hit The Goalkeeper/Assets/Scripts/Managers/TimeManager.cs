using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {

        #region Singleton

        public static TimeManager main; 

        private void Awake()
        {
            if (main != null && main != this)
            {
                Destroy(gameObject);
                return;
            }

            main = this;
        }

        #endregion
    
        public float slowdownFactor = .05f; //This will be the slowdown effect
        private const int SlowDownLength = 2; //This is the length of slowdown effect. We will increase game time with this value.
        public bool _timeFix; //This is the parameter for stopping slowdown.

        #region SlowDown Effect
        
        private void Update()
        {
            if (!_timeFix) return;
            Time.timeScale += (1 / SlowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
        }

        public void SlowMotion()
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
        
        #endregion
    }
}
