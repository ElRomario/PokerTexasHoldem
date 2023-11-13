using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;


namespace PokerExam
{
    
    enum Value
    {
        Two = 1,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    enum Suit
    {
        Spades = 1,
        Clubs,
        Hearts,
        Diamonds
    }
     struct Card
    {

        public Tuple<Suit, char, Value> SuitTuple;

        public Card(int value, int suitNumber)
        {
            
            switch(suitNumber)
            {
                case 1: SuitTuple = new Tuple<Suit, char, Value>(Suit.Hearts, '\u2665', (Value)value); break;
                case 2: SuitTuple = new Tuple<Suit, char, Value>(Suit.Diamonds, '\u2666', (Value)value); break;
                case 3: SuitTuple = new Tuple<Suit, char, Value>(Suit.Clubs, '\u2663', (Value)value); break;
                case 4: SuitTuple = new Tuple<Suit, char, Value>(Suit.Spades, '\u2660', (Value)value); break;
                default: throw new ArgumentException("Wrong suit number");
            }
        }
       
       

        public void PrintCard(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write($"|{SuitTuple.Item3,-5}  |");
            Console.SetCursorPosition(x, ++y);
            Console.Write($"|{SuitTuple.Item2,4}   |");
            Console.SetCursorPosition(x, ++y);
            Console.Write($"|  {SuitTuple.Item3,5}|");

        }
        Value GetValue()
        {
            return this.SuitTuple.Item3;
        }
        Suit GetSuit()
        {
            return this.SuitTuple.Item1;
        }
       

    }
}
