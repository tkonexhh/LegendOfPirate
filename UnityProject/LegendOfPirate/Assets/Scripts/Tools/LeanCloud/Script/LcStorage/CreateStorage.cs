using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UnityEngine;
using LeanCloud.Storage;
using LeanCloud.LiveQuery;
using TMPro;

namespace GameWish.Game
{
    public class CreateStorage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ShowText;
        private string m_ObjectId;
        private LCLiveQuery liveQuery;

        #region 创建对象并保存到后端 子类化创建 便于管理与操作

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
        /// 创建英雄对象并保存到后端 非子类化
        ///</summary>
        public async void CreateHero()
        {
            // 构建对象
            LCObject heroStorage = new LCObject("UserStorage");
            // 为属性赋值
            heroStorage["name"] = "chuck";
            heroStorage["level"] = "20";
            heroStorage["balance"] = 1024;
            // 将对象保存到云端
            await heroStorage.Save();
            m_ObjectId = heroStorage.ObjectId;
            //TODO 每一个对象的ObjectID都需要存档 以便于查询更新使用
        }
        ///<summary>
        /// 创建英雄对象并保存到后端 子类化创建
        ///</summary>
        public async void CreateSubHero()
        {
            // LCQuery<UserStorage> query = new LCQuery<UserStorage>("UserStorage");
            // query.WhereExists("name");
            UserStorage heroStorage = new UserStorage()
            {
                Name = "chuck",
                Level = 20,
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

        #region 查询对象 返回数据 对于已经保存到云端的 LCObject，可以通过它的 objectId 将其取回 可以获取全部数据
        ///<summary>
        /// 获取后端的英雄对象
        ///</summary>
        public async void QueryHero()
        {
            LCQuery<UserStorage> query = new LCQuery<UserStorage>("UserStorage");
            // query.WhereEndsWith("name", "chuck");
            UserStorage hero = await query.Get(m_ObjectId);
            if (hero.Name != null)
                m_ShowText.text = hero.Name + "_" + hero.Level + "_" + hero.Balance.ToString();
            else
                m_ShowText.text = "查无此人";

            // LiveQueryTest();
        }

        ///<summary>
        /// 获取关卡等级奖励
        ///</summary>
        public async void QueryLevelAward()
        {
            LCQuery<AwardStorage> query = new LCQuery<AwardStorage>("AwardStorage");
            query.Limit(40);
            // query.WhereEndsWith("name", "chuck");
            ReadOnlyCollection<AwardStorage> list = await query.Find();
            foreach (AwardStorage item in list)
            {
                m_ShowText.text += item.Level + "_" + item.Award + "\n";
            }
        }
        public async void LiveQueryTest()
        {
            LCQuery<UserStorage> query = new LCQuery<UserStorage>("UserStorage");
            query.WhereEndsWith("name", "chuck");
            liveQuery = await query.Subscribe();
            liveQuery.OnCreate = (obj) =>
            {
                Debug.Log($"create: {obj}");
            };
            liveQuery.OnUpdate = (obj, keys) =>
            {
                Debug.Log($"update: {obj}");
                Debug.Log(keys.Count);
            };
            liveQuery.OnDelete = (objId) =>
            {
                Debug.Log($"delete: {objId}");
            };
            liveQuery.OnEnter = (obj, keys) =>
            {
                Debug.Log($"enter: {obj}");
                Debug.Log(keys.Count);
            };
            liveQuery.OnLeave = (obj, keys) =>
            {
                Debug.Log($"leave: {obj}");
                Debug.Log(keys.Count);
            };
        }
        #endregion

        #region 要更新一个对象，只需指定需要更新的属性名和属性值，然后调用 Save 方法 需要 唯一标识 ObjectId

        ///<summary>
        /// 获取后端的英雄对象 根据 m_ObjectId 获取对应的对象 从而进行更新数据
        ///</summary>
        public async void UpdateHero()
        {
            UserStorage hero = LCObject.CreateWithoutData("UserStorage", m_ObjectId) as UserStorage;
            hero.Name = "Chuck2";
            hero.Level = 40;
            await hero.Save();
        }
        #endregion

        #region 删除指定对象 需要 唯一标识 ObjectId

        ///<summary>
        /// 获取后端的英雄对象 根据 m_ObjectId 获取对应的对象 从而进行更新删除
        ///</summary>
        public async void DeleteHero()
        {
            LCObject hero = LCObject.CreateWithoutData("UserStorage", m_ObjectId);
            await hero.Delete();
        }
        #endregion
    }

}