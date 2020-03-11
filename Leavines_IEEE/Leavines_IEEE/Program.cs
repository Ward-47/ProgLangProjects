using System;


namespace Leavines_IEEE
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter your Decimal Floating Point: "); // Prompts user to input
            decimal x = decimal.Parse(Console.ReadLine()); // Gets user input
            int bit; // Sign bit is assigned 
            int mult = 0; // Multiplication is assigned
            int div = 0; // Division is assigned
            int exp = 0; // Exponent is assigned
            int Double_exp = 0; // Exponent for the Double Precision is assigned. 
            //decimal some = x; // NEW CODE 
            string Single_rem = ""; // String for remainderin Single Precision is declared. 
            string Double_rem = ""; // String for the remainder used in Double precision is declared
            string SingleBinaryReverse = ""; // String to reverse the binary to match the exact output in Single Precision is declared 
            string DoubleBinaryReverse = ""; // String to reverse the binary to match the exact output in Double Precision is declared

            //string StringReversed = "";

            /*-----------------------------------------*/
            /* The next line of code will go through the procedure on  on getting the output for
             * SINGLE PRECISION.*/
            /*----------------------------------------*/

            // If statement for when the input is greater then zero and less than one. 
            if (x > 0 && x < 1)
            {
                bit = 0;
                while (x < 1)
                {
                    x = x * 2;
                    mult++;
                }
                mult = mult * -1;
                exp = 127 + mult;
                //Console.WriteLine("exp for number between 0 and 1: " + exp);
                Double_exp = 1023 + mult;
                //Console.WriteLine("exp for double precision between 0 and 1: " + Double_exp);
                while (exp > 0)
                {
                    Single_rem = Single_rem + exp % 2;
                    exp = exp / 2;
                }
                Single_rem = Single_rem + "0";

                while(Double_exp > 0)
                {
                    Double_rem = Double_rem + exp % 2;
                    Double_exp = Double_exp / 2; 
                }
                Double_rem = Double_rem + "0"; 


            }

            // If statement if the user's input is greater than zero and greater than or equal to one. 
            else if (x > 0 && x >= 1)
            {
                bit = 0;
                while (x >= 2)
                {
                    x = x / 2;
                    div++;
                }
                exp = 127 + div;
                Double_exp = 1023 + div;
                while (exp > 0)
                {
                    Single_rem = Single_rem + exp % 2;
                    exp = exp / 2;
                }
                while (Double_exp > 0)
                {
                    Double_rem = Double_rem + Double_exp % 2;
                    Double_exp = Double_exp / 2;
                }
            }
            else
            {
                x = Math.Abs(x);
                bit = 1;
                while (x >= 2)
                {
                    x = x / 2;
                    div++;
                }
                exp = 127 + div;
                Double_exp = 1023 + div; 

                while (exp > 0)
                {
                    Single_rem = Single_rem + exp % 2;
                    exp = exp / 2;
                }

                while (Double_exp > 0)
                {
                    Double_rem = Double_rem + Double_exp % 2;
                    Double_exp = Double_exp / 2;
                }
            }

            // The string of binary for the remainders are reversed. 
            SingleBinaryReverse = StringReversed(Single_rem);

            // String for the Single Precision for the mantissa is declared
            string S_mantissa = "";

            // Integer for the Single Precision for the mantissa is declared. 
            int S_mantissaCount = 0; 

            // This part gets the decimal part of the decimal number inputted by the user. 
            double dec = (double)(x - Math.Truncate(x));

            //Console.WriteLine("decimal part of inputted number: " + dec);

            // Decimal part of the mantissa is multiplied by 2
            dec = dec * 2;
            
            // While loop that will display 23 decimal places for the mantissa as shown on the website.  
            while (S_mantissaCount < 23)
            {
                if (dec >= 1)
                {
                    S_mantissa = S_mantissa + "1";
                    dec = dec - Math.Truncate(dec);
                }
                else
                {
                    S_mantissa = S_mantissa + "0";
                }
                dec = dec * 2;
                S_mantissaCount++;
            }


            // Output for the Single Precision Results 
            Console.WriteLine("---------------------------");
            Console.WriteLine("Single Precision (32 bits):");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Bit 31 Sign Bit: " + bit);
            Console.WriteLine("Bits 30 - 23 Exponent Field: " + SingleBinaryReverse);
            Console.WriteLine("Bits 22 - 0 Significand: " + S_mantissa);


            /*-----------------------------------------*/
            /* The next line of code will go through the procedure on  on getting the output for
             * DOUBLE PRECISION.*/
            /*----------------------------------------*/

            // The string of binary for the remainders is reversed. 
            DoubleBinaryReverse = StringReversed(Double_rem);

            // This part gets the decimal part of the decimal number inputted by the user
            dec = (double)(x - Math.Truncate(x));

            // String for the Double Precision for mantissa is declared. 
            string Double_man = "";

            // integer for the mantissa for Double Precision is declared. 
            int Double_manCount = 0; 

            // Decimal part for the mantissa is multiplied by 2. 
            dec = dec * 2;

            // While loop that will display 52 decimal places as shown on the website.
            while (Double_manCount < 52)
            {
                if (dec >= 1)
                {
                    Double_man = Double_man + "1";
                    dec = dec - Math.Truncate(dec);
                }
                else
                {
                    Double_man = Double_man + "0";
                }
                dec = dec * 2;
                Double_manCount++; 
                
            }


            // Output for the Double Precision results. 
            Console.WriteLine("    ");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Double Precision (64 bits):");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Bit 63 Sign Bit: " + bit);
            Console.WriteLine("Bits 62 - 52 Exponent Field: " + DoubleBinaryReverse);
            Console.WriteLine("Bits 51 - 0 Significand: " + Double_man);

        }


        // Function to reverse the stings when outputted after user makes his/her input. 
        public static string StringReversed(string rem)
        {
            string BinaryReverse = "";
            for (int a = rem.Length - 1; a >= 0; a--){
                BinaryReverse = BinaryReverse + rem[a];
            }
            return BinaryReverse;
        }

    }
}



