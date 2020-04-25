using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Main;


        [Header("Listeler"), SerializeField, NotNull]
        private List<GameObject> levellar = new List<GameObject>();

        [SerializeField] [NotNull] private List<Color> planeColors = new List<Color>();
        [SerializeField] [NotNull] private List<Color> lastPlaneColors = new List<Color>();
        [SerializeField] [NotNull] private List<Color> capsuleColors = new List<Color>();

        [SerializeField] [NotNull] private List<Color> seaColors = new List<Color>();

        //[SerializeField] private Dictionary<int, Color[,,,]> colors = new Dictionary<int, Color[,,,]>();
        [SerializeField] [NotNull] private List<Material> objectsThatNeededToChangeColors = new List<Material>();


        //Seviye için gereken attributelar...
        [NotNull] private static int currentLevel;
        [NotNull] public int thisLevel; //Bu global erişim için;
        [NotNull] private static int _nextLevel;
        [NotNull] private static int _currentScene;
        [NotNull] private static bool _restartLevelControl;
        [NotNull] private static bool _firstRestartState;
        [NotNull] private static bool _nextLevelControl;
        [NotNull] private static bool _randomizeLevelControl;
        [NotNull] private int _newlevel;

        //[SerializeField] private Animator levelLoadAnim;
        [SerializeField] private TextMeshProUGUI levelText;

        [NotNull]
        public Color currentColor; //Bunu dotween üzerinde renk değiştirdiğimiz için yazdık böylece erişebiliyoruz.

        public bool
            levelRestarted; //This is the condition for checking the levels restart state. (If it is we will not increase the health & damage of goalkeeper.)

        public bool
            nextLevelTrigger; //This is the condition for the checking the next level state. (If it is we will increase the health & damage of goalkeeper.)

        private static readonly int Color58E0201D = Shader.PropertyToID("Color_58E0201D");
        private static readonly int StartAnim = Animator.StringToHash("Start");

        #region Singleton

        private void Awake()
        {
            if (Main != null && Main != this)
            {
                Destroy(gameObject);
                return;
            }

            Main = this;

            #endregion


            if (_restartLevelControl)
            {
                levelRestarted = true;
                levellar[currentLevel].SetActive(true);
                _restartLevelControl = false;
            }

            else if (_nextLevelControl && !_randomizeLevelControl)
            {
                nextLevelTrigger = true;
                levellar[_nextLevel].SetActive(true);
                _nextLevelControl = false;
                //ChangeColors();
                currentLevel = _nextLevel;
                thisLevel = currentLevel;
            }
            else if (!_nextLevelControl)
            {
                if (PlayerPrefs.GetInt("highlevel") >= levellar.Count)
                {
                   if(_randomizeLevelControl) nextLevelTrigger = true;
                    var random = Random.Range(0, levellar.Count);
                    levellar[random].SetActive(true);
                    currentLevel = random;
                    thisLevel = currentLevel;
                    //ChangeColors();
                }
                else if (PlayerPrefs.GetInt("highlevel") < levellar.Count)
                {
                    if (_firstRestartState)
                    {
                        levellar[PlayerPrefs.GetInt("highlevel") - 1].SetActive(true);
                    }
                    else
                    {
                        levellar[PlayerPrefs.GetInt("highlevel")].SetActive(true);
                    }

                    currentLevel = PlayerPrefs.GetInt("highlevel");
                    thisLevel = currentLevel;
                    //ChangeColors();
                }
            }
            if (_nextLevelControl && _randomizeLevelControl)
            {
                nextLevelTrigger = true;
                var newLevel = Random.Range(0, levellar.Count);
                while (true)
                {
                    if (newLevel == currentLevel)
                    {
                        newLevel = Random.Range(0, levellar.Count);
                    }
                    else
                    {
                        break;
                    }
                }

                _nextLevelControl = false;
                levellar[newLevel].SetActive(true);
                currentLevel = _nextLevel;
                _randomizeLevelControl = false;
            }
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey("highlevel"))
            {
                levelText.text = "LEVEL " + 1;
                PlayerPrefs.SetInt("highlevel", 1);
                _firstRestartState = true;
            }
            else
            {
                levelText.text = "LEVEL " + PlayerPrefs.GetInt("highlevel");
            }
        }

        private static void RandomizeLevel()
        {
            _randomizeLevelControl = true;
            _restartLevelControl = false;
            PlayerPrefs.SetInt("highlevel", PlayerPrefs.GetInt("highlevel") + 1);
            SceneManager.LoadScene(_currentScene);
        }

        public void NextLevelEarlyBird()
        {
            if (currentLevel + 1 < levellar.Count)
            {
                _nextLevelControl = true;
                _nextLevel = currentLevel + 1;
                levellar[_nextLevel].transform.position = new Vector3(levellar[currentLevel].transform.position.x,
                    levellar[currentLevel].transform.position.y, levellar[currentLevel].transform.position.z + 198.02f);
                levellar[_nextLevel].SetActive(true);
            }
            else if (currentLevel + 1 >= levellar.Count)
            {
                while (true)
                {
                    if (_newlevel == currentLevel)
                    {
                        _newlevel = Random.Range(0, levellar.Count);
                    }
                    else
                    {
                        break;
                    }
                }

                levellar[_newlevel].transform.position = new Vector3(levellar[currentLevel].transform.position.x,
                    levellar[currentLevel].transform.position.y, levellar[currentLevel].transform.position.z + 198.02f);
                levellar[_newlevel].SetActive(true);
            }
        }

        public void NextLevel()
        {
            if (PlayerPrefs.GetInt("highlevel") + 1 < levellar.Count)
            {
                _nextLevelControl = true;
                _restartLevelControl = false;
                _nextLevel = currentLevel + 1;
                PlayerPrefs.SetInt("highlevel", _nextLevel+1);
                SceneManager.LoadScene(_currentScene);
            }
            else if (PlayerPrefs.GetInt("highlevel") + 1 >= levellar.Count)
            {
                RandomizeLevel();
            }
        }

        public void RestartLevel()
        {
            for (var i = 0; i < levellar.Count; i++)
            {
                if (!levellar[i].activeInHierarchy)
                {
                    continue;
                }

                _restartLevelControl = true;
                currentLevel = i;
                SceneManager.LoadScene(_currentScene);
                break;
            }
        }

        private void ChangeColors()
        {
            var randomSea = Random.Range(0, seaColors.Count);
            var randomMat = Random.Range(0, planeColors.Count);
            currentColor = planeColors[randomMat];
            objectsThatNeededToChangeColors[0].color = planeColors[randomMat];
            objectsThatNeededToChangeColors[1].color = lastPlaneColors[randomMat];
            objectsThatNeededToChangeColors[2].color = capsuleColors[randomSea];
            objectsThatNeededToChangeColors[3].SetColor(Color58E0201D, seaColors[randomSea]);
        }
    }
