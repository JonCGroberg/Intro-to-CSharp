using System;
using System.Windows.Forms;

namespace ___
{
    public partial class CountVowelsForm : Form
    {
        public CountVowelsForm()
        {
            InitializeComponent();
        }

        private void UserInputBox_TextChanged(object sender, EventArgs e)
        {   
            //convert to string just to be safe
            //then run the string through the vowel counting method to get an answer
            String userString = Convert.ToString(UserInputBox.Text);
            int vowelsCount = CountVowels(userString);

            //format output based on english grammer rules
            if (vowelsCount == 1)
            { 
               CounterLabel.Text = String.Format("{0} Vowel", vowelsCount);
            }
            else
            {
               CounterLabel.Text = String.Format("{0} Vowels", vowelsCount);
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private int CountVowels(string userString)
        {
            // loop through the string and count vowels 
            int vowelsCount = 0;
            for (int i = 0; i < userString.Length; i++)
            {
                char character = userString[i];
                if (character == 'a'|| character == 'e'|| character == 'i'|| character == 'o'|| character == 'u')
                {
                    vowelsCount++;
                }
            }
            return vowelsCount;
        }
    }
}
