using LeanCloud.Storage;
using LeanCloud;
using UnityEngine;
using Qarth;
namespace GameWish.Game
{
    internal class Hello : LCObject
    {
        internal World World
        {
            get => this["world"] as World;
            set
            {
                this["world"] = value;
            }
        }

        internal Hello() : base("Hello") { }
    }

    internal class World : LCObject
    {
        internal string Content
        {
            get => this["content"] as string;
            set
            {
                this["content"] = value;
            }
        }

        internal World() : base("World") { }
    }

    ///<summary>
    /// 玩家账号信息
    ///</summary>
    internal class AccountStorage : LCObject
    {
        ///<summary>
        /// 玩家本人
        ///</summary>
        internal string Account
        {
            get => this["account"] as string;
            set
            {
                this["account"] = value;
            }
        }

        internal AccountStorage() : base("AccountStorage") { }
    }

    ///<summary>
    /// 玩家个人信息
    ///</summary>
    internal class HeroStorage : LCObject
    {
        internal HeroStorage() : base("HeroStorage") { }
        ///<summary>
        /// 玩家姓名
        ///</summary>
        internal string Name
        {
            get
            {
                return this["name"] as string;
            }
            set
            {
                this["name"] = value;
            }
        }
        ///<summary>
        /// 玩家等级
        ///</summary>
        internal string Level
        {
            get
            {
                return this["level"] as string;
            }
            set
            {
                this["level"] = value;
            }
        }
        ///<summary>
        /// 玩家货币
        ///</summary>
        internal int Balance
        {
            get => (int)this["balance"];
            set
            {
                this["balance"] = value;
            }
        }
        //TODO 其他属性
    }

    ///<summary>
    /// 游戏数据存储 可拓展 每一个对象对应一个表 
    ///</summary>
    public class GameStorage : TSingleton<GameStorage>
    {
        private void Awake()
        {
            SetUp();
        }
        ///<summary>
        /// 具体对象的子类化注册
        ///</summary>
        public void SetUp()
        {
            LCObject.RegisterSubclass("Hello", () => new Hello());
            LCObject.RegisterSubclass("World", () => new World());
            LCObject.RegisterSubclass("AccountStorage", () => new AccountStorage());
            LCObject.RegisterSubclass("HeroStorage", () => new HeroStorage());
        }
    }
}