using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using LeanCloud;
using LeanCloud.Storage;


namespace GameWish.Game
{
    public class LeaderboardScene : MonoBehaviour
    {
        private const string LEADERBOARD_NAME = "Boss_Daily";

        public GameObject contentObject;
        public Text rankingTextPrefab;

        public Text scoreText;

        async void Start()
        {
            LeanCloudMgr.S.OnInit();
            await RefreshLeaderboard();
        }

        public async void OnRefreshClicked()
        {
            await RefreshLeaderboard();
        }

        public async void OnPlayClicked()
        {
            int score = UnityEngine.Random.Range(0, 10000);
            scoreText.text = $"得分: {score}";

            try
            {
                //LCUser currentUser = await LCUser.GetCurrent();
                var user = new LCUser();
                user.Username = "playerB";
                user.Password = "passwordA";
                await user.SignUp();
                Dictionary<string, double> statistic = new Dictionary<string, double> {
                    { LEADERBOARD_NAME, score }
                };
                await LCLeaderboard.UpdateStatistics(user, statistic, overwrite: false);
            }
            catch (LCException e)
            {
                Debug.LogError(e);
            }
        }

        private async Task RefreshLeaderboard()
        {
            contentObject.transform.DetachChildren();
            try
            {
                LCLeaderboard leaderboard = LCLeaderboard.CreateWithoutData(LEADERBOARD_NAME);
                ReadOnlyCollection<LCRanking> rankings = await leaderboard.GetResults(limit: 10,
                    selectUserKeys: new string[] { "username" });
                for (int i = 0; i < rankings.Count; i++)
                {
                    LCRanking ranking = rankings[i];
                    // Hero hero = ranking.User["hero"] as Hero;
                    Text rankingText = Instantiate(rankingTextPrefab);
                    rankingText.text = $"第 {i + 1} 名 : {ranking.User.Username} --- {Convert.ToInt32(ranking.Value)} 分";
                    rankingText.transform.SetParent(contentObject.transform);
                }
            }
            catch (LCException e)
            {
                Debug.LogError(e);
            }
        }
    }

}