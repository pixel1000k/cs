using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace projekt_zaliczeniowy
{
    public static class G // zglobalizowane zmienne i listy, by było łatwiej
    {
        public static int[] PInfo = { 1, 100, 30, 0, 10, 5,};// lvl 0, hp 1, gold 2, atak(broń) 3 , atak 4 , defense 5
        public static int[] GoblinInfo = { 30, 5, 15, 5, 0 };// hp 0, zdobywany gold 1 , górna granica ataku 2 , defense 3 , typ 4
        public static int[] BanditInfo = { 50, 15, 10, 0, 0 };// tak samo jak z goblinem
        public static int i = 0, skillI, localPHp = PInfo[1], localEHp = GoblinInfo[0], totaldmg, totalEdmg, pom, pom2, pom3 ,x = 1,lvlpoints = 0,xpt = 100;
        public static double BTypeDmg, PomDmg;                        // pom oznaczają zmienne pomocnicze, głównie przenoszą wartość random
        public static string enemy = "goblin", skill = "", Class = "", input1 = "",input2 = "";
        public static string[] PlayerSkillSet = { "normal", "fire", "water", "grass" };
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            var ETypes = new Dictionary<int, string>();
            ETypes[0] = "normal";
            ETypes[1] = "fire";
            ETypes[2] = "water";
            ETypes[3] = "grass";
            var Icosts = new Dictionary<string, int>();// koszt itemów
            Icosts["dagger"] = 5;
            Icosts["sword"] = 30;
            Icosts["hat"] = 15;
            Icosts["chainmail"] = 50;
            var Istats = new Dictionary<string, int>();// boosty od itemów
            Istats["dagger"] = 5;
            Istats["sword"] = 15;
            Istats["hat"] = 1;
            Istats["chainmail"] = 10;
            var Ibool = new Dictionary<string, bool>();// słownik przypisujący wartość itemom, true jeśli są kupione, false jeśli nie
            Ibool["dagger"] = false;
            Ibool["sword"] = false;
            Ibool["hat"] = false;
            Ibool["chainmail"] = false;
            while (true){
                Console.WriteLine("Your journey begins\n\nPick your class:\n1.Tank(hp+,dmg-)\n2.Berserk(none)\n3.Assasin(dmg+,hp-)");
                G.Class = Console.ReadLine();
                if (G.Class == "1" || G.Class == "2" || G.Class == "3") break;
                Console.Clear();
                Console.WriteLine("invalid command");
                Thread.Sleep(1000);
                Console.Clear();
            }
            switch (G.Class){
                case "1":
                    G.PInfo[1] += 50;
                    G.PInfo[4] -= 2; break;
                case "2":
                    break;
                case "3":
                    G.PInfo[1] -= 25;
                    G.PInfo[4] += 5; break;
            }
            Console.Clear();
            while (true){
                while (true){
                    Console.WriteLine($"Chose your action({G.x}):\n1.go on journey\n2.shop\n3.see stats");
                    G.input1 = Console.ReadLine();
                    if (G.input1 == "1" || G.input1 == "2" || G.input1 == "3") break;
                    Console.Clear();
                    Console.WriteLine("invalid command");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                switch (G.input1){
                    case "1":
                        Random rnd2 = new Random();
                        G.pom2 = rnd2.Next(1, 3);// random do losowania przeciwnika
                        Random rnd3 = new Random();
                        G.pom3 = rnd3.Next(0, 4);// random do losowania typu przeciwnika
                        switch (G.pom2)
                        {
                            case 1://goblin
                                Fight(G.GoblinInfo, "goblin", G.pom3);
                                G.xpt += 20;
                                break;
                            case 2://bandit
                                Fight(G.BanditInfo, "bandit", G.pom3);
                                break;
                        }
                        G.x++;
                        break;
                    case "2":
                        while (true){
                            while (true){
                                Console.Clear();
                                Console.WriteLine($"shop\n\ndmg:\n1.dagger - {Icosts["dagger"]}\n2.sword - {Icosts["sword"]}\n");
                                Console.WriteLine($"def:\n3.hat - {Icosts["hat"]}\n4.chainmail - {Icosts["chainmail"]}\n");
                                Console.WriteLine("quit\n");
                                G.input2 = Console.ReadLine();
                                if (G.input2 == "quit" || G.input2 == "dagger" || G.input2 == "sword" || G.input2 == "hat" || G.input2 == "chainmail") break;
                                Console.Clear();
                                Console.WriteLine("invalid command");
                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            if (G.input2 == "quit") { 
                                Console.Clear();
                                break; 
                            }
                            else if (Ibool[G.input2] == false && G.PInfo[2] >= Icosts[G.input2])
                            {
                                G.PInfo[2] -= Icosts[G.input2];
                                if (G.input2 == "dagger" || G.input2 == "sword") G.PInfo[3] += Istats[G.input2];
                                else if (G.input2 == "hat" || G.input2 == "chainmail") G.PInfo[5] += Istats[G.input2];
                                Ibool[G.input2] = true;
                                Console.Clear();
                                Console.WriteLine("you have bought a " + G.input2);
                                Thread.Sleep(2000);
                            }
                            else if (G.PInfo[2] <= Icosts[G.input2])
                            {
                                Console.Clear();
                                Console.WriteLine("you are too poor to buy a " + G.input2);
                                Thread.Sleep(2000);
                            }
                            else if (Ibool[G.input2] == true)
                            {
                                Console.Clear();
                                Console.WriteLine("you already have a " + G.input2 + "\n");
                                Thread.Sleep(2000);
                            }
                        }
                        break;
                    case "3":
                        Console.Clear();
                        G.PInfo[0] = G.xpt/100;
                        G.lvlpoints = G.xpt % 100;
                        Console.WriteLine($"lvl - {G.PInfo[0]} ({G.lvlpoints}/100)\nhp - {G.PInfo[1]}\ngold - {G.PInfo[2]}\ndmg - {G.PInfo[4] + G.PInfo[3]}\n");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }

        }
        static void Fight(int[] enemyArr, string Ene, int a){
            enemyArr[4] = a;
            while (true)
            {
                var ETypes = new Dictionary<int, string>();// typy przeciwników
                ETypes[0] = "normal";
                ETypes[1] = "fire";
                ETypes[2] = "water";
                ETypes[3] = "grass";
                G.i++;
                //runda gracza
                Console.Clear();
                while (true)
                {
                    Console.WriteLine("Round " + G.i);
                    Console.WriteLine($"    O           O\n   / \\         / \\\n    |           |\n   / \\         / \\");
                    Console.WriteLine("player        " + Ene + " (" + ETypes[enemyArr[4]] + " type)");
                    Console.WriteLine("hp: " + G.localPHp + "       hp: " + G.localEHp + "\n");
                    Console.WriteLine($"Make a move:\n1.{G.PlayerSkillSet[0]}\n2.{G.PlayerSkillSet[1]}\n3.{G.PlayerSkillSet[2]}\n4.{G.PlayerSkillSet[3]}\n");
                    G.skill = Console.ReadLine();
                    if (G.skill == "1" || G.skill == "2" || G.skill == "3" || G.skill == "4") break;
                    Console.Clear();
                    Console.WriteLine("invalid command");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                Console.Clear();
                G.skillI = Convert.ToInt32(G.skill);
                switch (enemyArr[4]) // damage multiplier od żywiołów
                {      
                    case 0:
                        G.BTypeDmg = 1; break;
                    case 1:
                        switch (G.skill) {
                            case "1":
                                G.BTypeDmg = 1; break;
                            case "2":
                                G.BTypeDmg = 1; break; 
                            case "3":
                                G.BTypeDmg = 1.5; break;
                            case "4":
                                G.BTypeDmg = 0.5; break;
                        }break;
                    case 2:
                        switch (G.skill)
                        {
                            case "1":
                                G.BTypeDmg = 1; break;
                            case "2":
                                G.BTypeDmg = 0.5; break;
                            case "3":
                                G.BTypeDmg = 1; break;
                            case "4":
                                G.BTypeDmg = 1.5; break;
                        }break;
                    case 3:
                        switch (G.skill)
                        {
                            case "1":
                                G.BTypeDmg = 1; break;
                            case "2":
                                G.BTypeDmg = 1.5; break;
                            case "3":
                                G.BTypeDmg = 0.5; break;
                            case "4":
                                G.BTypeDmg = 1; break;
                        }break;

                }
                G.totaldmg = G.PInfo[4] + G.PInfo[3] - enemyArr[3];
                G.PomDmg = G.BTypeDmg * G.totaldmg;
                G.totaldmg = (int)G.PomDmg;
                G.localEHp -= G.totaldmg;
                if(G.localEHp < 0) G.localEHp = 0;
                Console.WriteLine("Round " + G.i);
                Console.WriteLine($"    O           O\n   / \\         / \\\n    |           |\n   / \\         / \\");
                Console.WriteLine("player        " + Ene + " (" + ETypes[enemyArr[4]]+" type)");
                Console.WriteLine("hp: " + G.localPHp + "       hp: " + G.localEHp + "\n");
                Console.WriteLine("player uses " + G.PlayerSkillSet[G.skillI - 1] + " type attack, deals " + G.totaldmg + " damage");
                Console.ReadLine();
                Console.Clear();
                //runda przeciwnika
                Random random = new Random();
                G.pom = random.Next(-5, 5);
                G.totalEdmg = enemyArr[2] + G.pom - G.PInfo[5];
                G.localPHp -= G.totalEdmg;
                if(G.localPHp < 0) G.localEHp = 0;
                Console.WriteLine("Round " + G.i);
                Console.WriteLine($"    O           O\n   / \\         / \\\n    |           |\n   / \\         / \\");
                Console.WriteLine("player        " + Ene + " (" + ETypes[enemyArr[4]] + " type)");
                Console.WriteLine("hp: " + G.localPHp + "       hp: " + G.localEHp + "\n");
                Console.WriteLine($"{Ene} uses {ETypes[enemyArr[4]]} type attack, deals {G.totalEdmg} damage");
                Console.ReadLine();
                Console.Clear();
                if (G.localEHp <= 0)
                {
                    G.i = 0;
                    G.PInfo[2] += enemyArr[1];
                    Console.Clear ();
                    Console.WriteLine($"gg ez\n{Ene} has been slain\nyou gained {enemyArr[1]} gold");
                    G.PInfo[2] += enemyArr[1];
                    G.localPHp = G.PInfo[1];
                    G.localEHp = enemyArr[0];
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
                else if (G.localPHp <= 0)
                {
                    G.i = 0;
                    G.localPHp = G.PInfo[1];
                    G.localEHp = enemyArr[0];
                    Console.Clear ();
                    Console.WriteLine($"player has been slain\nyou lose :)");
                    break;
                }
            }
        }
    }
}
