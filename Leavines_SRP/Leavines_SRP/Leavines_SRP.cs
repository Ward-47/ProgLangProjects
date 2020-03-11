using System;
using System.Collections.Generic;

namespace Leavines_SRP
{
    class Leavines_SRP
    {
        public class Lexer
        {
            private string lex;
            private string tok;
            public Lexer(string lex, string token)
            {
                this.lex = lex;
                this.tok = token;
            }
            public string Lexe
            {
                get 
                { 
                return lex; 
                }

                set 
                { 
                lex = value; 
                }
            }
            public string TKN
            {
                get 
                { 
                return tok; 
                }

                set 
                { 
                tok = value; 
                }
            }
        }
        static void Main(string[] args)
        {
            Console.Write(">Enter your expression:\n");
            string input = Console.ReadLine();
            if (input.Length == 0)
            {
                Console.WriteLine("input cannot be null");
                throw new ArgumentException("input cannot be null");
            }

            Console.WriteLine(); // adds space to help make the program easy to read
            Console.WriteLine("----------------");
            Console.WriteLine("Calling Lexer");
            Console.WriteLine("----------------");
            List<Lexer> LxList = new List<Lexer>();
            List<string> id_LST = new List<string>();
            String all = "";

            // This checks to see if the string is empty. 
            int i = 0;
            while (i < input.Length)
            {
                if (Char.IsLetterOrDigit(input[i]))
                {
                    while (i < input.Length && Char.IsLetterOrDigit(input[i]) == true)
                    {
                        all += input[i];
                        i++;

                    }
                    LxList.Add(new Lexer(all, "id"));
                    id_LST.Add("id");
                    all = "";
                }

                else if (input[i] == '(')
                {
                    LxList.Add(new Lexer("(", "("));
                    id_LST.Add("(");
                    i++;
                }
                else if (input[i] == ')')
                {
                    LxList.Add(new Lexer(")", ")"));
                    id_LST.Add(")");
                    i++;
                }

                else if (input[i] == '*')
                {
                    LxList.Add(new Lexer("*", "*"));
                    id_LST.Add("*");
                    i++;
                }

                else if (input[i] == '+')
                {
                    LxList.Add(new Lexer("+", "+"));
                    id_LST.Add("+");
                    i++;
                }

                else if (input[i] == ' ')
                {
                    i++;
                }
                else
                {
                    all += input[i];
                    throw new ArgumentException(input[i] + "is not a vailid Input");

                }
            }


            // The $ symbol is added to the user's input. 
            id_LST.Add("$");

            // This foreach loop is what displays the chart. 
            foreach (var j in LxList)
            {
                Console.Write("{0}:{1}\n", j.Lexe, j.TKN);
            }
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine("Input Deriviation");
            Console.WriteLine("----------------");
            foreach (var j in id_LST)
            {
                Console.Write("{0}", j);
            }
            Console.WriteLine();



            /* This is for the action table where the positive represents
               the shift sequence and the reduce sequence is for the 
               negative including 100 which represents accept once the 
               parsing steps are finished. */
            int[,] Action = new int[12, 6];
            Action[0, 0] = 5; Action[0, 3] = 4;
            Action[1, 1] = 6; Action[1, 5] = 100;
            Action[2, 1] = -2; Action[2, 2] = 7; Action[2, 4] = -2; Action[2, 5] = -2;
            Action[3, 1] = -4; Action[3, 2] = -4; Action[3, 4] = -4; Action[3, 5] = -4;
            Action[4, 0] = 5; Action[4, 3] = 4;
            Action[5, 1] = -6; Action[5, 2] = -6; Action[5, 4] = -6; Action[5, 5] = -6;
            Action[6, 0] = 5; Action[6, 3] = 4;
            Action[7, 0] = 5; Action[7, 3] = 4;
            Action[8, 1] = 6; Action[8, 4] = 11;
            Action[9, 1] = -1; Action[9, 2] = 7; Action[9, 4] = -1; Action[9, 5] = -1;
            Action[10, 1] = -3; Action[10, 2] = -3; Action[10, 4] = -3; Action[10, 5] = -3;
            Action[11, 1] = -5; Action[11, 2] = -5; Action[11, 4] = -5; Action[11, 5] = -5;

            // This is for the Goto table that has the E, T, and F. 
            int[,] Goto = new int[12, 3];
            Goto[0, 0] = 1; Goto[0, 1] = 2; Goto[0, 2] = 3;
            Goto[4, 0] = 8; Goto[4, 1] = 2; Goto[4, 2] = 3;
            Goto[6, 1] = 9; Goto[6, 2] = 3; Goto[7, 2] = 10;
            int column = 0;
            int rc = 0;
            int state = 0;
            int PrevStat = 0;
            int find = 0;
            int go = 0;
            string ETF = null;

            /* this boolean is for the 
               right parenthesis to be 
               added after the removal */
            bool RitPar = false;

            /* this boolean checks to see 
            if left parenthesis was deleted */
            bool LP_Del = false;

            //lists keep track of what is happening
            List<String> stk = new List<String>();
            List<String> StkTrace = new List<String>();
            List<String> P_Steps = new List<String>();
            string total = "";
            stk.Add(state.ToString());
            StkTrace.Add(state.ToString());
            Console.WriteLine(); //adds space for presentation
            Console.WriteLine("----------------");
            Console.WriteLine("Parsing Steps");
            Console.WriteLine("----------------");


            /* while loop where the rows and columns 
            do not equal the accept value */
            while (Action[state, column] != 100)
            {
                if (id_LST[rc] == "id")
                {
                    column = 0;
                }
                else if (id_LST[rc] == "+")
                {
                    column = 1;
                }
                else if (id_LST[rc] == "*")
                {
                    column = 2;
                }
                else if (id_LST[rc] == "(")
                {
                    column = 3;
                }
                else if (id_LST[rc] == ")")
                {
                    column = 4;
                }
                else if (id_LST[rc] == "$")
                {
                    column = 5;
                }
                else
                {
                    break;
                }

                // if then else for the SHIFT sequence 
                if (Action[state, column] > 0)
                {
                    state = Action[state, column];
                    P_Steps.Add("S" + state);
                    stk.Add(id_LST[rc]);
                    stk.Add(state.ToString());
                    foreach (var j in stk)
                    {
                        total += j;
                    }
                    StkTrace.Add(total);
                    total = "";
                    rc++;
                }

                // if then else for the REDUCE sequence
                else if (Action[state, column] < 0)
                {
                    if (Action[state, column] == -1)
                    {
                        find = stk.LastIndexOf("E");
                        ETF = "E";
                        go = 0;
                    }

                    else if (Action[state, column] == -2)
                    {
                        find = stk.LastIndexOf("T");
                        ETF = "E";
                        go = 0;
                    }

                    else if (Action[state, column] == -3)
                    {
                        find = stk.LastIndexOf("T");
                        ETF = "T";
                        go = 1;
                    }

                    else if (Action[state, column] == -4)
                    {
                        find = stk.LastIndexOf("F");
                        ETF = "T";
                        go = 1;
                    }

                    else if (Action[state, column] == -5)
                    {
                        find = stk.IndexOf("(");
                        ETF = "F";
                        go = 2;

                        // states that the left parenthesis will be deleted
                        LP_Del = true;
                    }

                    else if (Action[state, column] == -6)
                    {
                        find = stk.IndexOf("id");
                        ETF = "F";
                        go = 2;
                    }

                    for (int r = stk.Count - 1; r > find - 1; r--)
                    {
                        /* checks to see if right parenthesis was deleted
                           and if left paren is being deleted */
                        if (stk[r] == ")" && LP_Del == false)
                        {
                            //if it was return true
                            RitPar = true;
                        }
                        stk.RemoveAt(r);

                    }
                    PrevStat = Int32.Parse(stk[find - 1]);
                    stk.Add(ETF);

                    //resets if left paren will be deleted or not
                    LP_Del = false;

                    //adds right parenthesis                    
                    if (RitPar == true)
                    {
                        stk.Add(")");
                        RitPar = false;

                    }
                    P_Steps.Add("R" + Math.Abs(Action[state, column]));
                    state = Goto[PrevStat, go];
                    stk.Add(state.ToString());
                    foreach (var j in stk)
                    {
                        total += j;
                    }
                    StkTrace.Add(total);
                    total = "";
                    if (Action[state, column] == 100)
                    {
                        P_Steps.Add("ACCEPT ");
                    }
                }
                else if (Action[state, column] == 0)
                {
                    P_Steps.Add("ERROR  ");
                    break;
                }


            }

            for (int a = 0; a < StkTrace.Count; a++)
            {

                Console.WriteLine(P_Steps[a]);
            }

            Console.WriteLine(); // adds space for presentation
            Console.WriteLine("----------------");
            Console.WriteLine("Stack");
            Console.WriteLine("----------------");
            for (int a = 0; a < StkTrace.Count; a++)
            {

                Console.WriteLine(StkTrace[a]);
            }
            Console.WriteLine();
            Console.ReadKey(true);
        }
    }
}



