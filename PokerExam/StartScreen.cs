using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExam
{
    
    static class StartScreen
    {
        static bool ShowBanner = false;
        public static void ShowStartScreen()
        {

            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.Clear();

                    string[] banner = {
          ".S_sSSs      sSSs_sSSs     .S    S.     sSSs   .S_sSSs            sSSs  sdSS_SSSSSSbs   .S_SSSs     .S_sSSs      sSSs",
".SS~YS%%b    d%%SP~YS%%b   .SS    SS.   d%%SP  .SS~YS%%b          d%%SP  YSSS~S%SSSSSP  .SS~SSSSS   .SS~YS%%b    d%%SP",
"S%S   `S%b  d%S'     `S%b  S%S    S&S  d%S'    S%S   `S%b        d%S'         S%S       S%S   SSSS  S%S   `S%b  d%S'",
"S%S    S%S  S%S       S%S  S%S    d*S  S%S     S%S    S%S        S%|          S%S       S%S    S%S  S%S    S%S  S%|",
"S%S    d*S  S&S       S&S  S&S   .S*S  S&S     S%S    d*S        S&S          S&S       S%S SSSS%S  S%S    d*S  S&S",
"S&S   .S*S  S&S       S&S  S&S_sdSSS   S&S_Ss  S&S   .S*S        Y&Ss         S&S       S&S  SSS%S  S&S   .S*S  Y&Ss",
"S&S_sdSSS   S&S       S&S  S&S~YSSY%b  S&S~SP  S&S_sdSSS         `S&&S        S&S       S&S    S&S  S&S_sdSSS   `S&&S",
"S&S~YSSY    S&S       S&S  S&S    `S%  S&S     S&S~YSY%b           `S*S       S&S       S&S    S&S  S&S~YSY%b     `S*S",
"S*S         S*b       d*S  S*S     S%  S*b     S*S   `S%b           l*S       S*S       S*S    S&S  S*S   `S%b     l*S",
"S*S         S*S.     .S*S  S*S     S&  S*S.    S*S    S%S          .S*P       S*S       S*S    S*S  S*S    S%S    .S*P",
"S*S          SSSbs_sdSSS   S*S     S&   SSSbs  S*S    S&S        sSS*S        S*S       S*S    S*S  S*S    S&S  sSS*S",
"S*S           YSSP~YSSY    S*S     SS    YSSP  S*S    SSS        YSS'         S*S       SSS    S*S  S*S    SSS  YSS'",
"SP                         SP                  SP                             SP               SP   SP",
"Y                          Y                   Y                              Y                Y    Y"

        };
                    foreach (string line in banner)
                        Console.WriteLine(line);

                    if (ShowBanner == true)
                    {
                        Console.WriteLine("                                              -------PRESS ENTER TO CONTINUE------");
                    }
                    else Console.WriteLine(" ");

                    ShowBanner = !ShowBanner;
                    System.Threading.Thread.Sleep(500);

                }

            }
            while (Console.ReadKey(true).Key != ConsoleKey.Enter);
            {
                return;
            }


        }
    }
}
