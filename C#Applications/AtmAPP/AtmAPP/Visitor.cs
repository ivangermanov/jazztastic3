using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmAPP {
    class Visitor {
        public int VisitorNo { get; private set; }
        public double Balance { get; private set; }

        public string[] Record { get; private set; }

        public Visitor(string line) {
            Record = new string[2];
            ProcessLine(line);
        }

        private void ProcessLine(string line) {
            char[] temp = line.ToCharArray();
            int index = 0;
            string[] info = new string[2];

            for (int i = 0; i < temp.Length; i++) {
                if (temp[i] != ' ') {
                    info[index] += temp[i];
                } else {
                    index++;
                }
            }
            VisitorNo = Convert.ToInt32(info[0]);
            Balance = Convert.ToDouble(info[1]);
            Record[0] = $"{VisitorNo}";
            Record[1] = $"{Balance}";
        }

        public void AddBalance(double balance) {
            Balance += balance;
        }
    }
}
