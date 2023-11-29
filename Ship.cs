using System;
namespace HelloWorld { 
class Ship { 
    int damaged;
    int length;
    bool sunk;
    int[] x;
    int[] y;
    public int Damaged {get{return damaged;}set{damaged = value;}}
    public int Length {get{return length;}set{length = value;}}
    public bool Sunk {get{return sunk;}set{sunk = value;}}
    public int[] X {get{return x;}set{x = value;}}
    public int[] Y {get{return y;}set{y = value;}}
    public Ship(int l = 1, int d = 1){
        Damaged = d;
        Length = l;
        Sunk = false;
        X = new int[l];
        Y = new int[l];
    }
    public bool Checksunk(){
        if (damaged == length){
            sunk = true;
        }
        return sunk;
    }
    public bool CheckHit(int a, int b, Ship s){
        bool hit = false;
        int len = s.Length;
        for (int i = 0;i<len;i++){
            if (s.X[i] == a && s.Y[i] == b){
                s.Damaged++;
                hit = true;
            }
        }
        return hit;
    }
} 
}
