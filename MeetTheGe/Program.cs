using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeetTheGe
{
    internal class Program
    {
        private static Ge[] geRoster = { new Ge(), new Ge("Ge2", 15, 0.98, "regular Ge"), new Ge("bonk", 17, 0.5, "he's a bonk", "bonk.") };
        private static Ge[] leaderboardR = { null, null, null, null, null, null, null, null, null, null };
        private static long[] scoresR = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private static Ge[] leaderboardB = { null, null, null, null, null, null, null, null, null, null };
        private static long[] scoresB = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private static Ge[] leaderboardG = { null, null, null, null, null, null, null, null, null, null };
        private static long[] scoresG = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private static Random random = new Random();
        static void Main(string[] args)
        {
            MakeGe();
            MakeGe();
            MakeGe();
            MakeGe();
            MakeGe();
            MakeGe();
            while (true)
            {
                MakeGe();
                MakeGe();
                MakeGe();
                BattleRoyale(geRoster);
                BarFight(geRoster);
            }
        }

        public static void BattleRoyale(Ge[] participants)
        {
            bool[] kod = new bool[participants.Length];
            long[] scorekeeper = new long[participants.Length];
            int[] levels = new int[participants.Length];
            int[] lastGe = new int[participants.Length];

            Console.WriteLine("BATTLE ROYALE!!!!");

            while (MoreThanOneLeft(kod))
            {
                int index = 0;
                int failsafe = 0;
                bool found = false;
                Console.Write("working on l1...");
                while (!found)
                {
                    if (failsafe <= 10)
                    {
                        index = random.Next(0, participants.Length);
                        if (!kod[index] && (lastGe[index] <= LeastAmt(lastGe, kod)))
                        {
                            found = true;
                        }
                        else
                        {
                            Console.Write(".");
                            failsafe++;
                        }
                    }
                    else
                    {
                        for (int xl = 0; xl < lastGe.Length; xl++)
                        {
                            lastGe[xl]--;
                            if (lastGe[xl] < 0)
                            {
                                lastGe[xl] = 0;
                            }
                        }
                        failsafe = 0;
                    }
                }
                Console.WriteLine("l1 done!");

                lastGe[index] = 2;

                for (int i = 0; i < participants.Length; i++)
                {
                    if (!kod[i] && i != index)
                    {
                        Console.WriteLine(i + " - " + participants[i].NameWithInfo(participants[i].InfoBR()));
                    }
                }

                Console.WriteLine("Who does " + participants[index].Name + " fight? [" + participants[index].InfoBR() + "]");
                int ind2 = 0;
                bool successful = false;
                while (!successful)
                {
                    Console.Write("Enter a positive integer: ");
                    successful = int.TryParse(Console.ReadLine(), out ind2);
                    if (ind2 < 0 || ind2 >= participants.Length || kod[ind2] || !successful)
                    {
                        Console.WriteLine("Invalid input. Try again.");
                        successful = false;
                    }
                }
                bool isTrueGe = participants[index].Attack(participants[ind2]);
                if (isTrueGe)
                {
                    kod[ind2] = true;
                    scorekeeper[index] += 10000;
                    levels[index]++;
                    Console.WriteLine("10000 added to " + participants[index].Name + "'s score");
                }
                else
                {
                    scorekeeper[index] += 5000;
                    Console.WriteLine("5000 added to " + participants[index].Name + "'s score");
                }
                Console.Write("  ENTER TO CONTINUE>>>");
                _ = Console.ReadLine();

                Console.Write("working on l2...");
                for (int xl = 0; xl < lastGe.Length; xl++)
                {
                    lastGe[xl]--;
                    if (lastGe[xl] < 0)
                    {
                        lastGe[xl] = 0;
                    }
                }
                Console.WriteLine("l2 done!");
            }

            int val = 0;
            for (int j = 0; j < participants.Length; j++)
            {
                if (!kod[j])
                {
                    val = j;
                }
                participants[j].ResetHP();
            }
            Console.WriteLine(participants[val].Name + " won!");
            scorekeeper[val] += 25000;
            Console.WriteLine("25000 added to " + participants[val].Name + "'s score");
            UpdateLBR(participants, scorekeeper);
        }

        private static void UpdateLBR(Ge[] list, long[] newScores)
        {
            Console.WriteLine("Updating leaderboard...");
            for (int l = 0; l < list.Length; l++)
            {
                Ge tempGe = list[l];
                long tempScore = newScores[l];
                for (int f = 0; f < scoresR.Length; f++)
                {
                    if (tempScore > scoresR[f])
                    {
                        Ge tge = leaderboardR[f];
                        long tsc = scoresR[f];
                        leaderboardR[f] = tempGe;
                        scoresR[f] = tempScore;
                        tempGe = tge;
                        tempScore = tsc;
                    }
                }
            }
            PrintLBR();
        }

        public static void PrintLBR()
        {
            Console.WriteLine("Battle Royale Leaderboard: ");
            for (int t = 0; t < 10; t++)
            {
                if (t + 1 < 10)
                    Console.Write(" ");
                if (leaderboardR[t] != null)
                    Console.WriteLine((t + 1) + ". - " + leaderboardR[t].Name + " - Score: " + scoresR[t]);
            }
            Console.WriteLine("  ENTER TO CONTINUE>>>");
            _ = Console.ReadLine();
        }

        public static void BarFight(Ge[] participants)
        {
            bool[] kod = new bool[participants.Length];
            long[] scorekeeper = new long[participants.Length];
            int[] levels = new int[participants.Length];
            for (int i = 0; i < levels.Length; i++)
                levels[i] = 1;
            int[] lastGe = new int[participants.Length];

            Console.WriteLine("BAR FIGHT!!!!");

            while (MoreThanOneLeft(kod))
            {
                int index = 0;
                int failsafe = 0;
                bool found = false;
                Console.Write("working on l1...");
                while (!found)
                {
                    if (failsafe <= 10)
                    {
                        index = random.Next(1, participants.Length);
                        if (!kod[index] && (lastGe[index] <= LeastAmt(lastGe, kod)))
                        {
                            found = true;
                        }
                        else
                        {
                            Console.Write(".");
                            failsafe++;
                        }
                    }
                    else
                    {
                        for (int xl = 0; xl < lastGe.Length; xl++)
                        {
                            lastGe[xl]--;
                            if (lastGe[xl] < 0)
                            {
                                lastGe[xl] = 0;
                            }
                        }
                        failsafe = 0;
                    }
                }
                Console.WriteLine("l1 done!");

                lastGe[index] = 2;

                for (int i = 0; i < participants.Length; i++)
                {
                    if (!kod[i] && i != index)
                    {
                        Console.WriteLine(i + " - " + participants[i].NameWithInfo(participants[i].InfoBF()));
                    }
                }

                Console.WriteLine("Who does " + participants[index].Name + " fight? [" + participants[index].InfoBF() + "]");
                int ind2 = 0;
                bool successful = false;
                while (!successful)
                {
                    Console.Write("Enter a positive integer: ");
                    successful = int.TryParse(Console.ReadLine(), out ind2);
                    if (ind2 < 0 || ind2 >= participants.Length || kod[ind2] || !successful)
                    {
                        Console.WriteLine("Invalid input. Try again.");
                        successful = false;
                    }
                }
                bool isTrueGe = Ge.SizeUp(participants[index], participants[ind2], levels[index], levels[ind2]);
                if (isTrueGe)
                {
                    kod[ind2] = true;
                    scorekeeper[index] += 10000;
                    levels[index]++;
                    Console.WriteLine("10000 added to " + participants[index].Name + "'s score (level up!)");
                }
                else
                {
                    if (participants[index].GetHp() == 0)
                    {
                        kod[index] = true;
                    }

                    scorekeeper[ind2] += 5000;
                    Console.WriteLine("5000 added to " + participants[ind2].Name + "'s score");
                }
                Console.Write("  ENTER TO CONTINUE>>>");
                _ = Console.ReadLine();

                Console.Write("working on l2...");
                for (int xl = 0; xl < lastGe.Length; xl++)
                {
                    lastGe[xl]--;
                    if (lastGe[xl] < 0)
                    {
                        lastGe[xl] = 0;
                    }
                }
                Console.WriteLine("l2 done!");
            }

            int val = 0;
            for (int j = 0; j < participants.Length; j++)
            {
                if (!kod[j])
                {
                    val = j;
                }
                participants[j].ResetHP();
            }
            Console.WriteLine(participants[val].Name + " won!");
            scorekeeper[val] += 25000;
            Console.WriteLine("25000 added to " + participants[val].Name + "'s score");
            UpdateLB(participants, scorekeeper);
        }

        private static void UpdateLB(Ge[] list, long[] newScores)
        {
            Console.WriteLine("Updating leaderboard...");
            for (int l = 0; l < list.Length; l++)
            {
                Ge tempGe = list[l];
                long tempScore = newScores[l];
                for (int f = 0; f < scoresB.Length; f++)
                {
                    if (tempScore > scoresB[f])
                    {
                        Ge tge = leaderboardB[f];
                        long tsc = scoresB[f];
                        leaderboardB[f] = tempGe;
                        scoresB[f] = tempScore;
                        tempGe = tge;
                        tempScore = tsc;
                    }
                }
            }
            PrintLB();
        }

        public static void PrintLB()
        {
            Console.WriteLine("Bar-Fight Leaderboard: ");
            for (int t = 0; t < 10; t++)
            {
                if (t + 1 < 10)
                    Console.Write(" ");
                if (leaderboardB[t] != null)
                    Console.WriteLine(t + 1 + ". - " + leaderboardB[t].Name + " - Score: " + scoresB[t]);
            }
            Console.WriteLine("  ENTER TO CONTINUE>>>");
            _ = Console.ReadLine();
        }

        public static void GingerGames(Ge[] participants)
        {
            //nothing
        }

        private static int LeastAmt(int[] list, bool[] blist)
        {
            int val = list[0];
            for (int b = 0; b < list.Length; b++)
            {
                if (!blist[b] && list[b] < val)
                    {
                    val = list[b];
                }
            }

            return val;
        }

        private static int LeastAmt(int[] list, bool[] blist, int ext)
        {
            int val = list[0];
            for (int b = 0; b < list.Length; b++)
            {
                if (!blist[b] && b != ext && list[b] < val)
                {
                    val = list[b];
                }
            }

            return val;
        }

        private static bool MoreThanOneLeft(bool[] list)
        {
            int val = 0;
            foreach (bool b in list)
            {
                if (!b)
                {
                    val++;
                }
            }

            if (val > 1)
            {
                return true;
            }

            return false;
        }

        private static void MakeGe()
        {
            Console.WriteLine("Would you like to make a new Ge?\nEnter \"yes\" in all lowercase to continue: ");
            string userInputStr = Console.ReadLine();
            if (userInputStr != "yes")
                return;
            Console.WriteLine("What is the name of the new Ge?\nEnter here: ");
            string str2 = Console.ReadLine();
            Console.WriteLine("How powerful is " + str2 + "?");
            int input3 = 0;
            bool successful = false;
            while (!successful)
            {
                Console.Write("Enter a positive integer (1-24): ");
                successful = int.TryParse(Console.ReadLine(), out input3);
                if (input3 <= 0 || input3 >= 25 || !successful)
                {
                    Console.WriteLine("Ge-huh!? Try again.");
                    successful = false;
                }
            }
            Console.WriteLine("What percentage of Ge is " + str2 + "?");
            double input4 = 0.0;
            successful = false;
            while (!successful)
            {
                Console.Write("Enter a percentage in decimal form (e.g. for 25%, do 0.25): ");
                successful = double.TryParse(Console.ReadLine(), out input4);
                if (input4 <= 0 || input4 > 1 || !successful)
                {
                    Console.WriteLine("Ge-huh!? Try again.");
                    successful = false;
                }
            }
            Console.WriteLine("What type of Ge is "+str2+"?\nEnter here: ");
            string str5 = Console.ReadLine();
            Console.WriteLine("Would you give "+str2+" a cool scream?\nEnter \"yes\" in all lowercase to continue: ");
            userInputStr = Console.ReadLine();
            if (userInputStr != "yes")
            {
                Ge newGe1 = new Ge(str2, input3, input4, str5);
                AddGe(newGe1);
                return;
            }
            Console.WriteLine("What is " + str2 + "'s cool scream?\nEnter here: ");
            string str6 = Console.ReadLine();
            Ge newGe = new Ge(str2, input3, input4, str5, str6);
            AddGe(newGe);
        }

        private static void AddGe(Ge ge)
        {
            Ge[] newRoster = new Ge[geRoster.Length + 1];
            for (int i = 0; i < geRoster.Length; i++)
            {
                newRoster[i] = geRoster[i];
            }
            newRoster[geRoster.Length] = ge;
            geRoster = newRoster;
            Console.WriteLine("Added " + ge.Name + " to the Ge Roster!");
        }
    }

    internal class Ge
    {
        private int hp;
        private static Random random = new Random();
        private static readonly string[] methodsOfAttack = { " launched a pizza onto the face of ", " swangled out ", " screamed and dug a hole under ", " defenestrated ", " screamed about jawn and ate the forehead of ", " perpetuated the study of Quakers and had a Dingo duel with ", " won the Dingo duel against ", " won the Great Amberturian Algo John from ", " super-duper-ultra-mega-giga-great-terrified ", " won the heart of ", " stang out the mind of ", " turned into the supreme overlord and decimated ", " drove over ", " hawackadid ", " became the soldy old yougassey and sold the dead fish to ", " told the Great Jeffamac Jawnery to scweam out of a cannon and fire upon ", " pulled a 360-ultima-xD-true-gamer-double-pantsy noscope on ", " told the Great McFireinagee to incinerate ", " told the Great Hangover McGininin to fly away with ", " bloxted ", " screamed \"&CS\" at ", " underdeminadated ", " told Don von Dragon to fly out of a pinpob at ", " rang the bell and flung it at " };

        public Ge()
        {
            Name = "Ge";
            Power = 12;
            hp = 50;
            GePercent = 1;
            Type = "regular Ge";
            CoolScream = "GEEEEHHHHH!!!";
        }

        public Ge(string name, int power, double gePct, string type)
        {
            Name = name;
            Power = power;
            hp = 50;
            GePercent = gePct;
            Type = type;
            CoolScream = "GEEEEHHHHH!!!";
        }

        public Ge(string name, int power, double gePct, string type, string scream)
        {
            Name = name;
            Power = power;
            hp = 50;
            GePercent = gePct;
            Type = type;
            CoolScream = scream;
        }

        public string CoolScream { get; set; }
        public string Name { get; private set; }
        public int Power
        {
            get => Power;

            private set
            {
                if (value > 0 && value < 25)
                {
                    Power = value;
                }
            }
        }
        public double GePercent
        {
            get => GePercent;

            private set
            {
                if (value > 0 && value <= 100)
                {
                    GePercent = value;
                }
            }
        }
        public string Type { get; private set; }

        public int GetHp() { return hp; }

        public void ResetHP() { hp = 50; }

        public bool IsKod()
        {
            if (hp == 0)
                return true;
            else
                return false;
        }

        public void TakeDmg(int ouchie)
        {
            hp -= ouchie;
            if (hp <= 0)
            {
                hp = 0;
                Console.WriteLine(Name + " got KO'd!");
            }
        }

        public bool Attack(Ge enemyGe)
        {
            if (enemyGe == null)
            {
                Console.WriteLine("Ge-huh!?");
                return false;
            }

            int randNum1 = random.Next(0, methodsOfAttack.Length);
            Console.WriteLine(Name + FormatMSG(methodsOfAttack[randNum1]) + enemyGe.Name + "!");
            enemyGe.TakeDmg(Power);
            return enemyGe.IsKod();
        }

        private string FormatMSG(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return msg;
            }

            int idx;

            if (msg.Contains("&CS"))
            {
                idx = msg.IndexOf("&CS");
                msg = msg.Substring(0, idx) + CoolScream + msg.Substring(idx + 3);
            }

            return msg;
        }

        public static bool SizeUp(Ge ge1, Ge ge2, int lev1 = 1, int lev2 = 1)
        {
            if (ge1 == null || ge2 == null)
            {
                Console.WriteLine("Ge-huh!?");
                return false;
            }

            if (ge1.Equals(ge2))
            {
                Console.WriteLine("Ge-huh!?");
                return false;
            }

            if (ge1.GePercent * lev1 > ge2.GePercent * lev2)
            {
                Console.WriteLine(ge1.Name + " is more Ge than " + ge2.Name);
                ge2.hp = 0;
                Console.WriteLine(ge2.Name + " got KO'd!");
                return true;
            }
            else if (ge1.GePercent * lev1 < ge2.GePercent * lev2)
            {
                Console.WriteLine(ge2.Name + " is more Ge than " + ge1.Name);
                ge1.hp = 0;
                Console.WriteLine(ge1.Name + " got KO'd!");
            }
            else
            {
                Console.WriteLine(ge1.Name + " and " + ge2.Name + " are both equal Ge");
            }
            return false;
        }

        public int Scream(int numOfRecip)
        {
            Console.WriteLine(Name + " screamed \"" + CoolScream + "\"");
            return Power / numOfRecip;
        }

        override public string ToString()
        {
            return $"Name: {Name}\n Power: {Power}\n HP: {hp}\n Percentage of Ge: {GePercent:p0}\n Type of Ge: {Type}\n Cool Scream: \"{CoolScream}\"";
        }

        public string NameWithInfo(string info)
        {
            return $"Name: {Name}; {info}";
        }

        public string InfoGG()
        {
            return $"Power: {Power}; HP: {hp}; Percentage of Ge: {GePercent:p0}; Type of Ge: {Type}; Cool Scream: \"{CoolScream}\"";
        }

        public string InfoBR()
        {
            return $"Power: {Power}; HP: {hp}; Type: {Type}; Cool Scream: \"{CoolScream}\"";
        }

        public string InfoBF()
        {
            return $"Percentage of Ge: {GePercent:p0}; Type: {Type}; Cool Scream: \"{CoolScream}\"";
        }
    }
}
