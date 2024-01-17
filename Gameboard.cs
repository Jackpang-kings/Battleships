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
    public bool Place(int n){
        bool valid;
        Console.WriteLine($"Length:{Ships[n].Length}");
        Console.WriteLine("Hint: The x, y cooridinates are for the head of the ship");
        // x is the coordinate of the gameboard
        Console.Write($"Enter x:");
        int x = Program.CheckInput(Console.ReadLine()!);
        // y is the coordinate of the gameboard
        Console.Write($"Enter y:");
        int y = Program.CheckInput(Console.ReadLine()!);
        // d is the direction of the ship   
        Console.WriteLine("1:Vertical 2:Horizontal");
        Console.Write($"Enter direction:");
        string d = PlaceDirection(Console.ReadLine()!);
        valid = ValidShip(n,x,y,d);
        return valid;
    }
    public bool ValidShip(int n, int x, int y,string d){
        int len = Ships[n].Length;
        int count = 0;
        bool valid = false;
        if (d == "1" && len+y <= 10){
            for (int i = 0;i<len;i++){
                if (board[y+i,x] != 1){
                    count++;
                }
            } 
        }else if (d=="2"&&len+x <= 10){
            for (int i = 0;i<len;i++){
                if (board[y,x+i] != 1){
                    count++;
                }
            }
        }
        if (count == len){
            valid = true;
            PlaceShip(n,x,y,d);
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
                d = Console.ReadLine()!;
            }
        }
        return d;
    }
    public void PlaceShip(int n, int x, int y,string d){
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
        }
    public string DisplayStatus(){
        int len = ships.Length;
        string status = "";
        status+="Ship".PadRight(10);
        status+="Length".PadRight(10);
        status+="Damaged".PadRight(10);
        status+="Sunk\n".PadRight(10);
        for (int i=0;i<len;i++){
            status+=$"{i+1}".PadRight(10);
            status+=$"{Ships[i].Length}".PadRight(10);
            status+=$"{Ships[i].Damaged}".PadRight(10);
            status+=$"{Ships[i].Sunk}\n".PadRight(10);
        }
        return status;
    }
} 
} 
