using System;
using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace PokerExam
{
    class Program
    {
        static int bet;
        static bool gameOver = false;

        static bool pairExist;
        static bool doublePairExist;
        static bool setExist;
        static bool straightExist;
        static bool flushExist;
        static bool fourOfAKindExist;
        static bool fullHouseExist;
        static bool straightFlushExist;
        static bool royalFlushExist;

        static void Main(string[] args)
        {
            StartScreen.ShowStartScreen();
            Setup();
        }
        
        static void Setup()
        {
            System.Console.Clear();
            Player player = new Player(100);

            const int tableCardsTotal = 5;
            const int handCardsTotal = 2;
            List<Card> allCards = new();
            List<Card> table = new();
            List<Card> hand = new();

            Game(allCards, table, hand, tableCardsTotal, handCardsTotal, player);
        }
        static void InitalizeAllCards(List<Card> allCards)
        {
            int suitsTotal = 4, valueTotal = 13;
            for (int i = 1; i < suitsTotal + 1; i++)
            {
                for (int j = 1; j < valueTotal + 1; j++)
                {
                    allCards.Add(new Card(j, i));
                }
            }
        }
        static void Game(List<Card> allCards, List<Card> table,
                        List<Card> hand, int tableCardsTotal,
                        int handCardsTotal, Player player)
        {

            while (!gameOver)
            {
                bet = 0;
                InitalizeAllCards(allCards);

                InitializeCards(handCardsTotal, hand);
                RemoveUsedCard(hand, allCards);
                Console.SetCursorPosition(14, 11);
                Console.Write("Your hand: ");
                PrintCardCollectionAtPosition(hand, 20, 10);

                AskForABet(player);
                Console.WriteLine($"Your bet: {bet}");

                InitializeCards(tableCardsTotal, table);
                RemoveUsedCard(table, allCards);
                PrintCardCollectionAtPosition(table, 0, 4);

                CombinationCheck(table, hand, player);

                System.Threading.Thread.Sleep(5000);
                Console.SetCursorPosition(30, 20);
                Console.Write("---PRESS ENTER TO CONTINUE--");
                Console.ReadLine();
                
                System.Console.Clear();
                DeleteCards(table);
                DeleteCards(hand);
                DeleteCards(allCards);

                gameOver = player.Balance <= 0 ? true : false;

            }


            //CombinationCheck(table, hand);

            //for(int i = 1; i < Suit.GetValues(typeof(Suit)).Length; i++)
            //{
            //    for(int j = 1; j < Value.GetValues(typeof(Value)).Length; j++)
            //    {
            //        allCards.Add(new Card(j, i));
            //    }
            //}


            //GiveHand()

            //AskForABet()
            //Console.WriteLine(allCards.Count);
            //GiveTable(combination);
            //RemoveUsedCard(combination, allCards);
            //Console.WriteLine(allCards.Count);             //CheckCombo()

        }
        static void DeleteCards(List<Card> cards)
        {
            cards.Clear();
        }

        static void CombinationCheck(List<Card> table, List<Card> hand, Player player)
        {
            List<Card> playedCards = new();
            playedCards.AddRange(table);
            playedCards.AddRange(hand);
            SortCardCollection(playedCards);
            var sameCards = playedCards
                                   .GroupBy(card => card.SuitTuple.Item3)
                                   .Select(group => new { CardValue = group.Key, Count = group.Count() });
            int sameCardsCounter = sameCards.Count();

            pairExist = PairCheck(playedCards, player);
            doublePairExist = DoublePairCheck(playedCards, player);
            setExist = SetCheck(playedCards, player);
            straightExist = StraightCheck(playedCards, player);
            flushExist = FlushCheck(playedCards, player);
            fourOfAKindExist = FourOfAKindCheck(playedCards, player);
            fullHouseExist = FullHouseCheck(playedCards, player);
            straightFlushExist = StraightFlushCheck(playedCards, player);
            royalFlushExist = RoyalFlushCheck(playedCards, player);


            Console.SetCursorPosition(5, 10);
            Console.WriteLine($"Same cards: { sameCardsCounter}");
            CombinationPrint();

            pairExist = false;
            setExist = false;
            doublePairExist = false;
            straightExist = false;
            flushExist = false;
            fourOfAKindExist = false;
            fullHouseExist = false;
            straightFlushExist = false;
            royalFlushExist = false;

        }

        static bool RoyalFlushCheck(List<Card> playedCards, Player player)
        {


            if (playedCards.Last().SuitTuple.Item3 - playedCards.First().SuitTuple.Item3 <= 4
            && playedCards.GroupBy(card => card.SuitTuple.Item3).Count() == playedCards.Count()
            && playedCards.First().SuitTuple.Item3 >= Value.Ten)
            {
                player.Balance += bet * 2;
                return true;

            }
            else
                return false;

        }
        static bool FullHouseCheck(List<Card> playedCards, Player player)
        {
            
            var group = playedCards.GroupBy(card => card.SuitTuple.Item3);
            if (group.Count() == 2 && group.First().Count() == 2 || group.First().Count() == 3)
            {
                player.Balance += bet * 2;
                return true;
            }
            else return false;
         

        }
        static bool FourOfAKindCheck(List<Card> playedCards, Player player)
        {   
            var groups = playedCards.GroupBy(card => card.SuitTuple.Item3);
            if (groups.Count() == 2 && groups.First().Count() == 1 || groups.Last().Count() == 4)
            {
                player.Balance += bet * 2;
                return true;
            }
            else return false;
            
        }
        static bool FlushCheck(List<Card> playedCards, Player player)
        {

            return playedCards.GroupBy(card => card.SuitTuple.Item1).Count() == 1;
        }
        static bool StraightCheck(List<Card> playedCards, Player player)
        {
            var groups = playedCards.OrderBy(card => card.SuitTuple.Item3);

            return groups.Last().SuitTuple.Item3 - groups.First().SuitTuple.Item3 <= 4 &&
             groups.GroupBy(card => card.SuitTuple.Item3).Count() == groups.Count();
        }

        static bool StraightFlushCheck(List<Card> playedCards, Player player)
        {
            if (StraightCheck(playedCards, player) && FlushCheck(playedCards, player))
            {
                player.Balance += bet * 2;
                return true;
            }
            else return false;
        }


        static bool SetCheck(List<Card> playedCards, Player player)
        {
            bool isSet = false;

            bool sameSuit = playedCards.Select(card => card.SuitTuple.Item1).Distinct().Count() == 1;
            bool diffrenetValues = playedCards.Select(card => card.SuitTuple.Item3).Distinct().Count() == 3;

            if (sameSuit && diffrenetValues)
            {
                player.Balance += bet * 2;
                isSet = true;
            }

            return isSet;
        }
        static bool DoublePairCheck(List<Card> playedCards, Player player)
        {
            var groups = playedCards.GroupBy(card => card.SuitTuple.Item3);
            if (groups.Count(group => group.Count() == 2) == 2)
            {
                player.Balance += bet * 2;
                return true;
            }
            else
                return false;
        }
        static bool PairCheck(List<Card> playedCards, Player player)
        {
            
            if (playedCards.GroupBy(card => card.SuitTuple.Item3).Count() < playedCards.Count())
            {
                player.Balance += bet * 2;
                return true;
            }
            else
                return false;
        }
        static void CombinationPrint()
        {


            int yGap = 15;
            if (pairExist)
            {
                Console.SetCursorPosition(40, yGap);
                Console.Write("You got pair!");
                yGap++;

            }

            if (doublePairExist)
            {
                Console.SetCursorPosition(40, yGap);
                Console.WriteLine("You got double pair!");
                yGap++;
            }
            if (setExist)
            {
                Console.SetCursorPosition(40, yGap);
                Console.WriteLine("You got set!");
                yGap++;
            }
            if (straightExist)
            {
                Console.SetCursorPosition(40, yGap);
                Console.WriteLine("You got straight!");
                yGap++;
            }
            if (flushExist)
            {
                Console.SetCursorPosition(40, yGap);
                Console.WriteLine("You got flush!");
                yGap++;
            }
            if (fourOfAKindExist)
            {
                Console.SetCursorPosition(40, yGap);
                Console.WriteLine("You got straight!");
                yGap++;
            }
            if (straightFlushExist)
            {
                Console.SetCursorPosition(40, yGap);
                Console.WriteLine("You got straight flush!");
                yGap++;
            }
            if (fullHouseExist)
            {
                Console.SetCursorPosition(40, yGap);
                Console.WriteLine("You got full house!");
                yGap++;
            }
            if (royalFlushExist)
            {
                    Console.SetCursorPosition(40, yGap);
                    Console.WriteLine("You got royal flush!");
                    yGap++;      
            }

        }

        static void PrintBetBar(Player player)
        {
            Console.SetCursorPosition(15, 20);
            Console.Write($"Your balance: {player.Balance}   ");
            Console.SetCursorPosition(38, 20);
            Console.Write("|  Make a bet  |");
            Console.SetCursorPosition(16, 21);
            Console.Write("-----------------------------------------------------------");
            Console.SetCursorPosition(21, 22);
            Console.Write("|Min|   |1/2 Pot|    |Max|    |Custom bet|     |Pass|");
            Console.SetCursorPosition(16, 23);
            Console.WriteLine("Enter: 1         2          3           4              5");
            Console.SetCursorPosition(16, 24);
        }
        static void AskForABet(Player player)
        {
            PrintBetBar(player);
            var input = Console.ReadLine();
            switch (input)
            {
                case "1": bet = 1; PrintBetBar(player); player.Balance--; return;
                case "2": bet = player.Balance / 2; player.Balance -= bet; PrintBetBar(player); return;
                case "3": bet = player.Balance; player.Balance = 0;PrintBetBar(player); return;
                case "4": Console.SetCursorPosition(10, 24);
                    Console.Write("Enter your bet:");
                    input =Console.ReadLine();bet = Int32.Parse(input); player.Balance -=Int32.Parse(input);  PrintBetBar(player); return;
                case "5": return;

                default:
                    Console.Write("Wrong input!");
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine("                    ");
                    AskForABet(player);
                    break;

            }

        }
    


        static void InitializeCards(int cardsCount, List<Card> cards)
        {
            Random random =new();
            for (int i = 0; i < cardsCount; i++)
            {
                Card newCard = new Card(random.Next(1, 14), random.Next(1, 5));
                cards.Add(newCard);
            }
        }
        static void GiveHand(List <Card> hand)
        {
            //hand = {
        }
        static void GiveTable(List<Card> cardCollection)
        {
            int xGap = 0, yGap = 4;
            foreach (Card element in cardCollection)
            {
                xGap += 15;
                element.PrintCard(xGap, yGap);
            }
        }
        static void RemoveUsedCard(List<Card> combination, List<Card> allCardsCollection)
        {
            allCardsCollection.RemoveAll(Card => combination.Contains(Card));
        }
        static void SortCardCollection(List <Card> cardColection)
        {
            //Item1 - Suit
            //Item2 - char (suit symbol)
            //Item3 - Value
            cardColection.Sort((x, y) =>
            {
                int valueComparasion = x.SuitTuple.Item3.CompareTo(y.SuitTuple.Item3);
                if (valueComparasion != 0)
                {
                    return valueComparasion;
                }
                return x.SuitTuple.Item1.CompareTo(y.SuitTuple.Item1);

            });
            
        }

       static void PrintCardCollectionAtPosition(List<Card> cardCollection, int xGap, int yGap)
        {
            foreach (Card element in cardCollection)
                element.PrintCard(xGap+=15, yGap);
        }
        static void GiveHand()
        {
                
        }
        static void GameOver()
        {

        }
    }
}

