using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SeasonPassStalker;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace PassStalker.Game
{
    internal class Destiny2 : SeasonPassStalker.SeasonPass
    {

        public Destiny2(SeasonPassStalker.Game gameInfo) : base(gameInfo)
        {
            SeasonPassStalker.Game d2 = new SeasonPassStalker.Game();
            d2.setValues(1, "Destiny2", "Destiny2 is a ...", "", "Bungie", "PC/Epic");
            game = d2;
        }

        public void getPassInfo()
        {
            // 信息处理
            
        }
    }
}
