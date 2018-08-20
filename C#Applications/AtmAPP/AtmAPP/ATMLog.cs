using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmAPP {
    class ATMLog {
        public string SourceURL { get; private set; }
        public string BankAccount { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string NumberOfDeposits { get; set; }

        private List<string> lines = new List<string>();
        public  List<Visitor> Visitors = new List<Visitor>();

        public ATMLog(string sourceURL) {
            SourceURL = sourceURL;
            ProcessLog();
        }

        private void ProcessLog() {
            string line;
            int index = 0;
            using (StreamReader file = new StreamReader(SourceURL)) {
                while ((line = file.ReadLine()) != null) {
                    lines.Add(line);
                }
                BankAccount = lines[0];
                StartTime = ConvertDate(lines[1]);
                EndTime = ConvertDate(lines[2]);
                NumberOfDeposits = lines[3];
            }
            foreach (string s in lines) {
                if (index > 3) {
                    Visitors.Add(new Visitor(s));
                }
                index++;
            }
        }
        
        private string ConvertDate(string line) {
            char[] temp = line.ToCharArray();
            string result = "";
            int count = 0;

            for (int i = 0; i < temp.Length; i++) {
                if (temp[i] != '/') {
                    result += temp[i];
                } else {
                    if (count == 2) {
                        result += ' ';
                    } else {
                        result += '-';
                        count++;
                    }
                }
            }
            return result;
        }
    }
}
