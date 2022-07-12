//TemperatureComparatorForm.cs display an average of 5 inputed temperature and tells the user if the temperatures are accending or decending or neither
//accending is defined as: no temperature is lower than any previous one,
//decending is defined as: every temperature is lower than the previous one
//neither is defined as: neither accending or decending 
//
//please note that the defintion provided by the homework requires that [1,2,5,5,10] count as accending
//                                                                while [10,5,5,2,1] does not count as decending
//
//Jonathan Groberg 6/10/22

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


    public partial class TemperatureComparatorForm : Form
    {
        public int [] temperatures = new int[5];

        public TemperatureComparatorForm()
        {
            InitializeComponent();
        }

        //updates the text boxes and labels
        private void UpdateUI(ref TextBox textBox, ref Label label,int id)
        {
            string userInput = textBox.Text;
            int temperature = 0;

            if (IsValidTemp(userInput, ref temperature))
            {
                label.Text = "Temperature " + (id + 1); //we add 1 because array indexs start at 0
                label.ForeColor = Color.Black;
                textBox.ForeColor = Color.Black;
                averageTemperatureLabel.ForeColor = Color.Black;
                temperatures[id] = temperature;
                averageTemperatureLabel.Text = "Average Temperature : " + GetAverage(temperatures);

                // if neither a or b are true, we return "it's a mixed bag"
                if (noTempLower(temperatures)) mainMessageLabel.Text = "Getting warmer";
                else if (allDecending(temperatures)) mainMessageLabel.Text = "Getting cooler";
                else mainMessageLabel.Text="It's a mixed bag";
            }
            else
            {
                label.Text = "Invalid Input";
                label.ForeColor = Color.Red;
                averageTemperatureLabel.ForeColor = Color.Red;
                textBox.ForeColor = Color.Red;

                mainMessageLabel.Text = "Hmm... try fixing your input";
            }

        }

        //first checks to see if the string is an integer, if so, then sets the intial temperature variable.
        //returns if the resulting variable falls in the rnage of -30 to 130
        public bool IsValidTemp(string temperatureString, ref int temperatureInt)
        {
            return (int.TryParse(temperatureString, out temperatureInt) && temperatureInt >= -30 && temperatureInt <= 130);
        }

        //add up elements and divide by array length
        private int GetAverage (int[] array)
        {
            int total = 0;
            for (int i = 0; i < array.Length; i++)
            {
                total += array[i];
            }
            return total / array.Length;
        }

        //we cant start at index 0 because we need the previous index, so we start at 1
        //here we check that no temperature is lower than any previous one, if we find an exception we instantly return false
        //if we dont find an excpetion we return true
        private bool noTempLower(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[i - 1]) return false;
            }
            return true;
        }

        //If every temperature is lower than the previous one we return true
        //If we find one case of a higher temperature we return false instantly
        private bool allDecending(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] >= array[i - 1]) return false;
            }
            return true;
        }

        //call update function with respective textbox, id, and label
        private void temperature1TextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateUI(ref temperature1TextBox,ref temperature1Label,0);
        }

        private void temperature2TextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateUI(ref temperature2TextBox, ref temperature2Label, 1);
        }

        private void temperature3TextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateUI(ref temperature3TextBox,ref temperature3Label, 2);
        }

        private void temperature4TextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateUI(ref temperature4TextBox,ref temperature4Label, 3);
        }

        private void temperature5TextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateUI(ref temperature5TextBox,ref temperature5Label, 4);
        }

        private void closeProgramButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
