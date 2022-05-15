using System.Text;

class ReadFromFile
{
    static void Main()
    {
        
        Game game = Init();
        Console.WriteLine(game.answer);
        Boolean win = false;
        while(win == false)
        {
            Console.WriteLine("Guess a word!");
            Console.WriteLine(game.table);
            String guess = Console.ReadLine();
            if (guess.Count() == game.answer.Count())
            {
                if (Equals(guess, game.answer))
                {
                    Console.WriteLine("Congratz, youre right!");
                    win = true;
                }
                else
                {
                    game.table = Logic(guess, game.answer, game.table);
                }
            }
        }


        Console.WriteLine("Press any key to exit.");
        System.Console.ReadKey();
    }

    public static Game Init()
    {
        string[] lines = System.IO.File.ReadAllLines(@"C:\DEV\.NET\test\Wordle\words.txt");
        System.Console.WriteLine("Contents of WriteLines2.txt = ");
        Random rand = new Random();
        int r = rand.Next(0, lines.Count());

        Game game = new Game();
        game.answer = lines[r];
        //game.table = new string(new string(('_', lines[r].Count()) + ", " + new string(' ', lines[r].Count())));
        game.table = new string('_', lines[r].Count()) + ", ";
        return game;
    }
    public static string Logic(string guess, string answer, string table)
    {
        StringBuilder sb = new StringBuilder(table);
        Stack<Char> wrongPlace = new Stack<Char>();
        Stack<Char> rightPlace = new Stack<Char>();



        for (int i = 0; i < answer.Length; i++)
        {
            // Right Letter in right position
             if (Equals(answer[i], guess[i]))
            {
                sb[i] = guess[i];
                rightPlace.Push(guess[i]);
            }
            else
            {
                // Right Letter in wrong position
                for (int j = 0; j < answer.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    else if (Equals(answer[i], guess[j]))
                    {
                        wrongPlace.Push(guess[i]);
                    }
                }
            }
        }


        while (wrongPlace.Any())
        {
            char letter = wrongPlace.Pop();
            if (!rightPlace.Contains(letter))
            {
                sb.Append(letter);
            }
        }

        return sb.ToString();
    }
}



class Game
{
    public Guess guess;
    public string answer;
    public string table;

    public Game()
    {
        this.guess = new Guess();
        this.answer = "";
        this.table = "";
    }
}

class Guess
{
    public string rightPlaceRightLetter;
    public string wrongPlaceRightLetter;


    public Guess()
    {
        this.rightPlaceRightLetter = "";
        this.wrongPlaceRightLetter = "";
    }
}