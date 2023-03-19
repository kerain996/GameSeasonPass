/* 
 * 游戏通行证追踪
 * 包含：联网获取信息，本地文件存储信息，本地处理时间信息，信息展示
 * 
 * Date:
 * Version:
 * Author:
 * 
 */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Net;
using System.Text;

using PassStalker.Game;
using System.Text.RegularExpressions;

namespace SeasonPassStalker
{

    // 平台类/结构体
    struct Platform
    {
        private static int id;
        private static string? hardwarePlatform; // PC, PS, Xbox, NS,
        private static string? softwarePlatform; // Steam,Epic,UbiSoft,Origin,EA desktop,GOG...PS4,PS5,XBox One, XBox Series X, NS

        public void Set(int i, string HP, string SP)
        {
            id = i;
            hardwarePlatform = HP;
            softwarePlatform = SP;
        }

        public string Get() { return hardwarePlatform + "/" + softwarePlatform;}
    }

    // 创建游戏基本信息结构体
    // 结构体无法构建是创立初值
    struct Game
    {
        private static int id;
        private static string? name;
        private static string? description;
        private static string? version;
        private static string? developer;
        private static string? platform;

        public void setValues(int i, string n, string o, string v, string d, string p)
        {
            id = i;
            name = n;
            description = o;
            version = v;
            developer = d;
            platform = p;
        }

        public int GetId(){ return id; }
        public string GetName() { return name; }
        public string GetVersion() { return version; }
        public string GetDeveloper() { return developer;}
        public string GetPlatform() { return platform; }
        public static string[] GetValues()
        {
            string[] values = new string[] { id.ToString(), name, description, version, developer, platform}; // int转换为string的其他方法，不知道为啥报错：(string)i, i as string
            return values;
        }
    }


    // 通行证类
    // 通行证，联网信息获取，信息文件保存
    class SeasonPass
    {
        public Game game;
        public SeasonPass(Game gameInfo)
        {
            this.game = gameInfo;                       // 类实例时，首先确定游戏类别
        }

        // 变量带初值
        public string seasonName = "";
        public int seasonNumber = 0;
        public int maxLevel;
        // 确定游戏开始与结束时间，使用DateTime类
        public DateTime startTime = DateTime.Now;
        public DateTime endDate =DateTime.Now;

        public int daysRemaing = 3;
        public int hrsRemaing = 12;

        // 确定网络获取变量
        private int currentLevel = 0;
        private static string api = "";
        private static string apiBackup = "";

        // 私有变量获取
        public int GetLevel() => currentLevel;

        // 确定联网获取网址
        public void SetUrl(string url) {api = url;}

        public void SetUrlBackup(string url) {apiBackup = url;}

        // 联网获取游戏信息
        // 不同游戏联网获取信息规则不同，联网用户登录？
        public void GetPassInfo()
        {
            // 基本信息获取，webclient方法
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            try
            {
                string html = wc.DownloadString(api);
            }
            catch(Exception)
            {
                string html = wc.DownloadString(apiBackup);
            }

            // 信息提取规则

            // 用户信息获取

        }

        public static async Task<string[]>  GetPassInfo(string url)
        {
            string[] info = new string[4];                 // 关键信息返回，赛季名称，赛季数，赛季剩余时间等
            HttpClient client = new HttpClient();
            // client.BaseAddress = new Uri(url);
            var result = await client.GetAsync(url);
            if (!result.IsSuccessStatusCode) { Console.WriteLine("连接错误！"); }
            else 
            { 
                Console.WriteLine(result.StatusCode.ToString());
                string html = await client.GetStringAsync(url);
                // info[0] = Regex.Match(html, ).Groups[1].Value;
            }
            return info;
        }

        // 通行证剩余时间获取或计算
        public void setDaysRemaing(int days)
        { daysRemaing = days; }
        public void setDaysRemaing(string startDate, string endDate)
        {

        }
        public void setDaysRemaing()
        {
            int years = Convert.ToInt32(endDate.Split('/')[0]) - int.Parse(startDate.Split('/')[0]);
        }

        // 关键信息文件保存，每游戏1文件
        public void infoSave()
        {
            string filename = game.GetName() + " SeasonPass" + seasonNumber.ToString() + ".txt";
            FileStream file = new(filename, FileMode.Append, FileAccess.ReadWrite);
            
        }

        // 关键信息文件提取
        public void infoExact()
        {
            string filename = game.GetName() + " SeasonPass" + seasonNumber.ToString() + ".txt";
            FileStream file = new(filename, FileMode.Open, FileAccess.Read);
        }
    }


    // 主程序运行
    class excuteProgram
    {
        public void Main(string[] args)
        {
            Game d2Game = new Game();
            d2Game.setValues(1, "Destiny2", "Destiny2 is a ...", "", "Bungie", "PC/Epic");
            Game yugiohMDGame = new Game();
            yugiohMDGame.setValues(2, "Yu-Gi-Oh! Master Duel", "MasterDuel is a ...", "", "Konami", "PC/Steam");
            Game apexGame = new Game();
            apexGame.setValues(3, "Apex Legends", "", "", "Respawn", "PC/Steam");

            Destiny2 d2 = new Destiny2(d2Game);
            YugiohMD ygoMD = new YugiohMD(yugiohMDGame);
        }
    }
}