using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Managers
{
    public enum TransPoscontrol
    {
        NotTrans,
        Trans
    }

    public class LevelManager : MonoBehaviour
    {
        public static int HighLevel;
        public static int ThisScene;
        public static int ThisLevel;
        public static bool RestartControl;
        public static bool NextLevelControl;
        public GameObject[] ObjLevelManager;


        [Header("Gecis olup olmadığını seç!")] public TransPoscontrol TransPosControl;

        [Header("Geçiş yapılacak pozisyonları ata!")]
        public GameObject NextLevelTargetPos;

        public GameObject NewLevelTargetPos;

        [SerializeField] [Range(0, 50)] public float TransSpeed;
        static bool LevelControl;
        static int ThisObjLevelInt;
        static int NewObjLevelInt;
        public int newLevel;

        public int
            levelÖlçer; //PlayerPrefteki değer eğer total level sayılarımızdan büyük random bir şekilde seviyelerden biri seçilip o tekrardan açılıyor. Böylece hata olmuyor.

        private void Awake()
        {
            //PlayerPrefs.SetInt("highlevel", 0);
            //Debug.Log("NextLevelControl: " + NextLevelControl);
            //Debug.Log("NewObjLevelInt: " + NewObjLevelInt);
            levelÖlçer = PlayerPrefs.GetInt("highlevel");
            if (NextLevelControl == true && !RestartControl)
            {
                ObjLevelManager[NewObjLevelInt - 1].SetActive(false);
                ObjLevelManager[NewObjLevelInt].SetActive(true);
            }
            else if (NextLevelControl == false && !RestartControl)
            {
                if (levelÖlçer + 1 < ObjLevelManager.Length)
                    ObjLevelManager[PlayerPrefs.GetInt("highlevel")].SetActive(true);
                else if (levelÖlçer + 1 >= ObjLevelManager.Length)
                {
                    newLevel = Random.Range(0, ObjLevelManager.Length);
                    ObjLevelManager[newLevel].SetActive(true);
                }
            }
            else if (RestartControl)
            {
                ObjLevelManager[ThisLevel].SetActive(true);
            }

            ThisScene = SceneManager.GetActiveScene().buildIndex;
            //Debug.Log("highlevel=" + PlayerPrefs.GetInt("highlevel"));
            for (int i = 0; i < ObjLevelManager.Length; i++)
            {
                if (ObjLevelManager[i].activeInHierarchy) ThisLevel = i;
            }
        }

        private void LateUpdate()
        {
            if (LevelControl)
            {
                ObjLevelManager[NewObjLevelInt].SetActive(true);

                ObjLevelManager[ThisObjLevelInt].transform.position = Vector3.MoveTowards(
                    ObjLevelManager[ThisObjLevelInt].transform.position, NextLevelTargetPos.transform.position,
                    Time.deltaTime * TransSpeed);
                ObjLevelManager[NewObjLevelInt].transform.position = Vector3.MoveTowards(
                    ObjLevelManager[NewObjLevelInt].transform.position, NewLevelTargetPos.transform.position,
                    Time.deltaTime * TransSpeed);


                if (ObjLevelManager[NewObjLevelInt].transform.position == NewLevelTargetPos.transform.position)
                {
                    LevelControl = false;
                    ObjLevelManager[NewObjLevelInt].SetActive(false);
                }
            }
        }

        public static void NextScene()
        {
            ThisScene += 1;
            SceneManager.LoadScene(ThisScene);
        }

        public static void NextScene(int setLevel)
        {
            SceneManager.LoadScene(setLevel);
        }

        public void PreviousLevel()
        {
            var newLevel = NewObjLevelInt - 1;

            for (var i = 0; i < ObjLevelManager.Length; i++)
            {
                if (ObjLevelManager[i].activeInHierarchy)
                {
                    ThisObjLevelInt = i;
                    if (newLevel > 0)
                    {
                        NewObjLevelInt = i - 1;
                    }

                    if (TransPosControl.ToString() == "Trans")
                    {
                        LevelControl = true;
                    }
                    else
                    {
                        ObjLevelManager[ThisObjLevelInt].SetActive(false);
                        ObjLevelManager[NewObjLevelInt].SetActive(true);
                    }

                    if (i > PlayerPrefs.GetInt("highlevel") && i != ObjLevelManager.Length)
                    {
                        PlayerPrefs.SetInt("highlevel", i);
                    }


                    break;
                }
            }
        }


        public void RestartLevel()
        {
            for (int i = 0; i < ObjLevelManager.Length; i++)
            {
                if (ObjLevelManager[i].activeInHierarchy)
                {
                    RestartControl = true;
                    ThisLevel = i;
                    SceneManager.LoadScene(ThisScene);
                    break;
                }
            }
        }

        public void NextLevelErkenDoğur()
        {
            if (PlayerPrefs.GetInt("highlevel") != 0)
            {
                newLevel = PlayerPrefs.GetInt("highlevel") + 1;
            }
            else if (levelÖlçer + 1 < ObjLevelManager.Length)
            {
                newLevel = NewObjLevelInt + 1;
            }
            else if (levelÖlçer + 1 >= ObjLevelManager.Length)
            {
                newLevel = Random.Range(0, ObjLevelManager.Length);
                while (newLevel == ThisLevel) newLevel = Random.Range(0, ObjLevelManager.Length);
            }

            if (newLevel < ObjLevelManager.Length)
            {
                ObjLevelManager[newLevel].SetActive(true);
                ObjLevelManager[newLevel].transform.position = new Vector3(ObjLevelManager[newLevel].transform.position.x,
                    ObjLevelManager[newLevel].transform.position.y, 296.5f);
            }
            else if (newLevel + 1 >= ObjLevelManager.Length)
            {
                newLevel = Random.Range(0, ObjLevelManager.Length);
                while (newLevel == ThisLevel) newLevel = Random.Range(0, ObjLevelManager.Length);
                ObjLevelManager[newLevel].SetActive(true);
                ObjLevelManager[newLevel].transform.position = new Vector3(ObjLevelManager[newLevel].transform.position.x,
                    ObjLevelManager[newLevel].transform.position.y, 296.5f);
            }
        }

        public void NextObjLevel()
        {
            for (int i = 0; i < ObjLevelManager.Length; i++)
            {
                if (ObjLevelManager[i].activeInHierarchy)
                {
                    ThisObjLevelInt = i;
                    if (newLevel < ObjLevelManager.Length)
                    {
                        NewObjLevelInt = i + 1;
                    }
                    else if (newLevel + 1 >= ObjLevelManager.Length)
                    {
                        NewObjLevelInt = Random.Range(0, ObjLevelManager.Length);
                        while (newLevel == ThisLevel) NewObjLevelInt = Random.Range(0, ObjLevelManager.Length);
                    }

                    if (TransPosControl.ToString() == "Trans")
                    {
                        LevelControl = true;
                    }
                    else
                    {
                        ObjLevelManager[ThisObjLevelInt].SetActive(false);
                        ObjLevelManager[NewObjLevelInt].SetActive(true);
                    }

                    NextLevelControl = true;
                    //if (NewObjLevelInt > PlayerPrefs.GetInt("highlevel") && i!= ObjLevelManager.Length) { PlayerPrefs.SetInt("highlevel", PlayerPrefs.GetInt("highlevel")+1); }
                    PlayerPrefs.SetInt("highlevel", PlayerPrefs.GetInt("highlevel") + 1);
                    //Debug.Log("HighLevel: " + PlayerPrefs.GetInt("highlevel"));
                    SceneManager.LoadScene(ThisScene);

                    break;
                }
            }
        }

        [FormerlySerializedAs("Acıklama")] [SerializeField] [Multiline] private string acıklama;
    }
}