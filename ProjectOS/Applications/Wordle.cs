using Cosmos.System;
using ProjectOS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 - This wordle code was based by CjDave's github repository "Wordle_Console" and made changes to match with the group's OS
 */

namespace ProjectOS.Applications
{
    internal class WordleCommand : Command
    {
        public WordleCommand(string name) : base(name) { }

        public override string execute(string[] args)
        {
            System.Console.Clear();
            Wordle wordle = new Wordle();
            return " ";
        }
    }

    internal class Wordle
    {
        enum Color { gray, yellow, green }
        enum MessageType { error, info, success }

        private readonly Data data = new Data();

        int tries, letter, currentTry;
        string[] words;
        string answer;

        public Wordle()
        {
            init(6, 5);
            bool correct = false;

            while (!correct && currentTry < tries)
            {
                printMessage($"Enter a {letter}-letter word. Current try is: {currentTry}", MessageType.info);
                string input = System.Console.ReadLine();

                if (input.Length != 5)  // Check for 5-letter words
                {
                    printMessage("This is not a valid 5-letter word.", MessageType.error);
                }
                else if (words.Contains(input))
                {
                    correct = CheckWord(input, answer, letter);
                    currentTry++;
                }
                else
                {
                    printMessage("Word is not in the list.", MessageType.error);
                }
            }

            if (!correct)
            {
                printMessage("Sorry, You Failed. The Word was:", MessageType.info);
                printMessage(answer, MessageType.success);

            }
            RetryOrExit(correct);
        }

        void init(int Tries, int Letter)
        {
            tries = Tries;
            letter = Letter;
            currentTry = 0;
            words = data.GetWordList();
            answer = SetAnswer(words);
        }

        string SetAnswer(string[] wordList)
        {
            Random rnd = new Random();
            int r = rnd.Next(0, wordList.Length);
            return wordList[r];
        }

        bool CheckWord(string input, string a, int count)
        {
            if (input == a)
            {
                printMessage(input, MessageType.success);
                printMessage("Congratulations! You Won.", MessageType.success);
                return true;
            }

            char[] answer = a.ToCharArray();
            char[] inputArray = input.ToCharArray();
            Color color;

            for (int i = 0; i < count; i++)
            {
                color = Color.gray;

                for (int d = 0; d < answer.Length; d++)
                {
                    if (inputArray[i] == answer[d])
                    {
                        color = d == i ? Color.green : Color.yellow;
                        if (color == Color.green)
                        {
                            answer[d] = ' ';
                            break;
                        }
                    }
                }

                printLetter(color, inputArray[i]);
            }

            System.Console.WriteLine("");
            return false;
        }

        void printLetter(Color color, char letter)
        {
            switch (color)
            {
                case Color.green:
                    printText(letter.ToString(), ConsoleColor.Green, ConsoleColor.White, false);
                    break;
                case Color.yellow:
                    printText(letter.ToString(), ConsoleColor.Yellow, ConsoleColor.Black, false);
                    break;
                case Color.gray:
                    printText(letter.ToString(), ConsoleColor.Gray, ConsoleColor.Black, false);
                    break;
            }
        }

        void printText(string text, ConsoleColor bColor, ConsoleColor fColor, bool isLine)
        {
            System.Console.BackgroundColor = bColor;
            System.Console.ForegroundColor = fColor;
            if (isLine)
            {
                System.Console.WriteLine(text);
            }
            else
            {
                System.Console.Write(text);
            }
            System.Console.ResetColor();
        }

        void printMessage(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.error:
                    printText(message, ConsoleColor.Red, ConsoleColor.White, true);
                    break;
                case MessageType.info:
                    printText(message, ConsoleColor.White, ConsoleColor.Black, true);
                    break;
                case MessageType.success:
                    printText(message, ConsoleColor.Green, ConsoleColor.Black, true);
                    break;
            }
        }

        public class Data
        {
            public string[] GetWordList()
            {
                return new string[]
                {
                 "about","above","abuse","actor","acute","admit","adopt","adult","after","again","agent","agree",
                 "ahead","alarm","album","alert","alike","alive","allow","alone","along","alter","among","anger",
                 "Angle","angry","apart","apple","apply","arena","argue","arise","array","aside","asset","audio",
                 "audit","avoid","award","aware","badly","baker","bases","basic","basis","beach","began","begin",
                 "begun","being","below","bench","billy","birth","black","blame","blind","block","blood","board",
                 "boost","booth","bound","brain","brand","bread","break","breed","brief","bring","broad","broke",
                 "brown","build","built","buyer","cable","calif","carry","catch","cause","chain","chair","chart",
                 "chase","cheap","check","chest","chief","child","china","chose","civil","claim","class","clean","clear","click","clock","close","coach","coast","could","count","court","cover","craft","crash","cream","crime","cross","crowd","crown","curve","cycle","daily","dance","dated","dealt","death","debut","delay","depth","doing","doubt","dozen","draft","drama","drawn","dream","dress","drill","drink","drive","drove","dying","eager","early","earth","eight","elite","empty","enemy","enjoy","enter","entry","equal","error","event","every","exact","exist","extra","faith","false","fault","fiber","field","fifth","fifty","fight","final","first","fixed","flash","fleet","floor","fluid","focus","force","forth","forty","forum","found","frame","frank","fraud","fresh","front","fruit","fully","funny","giant","given","glass","globe","going","grace","grade","grand","grant","grass","great","green","gross","group","grown","guard","guess","guest","guide","happy","harry","heart","heavy","hence","henry","horse","hotel","house","human","ideal","image","index","inner","input","issue","japan","jimmy","joint","jones","judge","known","label","large","laser","later","laugh","layer","learn","lease","least","leave","legal","level","lewis","light","limit","links","lives","local","logic","loose","lower","lucky","lunch","lying","magic","major","maker","march","maria","match","maybe","mayor","meant","media","metal","might","minor","minus","mixed","model","money","month","moral","motor","mount","mouse","mouth","movie","music","needs","never","newly","night","noise","north","noted","novel","nurse","occur","ocean","offer","often","order","other","ought","paint","panel","paper","party","peace","peter","phase","phone","photo","piece","pilot","pitch","place","plain","plane","plant","plate","point","pound","power","press","price","pride","prime","print","prior","prize","proof","proud","prove","queen","quick","quiet","quite","radio","raise","range","rapid","ratio","reach","ready","refer","right","rival","river","robin","roger","roman","rough","round","route","royal","rural","scale","scene","scope","score","sense","serve","seven","shall","shape","share","sharp","sheet","shelf","shell","shift","shirt","shock","shoot","short","shown","sight","since","sixth","sixty","sized","skill","sleep","slide","small","smart","smile","smith","smoke","solid","solve","sorry","sound","south","space","spare","speak","speed","spend","spent","split","spoke","sport","staff","stage","stake","stand","start","state","steam","steel","stick","still","stock","stone","stood","store","storm","story","strip","stuck","study","stuff","style","sugar","suite","super","sweet","table","taken","taste","taxes","teach","teeth","terry","texas","thank","theft","their","theme","there","these","thick","thing","think","third","those","three","threw","throw","tight","times","tired","title","today","topic","total","touch","tough","tower","track","trade","train","treat","trend","trial","tried","tries","truck","truly","trust","truth","twice","under","undue","union","unity","until","upper","upset","urban","usage","usual","valid","value","video","virus","visit","vital","voice","waste","watch","water","wheel","where","which","while","white","whole","whose","woman","women","world","worry","worse","worst","worth","would","wound","write","wrong"

                };
            }
        }

        private void RetryOrExit(bool correct)
        {
            if (correct)
            {
                System.Console.WriteLine("Congratulations! You guessed the word.");
                System.Console.WriteLine("Type \"1\" to Play Again or type \"2\" to Exit Wordle");
            }
            else
            {
                System.Console.WriteLine("Type \"1\" to Retry/Continue or type \"2\" to Exit Wordle");
            }

            string userInput = System.Console.ReadLine();

            if (userInput != null)
            {
                if (userInput == "1")
                {
                    System.Console.Clear();
                    new Wordle(); // Start a new game
                }
                else if (userInput == "2")
                {
                    System.Console.Clear();
                    Kernel.DisplayDefaultMessage();
                }
            }
            else
            {
                System.Console.WriteLine("Error!");
            }
        }
    }
        
}

