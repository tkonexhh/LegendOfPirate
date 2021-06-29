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
    internal class UserStorage : LCObject
    {
        internal UserStorage() : base("UserStorage") { }
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
        internal int Level
        {
            get
            {
                return (int)this["level"];
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
        //TODO 其他属性添加 比如 战斗力 经验值 等等
    }

    //TODO 其他对象 怪物 武器

    ///<summary>
    /// 海战关卡以及奖励配置
    ///</summary>
    internal class MarinLevelConfig : LCObject
    {
        ///<summary>
        /// 关卡id
        ///</summary>
        internal int Level
        {
            get => (int)this["Level"];
            set
            {
                this["Level"] = value;
            }
        }
        ///<summary>
        /// 资源产出
        ///</summary>
        internal string ResOutPut
        {
            get => this["ResOutput"] as string;
            set
            {
                this["ResOutput"] = value;
            }
        }
        ///<summary>
        /// 获胜奖励
        ///</summary>
        internal string Reward
        {
            get => this["Reward"] as string;
            set
            {
                this["Reward"] = value;
            }
        }
        ///<summary>
        /// 领取状态
        ///</summary>
        internal int GetState
        {
            get => (int)this["State"];
            set
            {
                this["State"] = value;
            }
        }
        internal MarinLevelConfig() : base("MarinLevelConfig") { }
    }
    ///<summary>
    /// 游戏数据存储 可拓展 每一个对象对应一个表 
    ///</summary>
    public class GameStorageMgr : TSingleton<GameStorageMgr>
    {
        ///<summary>
        /// 具体对象的子类化注册
        ///</summary>
        public void SetUp()
        {
            LCObject.RegisterSubclass("Hello", () => new Hello());
            LCObject.RegisterSubclass("World", () => new World());
            LCObject.RegisterSubclass("AccountStorage", () => new AccountStorage());
            LCObject.RegisterSubclass("UserStorage", () => new UserStorage());
            LCObject.RegisterSubclass("MarinLevelConfig", () => new MarinLevelConfig());
        }
    }
}