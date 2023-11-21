using System;
namespace HelloWorld { 
class Gameboard { 
    int[,] board = new int[10,10];
    Ship[] ships;
    public int[,] Board {get{return board;}set{board = value;}}
    public Ship[] Ships {get{return ships;}set{ships = value;}}
    public Gameboard(){
        int len = board.Length;
        for (int i = 0;i<len;i++){
            for (int j = 0;j<len;j++){
                board[i,j] = 0;
            }   
        }
        ships[0] = new Ship(0,2);
        ships[1] = new Ship(0,3);
        ships[2] = new Ship(0,3);
        ships[3] = new Ship(0,4);
        ships[4] = new Ship(0,5);
    }
    // n is the length of the ship
    public void Place(int n){
        // x is the coordinate of the gameboard
        int x = Program.CheckInput(Console.ReadLine());
        // x is the coordinate of the gameboard
        int y = Program.CheckInput(Console.ReadLine());
        board = PlaceShip(n,x,y);
    }
    public bool ValidShip(int n, int x, int y){
        bool valid = false;
        int count = 0;
        for (int i = 0;i<n;i++){
            if (board[x,y+i] != 1 || board[x+i,y] != 1){
                count++;
            }
        }
        if (count == 5){
            valid = true;
        }
        return valid;
    }
    string PlaceDirection(){
        string d = "";
        bool success = false;
        while (success == false){
            Console.WriteLine("1:Vertical 2:Horizontal");
            d = Console.ReadLine();
            if (d == "1" || d == "2"){
                success = true;
            }else{
                Console.WriteLine("Wrong Direction");
            }
        }
        return d;
    }
    public int[,] PlaceShip(int n, int x, int y){
        Ship s = ships[n];
        int len = s.Length;
        if (len+y > 9 || len+x > 9 || ValidShip(n,x,y) == false){
            Console.WriteLine("Cannot put ship there");
            Console.WriteLine("Enter x,y again");
            Place(n);
        }else{
            if (PlaceDirection() == "1"){
                for (int i = 0;i<len;i++){
                    board[x+i,y] = 1;
                    s.X[i] = x+i;
                    s.Y[i] = y;
            }
            }
        }
        return board;
    }
    public void DisplayBoard(){
        int len = board.Length;
        for (int i = 0;i<len;i++){
            for (int j = 0;j<len;j++){
                Console.WriteLine($"{board[i,j]}".PadRight(10));
            }            
        }
    }
    public void DisplayStatus(){
        int len = ships.Length;
        for (int i = 0; i<len;i++){
            Console.WriteLine($"Ship{i}");
            Console.WriteLine($"Length{ships[i].Length}");
            Console.WriteLine($"Damaged:{ships[i].Damaged}");
            Console.WriteLine($"Sunk:{ships[i].Sunk}");
        }
    }
} 
} 
