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
    }
    public bool Shoot(Player pl, int x, int y){
        bool outcome = false;
        int len = pl.Gboard.Ships.Length;
        pl.Mboard[y,x] = 1;
        for (int i = 0; i<len;i++){
            if (pl.Gboard.Ships[i].CheckHit(x,y,pl.Gboard.Ships[i]) == true){
                pl.Gboard.Board[y,x] = pl.Gboard.Board[y,x] - 2;
                pl.Gboard.Ships[i].Checksunk();
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
    }
}
