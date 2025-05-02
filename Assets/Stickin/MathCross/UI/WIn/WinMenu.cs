using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace stickin.menus.type1
{
    public class WinMenu : BaseMenu
    {
        [SerializeField] private Text _difficultText;
        [SerializeField] private TextTimer _timerText;
        [SerializeField] private TextWithIcon _coinsText;
        [SerializeField] private TextWithIcon _collectXText;
        [SerializeField] private Button _claimX3Btn;
        [SerializeField] private Button _claimBtn;

        [SerializeField] private int _claimX = 5;

        [InjectField] private ResourcesService _resourcesService;
        [InjectField] private AdsService _adsService;
        [InjectField] private LocalizationService _localizationService;

        private RewardResourceModule _rewardResource;

        public override void SetData(Hashtable data = null)
        {
            base.SetData(data);

            if (data != null && data.ContainsKey("game"))
            {
                var game = (Game) data["game"];
                
                _timerText.Init(game.GetGameModule<GameTimer>());
                _difficultText.text = string.Empty;// game.Difficult.ToString();

                _rewardResource = game.GetGameModule<RewardResourceModule>();
                if (_rewardResource != null)
                    _coinsText.SetText(_rewardResource.Value.ToString());
            }
        }

        private void Start()
        {
            InjectService.BindFields(this);
            
            _claimX3Btn.onClick.AddListener(OnClickClaimX3);
            _claimBtn.onClick.AddListener(OnClickClaim);

            var localizeText = _localizationService.GetStrById("strCollectX");
            localizeText = string.Format(localizeText, _claimX).ToUpperFirst();
            _collectXText.SetText(localizeText);
        }

        private void OnClickClaim()
        {
            SceneLoader.LoadScene(2);
        }
        
        private void OnClickClaimX3()
        {
            if (_adsService.IsRewardAvailable())
                _adsService.ShowReward(OnRewardComplete);
            else
                TextMessageMenu.ShowWithText(TextMessageMenu.AdsNotReady);
        }

        private void OnRewardComplete()
        {
            _resourcesService.ChangeResource(_rewardResource.Id, _rewardResource.Value * (_claimX - 1));

            var prize = new ResourceData();
            prize.Id = ResourcesService.CoinKey;
            prize.Value = _rewardResource.Value * _claimX;
            
            var data = new Hashtable {["resource"] = prize, ["callbackEnd"] = (Action)OnShowPrizeEnd};
            MenusService.Show<RewardResourceMenu>(data);
        }

        private void OnShowPrizeEnd()
        {
            SceneLoader.LoadScene(2);
        }
    }
}