using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;


namespace GUI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DisplayMessage : MonoBehaviour
    {
        #region Singleton

        public static DisplayMessage main;

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

        #region Variables.

        List<string> textList;
        TextAsset textFile;
        public TextMeshProUGUI powerBarText;

        #endregion

        private void Start()
        {
            powerBarText = GetComponent<TextMeshProUGUI>();
            //Setting text files path and reading it.
            ReadFromFile();
        }

        public void ShowPowerBarText(int id)
        {
            //This will be accessed from other classes so that we can display different messages every time.
            powerBarText.text = textList[id];
        }


        private void ReadFromFile()
        {
            //Reading the text file.
            textFile = Resources.Load<TextAsset>("Display Text");
            textList = textFile.text.Split('\n').ToList();
        }
    }
}