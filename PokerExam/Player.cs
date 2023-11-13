using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerExam
{
    class Player
    {
        public int Balance { get; set; }
        public Player(int balance) => this.Balance = balance;
    }
}
