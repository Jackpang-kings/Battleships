using System;
namespace HelloWorld { 
class Player { 
    Gameboard gboard;
    int[,] mboard = new int[10,10];
    string name;
    bool win;
    public string Name {get{return name;}set{name = value;}}
    public Gameboard Gboard {get{return gboard;}set{gboard = value;}}
    public int[,] Mboard {get{return mboard;}set{mboard = value;}}
    public bool Win {get{return win;}set{win = value;}}
    public Player(string n = "player"){
        Gboard = new Gameboard();
        Name = n;
        Win = false;
        int len = mboard.GetLength(0);
        for (int i = 0;i<len;i++){
            for (int j = 0;j<len;j++){
                mboard[i,j] = 0;
            } 
        }
    }
    public bool Shoot(Player player2, int x, int y){
        bool outcome = false;
        int len = player2.Gboard.Ships.Length;
        for (int i = 0; i<len;i++){
            if (player2.Gboard.Ships[i].CheckHit(x,y,player2.Gboard.Ships[i]) == true){
                player2.Gboard.Board[x,y]++;
                outcome = true;
            }
        }
        return outcome;
    }
    public void CheckWin(Player player1, Player player2){
        int counter = 0;
        for (int i = 0;i<5;i++){
            if (player2.Gboard.Ships[i].Checksunk() == true){
                counter++;
            }
        }
        if (counter == 5){
            player1.Win = true;
        }
    }
    public void MaskedBoard(int[,] mboard){
    int len = mboard.GetLength(0);
    for (int i = 0;i<len;i++){
        for (int j = 0;j<len;j++){
            Console.Write($"{mboard[i,j]}".PadRight(4));
        }  
        Console.WriteLine("\n");
    }
    }
} 
}
