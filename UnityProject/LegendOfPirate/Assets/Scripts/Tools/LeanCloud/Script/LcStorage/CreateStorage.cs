using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UnityEngine;
using LeanCloud.Storage;
using TMPro;


namespace GameWish.Game
{
    public class CreateStorage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ShowText;
        private string m_ObjectId;

        #region 创建对象并保存到后端

        ///<summary>
        /// 创建玩家账号对象并保存到后端
        ///</summary>
        public async void CreateAccount()
        {
            AccountStorage account = new AccountStorage();
            account.Account = "ChuckFly";
            await account.Save();
        }

        ///<summary>
        /// 创建英雄对象并保存到后端
        ///</summary>
        public async void CreateHero()
        {
            // LCQuery<HeroStorage> query = new LCQuery<HeroStorage>("HeroStorage");
            // query.WhereExists("name");
            HeroStorage heroStorage = new HeroStorage()
            {
                Name = "chuck",
                Level = "20",
                Balance = 1024
            };

            // 使用LCUser登录才能获取
            // LCUser currentUser = await LCUser.GetCurrent();
            // currentUser["hero"] = hero;
            await heroStorage.Save();
            m_ObjectId = heroStorage.ObjectId;
            //TODO 每一个对象的ObjectID都需要存档 以便于查询更新使用
        }

        ///<summary>
        /// HelloWorld
        ///</summary>
        public async void CreateHelloWorld()
        {
            World world = new World()
            {
                Content = "Hello World!"
            };
            Hello hello = new Hello()
            {
                World = world
            };
            await hello.Save();
        }

        #endregion

        #region 查询对象 返回数据 
        ///<summary>
        /// 获取后端的英雄对象
        ///</summary>
        public async void QueryHero()
        {
            LCQuery<HeroStorage> query = new LCQuery<HeroStorage>("HeroStorage");
            // query.WhereEndsWith("name", "chuck");
            HeroStorage hero = await query.Get(m_ObjectId);
            m_ShowText.text = hero.Name + "_" + hero.Level + "_" + hero.Balance.ToString();
        }
        #endregion


        #region 要更新一个对象，只需指定需要更新的属性名和属性值，然后调用 Save 方法

        ///<summary>
        /// 获取后端的英雄对象 根据 m_ObjectId 获取对应的对象 从而进行更新数据
        ///</summary>
        public async void UpdateHero()
        {
            HeroStorage hero = LCObject.CreateWithoutData("HeroStorage", m_ObjectId) as HeroStorage;
            hero.Name = "Chuck2";
            hero.Level = "40";
            await hero.Save();
        }
        #endregion


        #region 删除指定对象

        ///<summary>
        /// 获取后端的英雄对象 根据 m_ObjectId 获取对应的对象 从而进行更新删除
        ///</summary>
        public async void DeleteHero()
        {
            LCObject hero = LCObject.CreateWithoutData("HeroStorage", m_ObjectId);
            await hero.Delete();
        }
        #endregion
    }

}