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

    public bool ValidShip(int n,string command){
        int len = Ships[n].Length;
        int count = 0;
        bool valid = false;
        string[] commands = command.Split(",");
        int x = Convert.ToInt32(commands[0]);
        int y = Convert.ToInt32(commands[1]);
        string d = commands[2];
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
        }
        return valid;
    }

    public void PlaceShip(int n, string command,bool valid){
        if (valid){
            string[] commands = command.Split(",");
            int x = Convert.ToInt32(commands[0]);
            int y = Convert.ToInt32(commands[1]);
            string d = commands[2];
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
    }

    public string DisplayStatus(){
        int len = ships.Length;
        string status = "";
        status+="Ship".PadRight(10)+"Length".PadRight(10)+"Damaged".PadRight(10)+"Sunk\n";
        for (int i=0;i<len;i++){
            status+=$"{i+1}".PadRight(10)+$"{Ships[i].Length}".PadRight(10)+$"{Ships[i].Damaged}".PadRight(10)+$"{Ships[i].Sunk}\n";
        }
        return status;
    }

} 
} 
