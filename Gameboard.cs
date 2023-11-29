using System;
namespace HelloWorld { 
class Gameboard { 
    int[,] board = new int[10,10];
    Ship[] ships = new Ship[5];
    public int[,] Board {get{return board;}set{board = value;}}
    public Ship[] Ships {get{return ships;}set{ships = value;}}
    public Gameboard(){
        Ships[0] = new Ship(2,0);
        Ships[1] = new Ship(3,0);
        Ships[2] = new Ship(3,0);
        Ships[3] = new Ship(4,0);
        Ships[4] = new Ship(5,0);
    }
    // n is the length of the ship
    public void Place(int n){
        Console.WriteLine($"Ship{n}");
        Console.WriteLine("Hint: The x, y cooridinates are for the head of the ship");
        // x is the coordinate of the gameboard
        Console.Write($"Enter x:");
        int x = Program.CheckInput(Console.ReadLine());
        // y is the coordinate of the gameboard
        Console.Write($"Enter y:");
        int y = Program.CheckInput(Console.ReadLine());
        // d is the direction of the ship   
        Console.WriteLine("1:Vertical 2:Horizontal");
        Console.Write($"Enter direction:");
        string d = PlaceDirection(Console.ReadLine());
        if (ValidShip(n,x,y,d) == false){
            Console.WriteLine("Ship not Valid");
            Place(n);
        }else{
            Console.WriteLine("Ship Valid");
            board = PlaceShip(n,x,y,d);
        }
    }
    public bool ValidShip(int n, int x, int y,string d){
        int len = Ships[n].Length;
        bool valid = false;
        int count = 0;
        if (d == "1"){
            if (len+y <= 10){
                for (int i = 0;i<len;i++){
                    if (board[y+i,x] != 1){
                        count++;
                    }
                } 
            }
        }else{
            if (len+x <= 10){
                for (int i = 0;i<len;i++){
                    if (board[y,x+i] != 1){
                        count++;
                    }
                }
            }
        }
        if (count == len){
            valid = true;
        }
        return valid;
    }
    string PlaceDirection(string d){
        bool success = false;
        while (success == false){
            if (d == "1" || d == "2"){
                success = true;
            }else{
                Console.WriteLine("Wrong Direction");
                Console.WriteLine("1:Vertical 2:Horizontal");
                Console.Write($"Enter direction:");
                d = Console.ReadLine();
            }
        }
        return d;
    }
    public int[,] PlaceShip(int n, int x, int y,string d){
        Ship s = Ships[n];
        int len = s.Length;
        if (d == "1"){
            for (int i = 0;i<len;i++){
                board[y+i,x] = 1;
                s.X[i] = x;
                s.Y[i] = y+i;
            }
        }else{
            for (int i = 0;i<len;i++){
                board[y,x+i] = 1;
                s.X[i] = x+i;
                s.Y[i] = y;
        }
        }
        return board;
        }
    public void DisplayStatus(){
        int len = ships.Length;
        Console.Write($"Ship".PadRight(10));
        Console.Write($"Length".PadRight(10));
        Console.Write($"Damaged".PadRight(10));
        Console.Write($"Sunk".PadRight(10));
        Console.WriteLine("");
        for (int i=0;i<len;i++){
            Console.Write($"{i+1}".PadRight(10));
            Console.Write($"{Ships[i].Length}".PadRight(10));
            Console.Write($"{Ships[i].Damaged}".PadRight(10));
            Console.Write($"{Ships[i].Sunk}".PadRight(10));
            Console.Write("\n");
        }
    }
} 
} 
