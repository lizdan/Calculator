using System;
using System.CodeDom;
using System.Collections;
using System.Windows.Forms;

/*
 * Calculator that can make serial calculations (+,-,* and /) with integers (maximum length of 8 digits).
 * 
 *  @author Liza Danielsson, lizadani101.
 * */

namespace Laboration2_Kalkylator_Lizdan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

            //Input-list to be reached from several methods. Booleans to keep track of when buttons are clicked.
        private ArrayList input = new ArrayList();
        private Boolean resultButtonClicked;
        private Boolean operandClicked;



            //10 following event methods calls for method that checks length of current number and either adds digit to
            //input-list or not.
        private void Btn1_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('1');
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('2');
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('3');
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('4');
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('5');
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('6');
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('7');
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('8');
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('9');
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            ifDigitIsClicked('0');
        }


            //4 following event methods calls for method to either add operand to input-list or not.
        private void BtnPlus_Click(object sender, EventArgs e)
        {
            ifOperandIsClicked('+');
        }

        private void BtnMinus_Click(object sender, EventArgs e)
        {
            ifOperandIsClicked('-');
        }

        private void BtnMultiply_Click(object sender, EventArgs e)
        {
            ifOperandIsClicked('x');
        }

        private void BtnDivide_Click(object sender, EventArgs e)
        {
            ifOperandIsClicked('/');
        }


            //When "C" i clicked. Clear input-list and display.
        private void BtnClear_Click(object sender, EventArgs e)
        {
            input.Clear();
            ResultTxBx.Clear();
        }



            //When "=" is clicked.
        private void BtnResult_Click(object sender, EventArgs e)
        {
            int result = 0;
                //Keep track of when the result button has been clicked.
            resultButtonClicked = true;

            //Check if input is still empty, then clicking "=" won't work. 
            if (input.Count > 0)
            {
                //Else, call for method that makes the calculations.
                result = ifResultIsClicked();

                //Print the result.
                ResultTxBx.Text = result.ToString();
                input.Clear();

                //Result to String and add the calculated value as a new number to input-list.
                String resultAsString = result.ToString();

                for (int i = 0; i < resultAsString.Length; i++)
                {
                    input.Add(resultAsString[i]);
                }
            }
        }


        /// <summary>
        /// Checks if the "=" button has been clicked and if so, changes the Boolean to false to be able to do a new serial 
        /// calculation. As long as an operand wasn't clicked right before and as long as input-list isn't empty,
        /// the operand is added to the input-list through another method. Sets another Boolean to true to keep track of when
        /// operand has been clicked.
        /// </summary>
        /// <param name="symbol"></param>

        private void ifOperandIsClicked(char symbol)
        {
            
            if (resultButtonClicked == true)
            {
                resultButtonClicked = false;
            }
            

            if ((!operandClicked) && (input.Count > 0))
            {
                buttonClicked(symbol);
            }

            operandClicked = true;
        }



        /// <summary>
        /// Called each time a digit is clicked. Checks if the "=" button was clicked right before, if not the method 
        /// calls for another method that control that the number so far isn't longer than 8 digits. If not, the digit is 
        /// added to the input-list through another method.
        /// </summary>
        /// <param name="digit">The digit as a char sent from each digit-button-event method when clicked.</param>
        private void ifDigitIsClicked(char digit)
        {
            Boolean lengthOK;
                
            operandClicked = false;

            CheckIfResultIsClicked();

            lengthOK = checkLengthOfNr();
            if (lengthOK)
            {
                buttonClicked(digit);
            }
        }


        /// <summary>
        /// Called each time a digit i clicked and the length of the number so far is allowed.
        /// Adds the digit clicked to the input-list and prints the digit, including all previous digits/operands
        /// saved in input-list, in the display.
        /// </summary>
        /// <param name="numberClicked">The digit passed from the event methods connected to the digit and operand buttons.</param>
        private void buttonClicked(char numberClicked)
        {
            ResultTxBx.Clear();

                //Add digit to input-list.
            input.Add(numberClicked);

                //Print all digits and operands in input-list.
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].Equals('+') ||
                    input[i].Equals('-') ||
                    input[i].Equals('x') ||
                    input[i].Equals('/'))
                {
                    ResultTxBx.Text += " " + input[i] + " ";
                }
                else
                {
                    ResultTxBx.Text += input[i];
                }  
            }
        }




        /// <summary>
        /// Loops through the input-list (as a String) each time a digit is clicked to check if the number the user enters...
        /// are longer than 8 digits. If so, a warning message is shown and no more digits are saved to the input-list.
        /// </summary>
        /// <returns>Boolean. True if the numbers is shorter than 8 digits and false if it's not. </returns>
        private Boolean checkLengthOfNr()
        {
            int lengthOfNr = 0;
            String inputAsString = "";

                //Add each object in input-list to a new String.
            foreach (Char obj in input)
            {
                inputAsString += obj.ToString();
            }
            
                //Loop throgh String, add 1 to "lengthOfNr" for each digit. Reset after each operand.
            for (int i = 0; i < inputAsString.Length; i++)
            {
                if ((inputAsString[i].Equals('+')) ||
                    (inputAsString[i].Equals('-')) ||
                    (inputAsString[i].Equals('x')) ||
                    (inputAsString[i].Equals('/')))
                {
                    lengthOfNr = 0;
                }
                else
                {
                    lengthOfNr++;
                }
            }

            if (lengthOfNr >= 8)
            {
                MessageBox.Show("The number can't be longer than 8 digits.", "Input Error", MessageBoxButtons.OK, 
                                 MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }




        /// <summary>
        /// Is called when "=" button is clicked. The method loops through the input-list, adds each object into a String,
        /// loops through the String and saves the numbers and the digits separately in two different arrays. 
        /// Last both arrays are looped through and calculations are made depending on the operands. 
        /// </summary>
        /// <returns>int newvalue: the calculated value from looping through all numbers and operands.</returns>
        private int ifResultIsClicked()
        {
            String inputAsString = "";

                //Variable to save digits after each other as a number.
            String oneValue = "";

                //Arrays to save numbers and operands separately.
            int[] allValues = new int[input.Count];
            char[] allOperands = new char[input.Count];

                //To save calculated values for the next calculation.
            int newValue = 0;


            foreach (Char obj in input)
            {
                inputAsString += obj.ToString();
            }


            int v = 0;
            int o = 0;
            for (int i = 0; i < inputAsString.Length; i++)
            {
                    //If operand appears in String.
                if ((inputAsString[i].Equals('+')) ||
                    (inputAsString[i].Equals('-')) ||
                    (inputAsString[i].Equals('x')) ||
                    (inputAsString[i].Equals('/')))
                {
                        //If operand is on last index, only add the previous number to number-array.
                    if(i == inputAsString.Length - 1)
                    {
                        allValues[v] = int.Parse(oneValue);
                    }
                    else
                    {
                        try
                        {
                                //Add one number to number-array.
                            allValues[v] = int.Parse(oneValue);
                                //Reset variable for one number after each operand.
                            oneValue = "";
                                //Add operand to operand-array.
                            allOperands[o] = inputAsString[i];
                                //Increase both array indexes with one.
                            o++;
                            v++;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                    //If last digit, only add digit to number and add number to number-array.
                else if (i == (inputAsString.Length - 1))
                {
                    oneValue += inputAsString[i];
                    allValues[v] = int.Parse(oneValue);
                }
                    //Else, add one digit to the number.
                else
                {
                    oneValue += inputAsString[i];
                }
            }


                //Loops through all numbers, checks the array with operands to see which calculation to make
                //to the value saved with the number right after. 
            int k = 0;
            for (int i = 0; i < allValues.Length; i++)
            {
                    //Start with first number as a value to be calculated with. Jump to next loop.
                if (newValue == 0)
                {
                    newValue = allValues[i];
                    continue;
                }
                if (allOperands[k].Equals('+'))
                {
                    newValue += allValues[i];
                }
                else if (allOperands[k].Equals('-'))
                {
                    newValue -= allValues[i];
                }
                else if (allOperands[k].Equals('x'))
                {
                    newValue = newValue * allValues[i];
                }
                else if (allOperands[k].Equals('/'))
                {
                    //Skips calculation with 0.
                    try
                    {
                        newValue = newValue / allValues[i];
                    }
                    catch (DivideByZeroException)
                    {
                        MessageBox.Show("Not possible to divide by zero. Please try again without any divisons by zero.", 
                                         "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 0;
                    }
                }
                k++;
            }
            return newValue;
        }



        /// <summary>
        /// Checks if the "=" button has been clicked. Method is called each time a digit i clicked to check if the digit clicked
        /// is the first one after the previous calculations result has been shown. If so, the input-list and display is reset.
        /// </summary>
        private void CheckIfResultIsClicked()
        {
            if (resultButtonClicked == true)
            {
                input.Clear();
                ResultTxBx.Text = "";
                resultButtonClicked = false;
            }
        }
    }
}