using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{

    public class SoccerTeam
    {
        //attributes
        public string name;
        public string country;
        public int pool;
        public int group;
        public int tiebreak = 0;
        public int goalsFor = 0;
        public int goalsAgainst = 0;
        public int goalsAverage;
        public int points = 0;
                

        //constructor for name, country and pool
        public SoccerTeam(string name, string country, int pool)
        {
            this.name = name;
            this.country = country;
            this.pool = pool;
        }

        //default constructor
        public SoccerTeam()
        {

        }

       
    }

    static class ExtensionsClass
    {
        private static Random rng = new Random();

        //function to shuffle a list to create randomness
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        //function to swap to indices in a list
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }

    class Program
    {
            
        public static void Run()
        {    
            //create the list of teams to be entered in the tournament        
            List<SoccerTeam> TeamsList = new List<SoccerTeam>();

            //create the list of groups that the teams will compete in
            List<SoccerTeam> GroupList1 = new List<SoccerTeam>();
            List<SoccerTeam> GroupList2 = new List<SoccerTeam>();
            List<SoccerTeam> GroupList3 = new List<SoccerTeam>();
            List<SoccerTeam> GroupList4 = new List<SoccerTeam>();
            List<SoccerTeam> GroupList5 = new List<SoccerTeam>();
            List<SoccerTeam> GroupList6 = new List<SoccerTeam>();
            List<SoccerTeam> GroupList7 = new List<SoccerTeam>();
            List<SoccerTeam> GroupList8 = new List<SoccerTeam>();
            
            //create a list of GroupLists to easily access them using indices later
            var GroupList = new[] { GroupList1, GroupList2, GroupList3, GroupList4, GroupList5, GroupList6, GroupList7, GroupList8 };


            //add all the teams specified in the case info
            TeamsList.Add(new SoccerTeam("Bayern Munich", "Germany", 1));
            TeamsList.Add(new SoccerTeam("Sevilla", "Spain", 1));
            TeamsList.Add(new SoccerTeam("Real Madrid", "Spain", 1));
            TeamsList.Add(new SoccerTeam("Liverpool", "England", 1));
            TeamsList.Add(new SoccerTeam("Juventus", "Italy", 1));
            TeamsList.Add(new SoccerTeam("Paris Saint-Germain", "France", 1));
            TeamsList.Add(new SoccerTeam("Zenit", "Russia", 1));
            TeamsList.Add(new SoccerTeam("Porto", "Portugal", 1));

            TeamsList.Add(new SoccerTeam("Barcelona", "Spain", 2));
            TeamsList.Add(new SoccerTeam("Atlético Madrid", "Spain", 2));
            TeamsList.Add(new SoccerTeam("Manchester City", "England", 2));
            TeamsList.Add(new SoccerTeam("Manchester United", "England", 2));
            TeamsList.Add(new SoccerTeam("Borussia Dortmund", "Germany", 2));
            TeamsList.Add(new SoccerTeam("Shakhtar Donetsk", "Ukraine", 2));
            TeamsList.Add(new SoccerTeam("Chelsea", "England", 2));
            TeamsList.Add(new SoccerTeam("Ajax", "Holland", 2));

            TeamsList.Add(new SoccerTeam("Dynamo Kiev", "Ukraine", 3));
            TeamsList.Add(new SoccerTeam("Red Bull Salzburg", "Germany", 3));
            TeamsList.Add(new SoccerTeam("RB Leipzig", "Germany", 3));
            TeamsList.Add(new SoccerTeam("Internazionale", "Italy", 3));
            TeamsList.Add(new SoccerTeam("Olympiacos", "Greece", 3));
            TeamsList.Add(new SoccerTeam("Lazio", "Italy", 3));
            TeamsList.Add(new SoccerTeam("Krasnodar", "Russia", 3));
            TeamsList.Add(new SoccerTeam("Atalanta", "Italy", 3));

            TeamsList.Add(new SoccerTeam("Lokomotiv Moskova", "Russia", 4));
            TeamsList.Add(new SoccerTeam("Marseille", "France", 4));
            TeamsList.Add(new SoccerTeam("Club Brugge", "Belgium", 4));
            TeamsList.Add(new SoccerTeam("Bor. Mönchengladbach", "Germany", 4));
            TeamsList.Add(new SoccerTeam("Galatasaray", "Turkey", 4));
            TeamsList.Add(new SoccerTeam("Midtjylland", "Denmark", 4));
            TeamsList.Add(new SoccerTeam("Rennes", "France", 4));
            TeamsList.Add(new SoccerTeam("Ferencváros", "Hungary", 4));

            //shuffle the teams to create a random starting point for bracket creation
            TeamsList.Shuffle();


            /* 
            foreach (SoccerTeam team in TeamsList)
            {
                string sName = Convert.ToString(team.name);
                string sPool = Convert.ToString(team.pool);
                Console.Write("\n" + sName.PadRight(25, ' ') + team.country.PadRight(15, ' ') + sPool);
            }  
            */                                

            //loop over the entire TeamsList and remove the respective teams according to the given constraints until each team is in a GroupsList
            while (TeamsList.Count != 0)
            {
                for (int l = 0; l < 8; l++)
                {
                    int stuckCheck = 0;
                    for (int i = 1; i < 5; i++)
                    {
                        if (stuckCheck == 16)
                        {
                            //starts the program over again if we encounter an unsolvable teams list according to the constraints
                            Console.Clear();
                            Run();
                        }

                        int ind = TeamsList.FindIndex(SoccerTeam => SoccerTeam.pool == i);
                        if (!GroupList[l].Exists(SoccerTeam => SoccerTeam.country == TeamsList[ind].country))
                        {
                            GroupList[l].Add(TeamsList[ind]);
                            TeamsList.RemoveAt(ind);
                        }
                        else
                        {
                            i--;
                            TeamsList.Shuffle();
                        }
                        stuckCheck++;
                    }
                    
                }

            }

            //write out the created group brackets
            for (int k = 0; k < 8; k++)
            {
                Console.WriteLine("\n*GROUP* " + (k + 1) + ":");
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("\n" + GroupList[k][j].name.PadRight(25, ' ') + GroupList[k][j].country.PadRight(10, ' ') + GroupList[k][j].pool);

                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("\nPress any key to simulate matches.");
            Console.ReadKey();
            Console.Clear();

            //create a new group of lists to see which teams advanced to the next stage
            List<SoccerTeam> ElimMatch1 = new List<SoccerTeam>();
            List<SoccerTeam> ElimMatch2 = new List<SoccerTeam>();
            List<SoccerTeam> ElimMatch3 = new List<SoccerTeam>();
            List<SoccerTeam> ElimMatch4 = new List<SoccerTeam>();
            List<SoccerTeam> ElimMatch5 = new List<SoccerTeam>();
            List<SoccerTeam> ElimMatch6 = new List<SoccerTeam>();
            List<SoccerTeam> ElimMatch7 = new List<SoccerTeam>();
            List<SoccerTeam> ElimMatch8 = new List<SoccerTeam>();
            var ElimMatch = new[] { ElimMatch1, ElimMatch2, ElimMatch3, ElimMatch4, ElimMatch5, ElimMatch6, ElimMatch7, ElimMatch8 };


            Random rndScore = new Random();

            //this is the driver code for simulating matches with random scores assigned to each team according to the constraints
            //the code records all its data in each team's SoccerTeam class
            //the 1st and 2nd place of each group gets put into a respective ElimMatch list for the Top 16 matches
            for (int i = 0; i < 8; i++)
            {                         
                for(int j = 0; j < 4; j++)
                {
                    for(int k = 0 ; k < 4; k++)
                    {
                        int homeGoals = rndScore.Next(0, 8);
                        int awayGoals = rndScore.Next(0, 8);

                        if (k == j)
                            continue;

                        if (homeGoals > awayGoals)
                        {
                            GroupList[i][j].goalsAgainst += homeGoals;
                            GroupList[i][j].goalsFor += awayGoals;
                            GroupList[i][j].points += 3;
                            GroupList[i][k].goalsAgainst += awayGoals;
                            GroupList[i][k].goalsFor += homeGoals;
                        }
                        else if (homeGoals == awayGoals)
                        {
                            GroupList[i][j].goalsAgainst += homeGoals;
                            GroupList[i][j].goalsFor += awayGoals;
                            GroupList[i][j].points += 1;
                            GroupList[i][k].goalsAgainst += awayGoals;
                            GroupList[i][k].goalsFor += homeGoals;
                            GroupList[i][k].points += 1;
                        }
                        else
                        {
                            GroupList[i][j].goalsAgainst += homeGoals;
                            GroupList[i][j].goalsFor += awayGoals;
                            GroupList[i][k].goalsAgainst += awayGoals;
                            GroupList[i][k].goalsFor += homeGoals;
                            GroupList[i][k].points += 3;
                        } 
                                                  
                    }   
                                                               
                }

                for(int j = 0; j < 4; j++)
                {
                    GroupList[i][j].goalsAverage = GroupList[i][j].goalsAgainst - GroupList[i][j].goalsFor;
                }

                GroupList[i] = GroupList[i].OrderByDescending(SoccerTeam => SoccerTeam.points).ToList();

                if (GroupList[i][0].points == GroupList[i][1].points && GroupList[i][1].points == GroupList[i][2].points)
                {
                    GroupList[i] = GroupList[i].OrderByDescending(SoccerTeam => SoccerTeam.goalsAverage).ToList();
                }

                else if (GroupList[i][1].points == GroupList[i][2].points)
                {
                    if (GroupList[i][1].goalsAverage > GroupList[i][2].goalsAverage)
                        ;
                    else
                        GroupList[i].Swap(1, 2);
                }                          
                
                else if (GroupList[i][0].points == GroupList[i][1].points)
                {
                    if (GroupList[i][0].goalsAverage > GroupList[i][1].goalsAverage)
                        ;
                    else
                        GroupList[i].Swap(0, 1);
                    if (GroupList[i][0].goalsAverage == GroupList[i][1].goalsAverage)
                    {
                        GroupList[i] = GroupList[i].OrderByDescending(SoccerTeam => SoccerTeam.goalsAgainst).ToList();
                    }
                }

                else
                {
                    int rnd = rndScore.Next(0, 1);
                    GroupList[i][rnd].tiebreak = 1;                    
                    GroupList[i] = GroupList[i].OrderByDescending(SoccerTeam => SoccerTeam.tiebreak).ToList();
                }
                
                    ElimMatch[i].Add(GroupList[i][0]);
                    ElimMatch[i].Add(GroupList[i][1]);
                
            }

            //print out the overall group score simulation
            Console.WriteLine("\nThe overall group scores");
            Console.WriteLine("\n" + "Name".PadRight(25, ' ') + "Group".PadRight(20, ' ') + "Points".PadRight(20, ' ') + "Goals Average".PadRight(20, ' ') + "Goals Scored");

            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("\n");
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("\n" + GroupList[i][j].name.PadRight(25, ' ') + (i+1).ToString().PadRight(20, ' ') + GroupList[i][j].points.ToString().PadRight(20, ' ') + GroupList[i][j].goalsAverage.ToString().PadRight(20, ' ') + GroupList[i][j].goalsAgainst.ToString());
                }
            }

            Console.WriteLine("\n\nPress any key to see top 16 standings.");
            Console.ReadKey();
            Console.Clear();

            //print out the standings after match simulations
            Console.WriteLine("\n The current standings for the top 16 are:");
            Console.WriteLine("\n" +"Name".PadRight(25, ' ') + "Group".PadRight(20, ' ') + "Points".PadRight(20, ' ') + "Goals Average".PadRight(20, ' ') + "Goals Scored");
            
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("\n");
                for(int j = 0; j < 2; j++)
                {
                    Console.WriteLine("\n" + ElimMatch[i][j].name.PadRight(25, ' ') + (i + 1).ToString().PadRight(20, ' ') + ElimMatch[i][j].points.ToString().PadRight(20, ' ') + ElimMatch[i][j].goalsAverage.ToString().PadRight(20, ' ') + ElimMatch[i][j].goalsAgainst.ToString());
                }
            }

            Console.WriteLine("\n\nPress any key to exit program.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            Run();
        }

    }
}
