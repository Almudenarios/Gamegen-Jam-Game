using System.Collections;
using TMPro;
using GOM.Components.Core;
using GOM.Components.Player;
using GOM.Components.Honey;
using GOM.Classes.UI;
using GOM.Components.Flowers;
using GOM.Shared;
using UnityEngine.UI;
using UnityEngine;

namespace GOM.Components.UI {
    public class HUDManager : MonoBehaviour {
        [Header("Panels")]
        [SerializeField] GameObject PauseMenu;
        [SerializeField] GameObject EndGameScreen;
        [Space(2)]
        [Header("End Game Screen")]
        [SerializeField] TextMeshProUGUI EndText;
        [SerializeField] TextMeshProUGUI TimeText;
        [SerializeField] TextMeshProUGUI[] HoneyTexts;
        [SerializeField] TextMeshProUGUI CompletedHiveText;
        [SerializeField] Image BeeImage;
        [SerializeField] Sprite[] BeeSprites;
        [Space(10)] [Header("Game UI Elements")] 
        [SerializeField] private Sprite[] HoneyIcons;
        [SerializeField] private Image[] NextHoney;

        private GameManager _gameManager;
        private PlayerManager _player;
        private Timer _gameTimer;
        private Queue _nextHoneys = new Queue();
        private int _currentFlowerIndex;

        private void Start() {
            _gameManager = GameManager.Instance;
            _player = PlayerManager.Instance;
            _gameTimer = new Timer(0, 0);
            HoneyGenerator.OnFlowerGeneration += ShowNextFlower;
        }

        private void Update() {
            PauseMenu.SetActive(_gameManager.GamePaused());

            if (_gameManager.GamePaused()) return;

            if (_gameManager.GameLost()) {
                EndText.text = "Jornada\nCompletada";
                BeeImage.sprite = BeeSprites[0];
                HandleEndGame();
                return;
            } else if (_gameManager.GameWon()) {
                EndText.text = "Te han expulsado\ndel panal";
                BeeImage.sprite = BeeSprites[1];
                HandleEndGame();
                return;
            }

            _gameTimer.UpdateTimer(true);
        }

        private void HandleEndGame() {
            EndGameScreen.SetActive(true);

            TimeText.text = $"{_gameTimer.GetMinuteCount():00}:{_gameTimer.GetSecondCount():00}";
            for (int i = 0; i < HoneyTexts.Length; i++) {
                HoneyTexts[i].text = _player.GetHoneyCount(i).ToString();
            }
            CompletedHiveText.text = _player.GetCompletedHives().ToString();
        }

        private void ShowNextFlower(FlowerComponent flower) {
            
        }
    }
}
