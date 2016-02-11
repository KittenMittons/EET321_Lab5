using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.NI4882;  // Included to talk to National Instruments machines.

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Global Variables *************************************************************************\\

        NationalInstruments.NI4882.Device NI = new NationalInstruments.NI4882.Device(0, 1); // Initialize function generator on GPIB address 1.
        NationalInstruments.NI4882.Device OSC = new NationalInstruments.NI4882.Device(0, 2); // Initialize oscilloscope on GPIB address 2.

        DataClasses1DataContext zybo = new DataClasses1DataContext();

        // Variables
        float freq;             // Float for true frequency calculation.
        float truefreq;         // Float for the true frequency result.
        string stringfreq;      // String for frequency.
        
        // Lists for SQL Data
        List<string> BoardID = new List<string>();
        List<string> Dataset = new List<string>();
        List<int> Systemclockfreq = new List<int>();
        List<int> Samplefreq = new List<int>();
        List<float> Measurement = new List<float>();
        //*******************************************************************************************\\

        // Button Clicks ****************************************************************************\\  

        private void button50MHz8KHz_Click(object sender, EventArgs e)
        {
            string choice = listBoxChoice.Text;     // Choice of listbox.
            int clock = 50;                         // System clock of 50MHz
            int samplefreq = 8;                     // Sample frequency of 8KHz.
            float count = 6250;                     // Count factor is 6250 for 8KHz and 50MHz. USE THIS FOR THE DIVISION OF THE FREQUENCY.

            NI.Write("SOUR:FREQ?");                 // Measure the frequency of the function generator.
            stringfreq = NI.ReadString();           // Read frequency of function generator and store as a string.
            freq = float.Parse(stringfreq);         // Convert string to float.
            truefreq = freq * count;                // Calculate true frequency of the board's oscillator.

            listswitch(choice, clock, samplefreq);  // Calls the listswitch function, and passes 3 parameters to it.
        }

        private void button50MHz16KHz_Click(object sender, EventArgs e)
        {
            string choice = listBoxChoice.Text;
            int clock = 50;
            int samplefreq = 16;
            float count = 3125;

            NI.Write("SOUR:FREQ?");
            stringfreq = NI.ReadString();
            freq = float.Parse(stringfreq);
            truefreq = freq * count;

            listswitch(choice, clock, samplefreq);
        }

        private void button125MHz8KHz_Click(object sender, EventArgs e)
        {
            string choice = listBoxChoice.Text;
            int clock = 125;
            int samplefreq = 8;
            float count = 15625;

            NI.Write("SOUR:FREQ?");
            stringfreq = NI.ReadString();
            freq = float.Parse(stringfreq);
            truefreq = freq * count;

            listswitch(choice, clock, samplefreq);
        }

        private void button125MHz16KHz_Click(object sender, EventArgs e)
        {
            string choice = listBoxChoice.Text;
            int clock = 125;
            int samplefreq = 16;
            float count = 7813;                

            NI.Write("SOUR:FREQ?");
            stringfreq = NI.ReadString();
            freq = float.Parse(stringfreq);
            truefreq = freq * count;

            listswitch(choice, clock, samplefreq);
        }

        private void buttonDirect50MHz_Click(object sender, EventArgs e)
        {
            string choice = listBoxChoice.Text;
            int clock = 50;
            int samplefreq = 0;

            OSC.Write(":MEAS:FREQ? CHAN1"); // Send the command to read the frequency of the oscilloscope.
            stringfreq = OSC.ReadString();
            truefreq = float.Parse(stringfreq);

            listswitch(choice, clock, samplefreq);
        }

        private void buttonDirect125MHz_Click(object sender, EventArgs e)
        {
            string choice = listBoxChoice.Text;
            int clock = 125;
            int samplefreq = 0;

            OSC.Write(":MEAS:FREQ? CHAN1"); // Send the command to read the frequency of the oscilloscope.
            stringfreq = OSC.ReadString();
            truefreq = float.Parse(stringfreq);

            listswitch(choice, clock, samplefreq);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            
            DataClasses1DataContext zybo = new DataClasses1DataContext();           

            // For loop that setps through lists and submits data to the SQL. Data is all stored
            // on lists that the positions correspond with the individual data. This makes it easy to step
            // through the lists with the same count to get the correct data together.
            for (int count = 0; count < Measurement.Count; count++)
            {
                EET321_Lab5_Table data = new EET321_Lab5_Table();   // Create new data variable for the SQL table

                data.DateTime = DateTime.Now;                       // Setting the date/time for the SQL field.
                data.GroupID = textBoxGroupID.Text;                 // Pulling GroupID from textbox for the SQL field.
                data.BoardID = BoardID[count];                      // Submitting board ID.
                data.SystemClockFreq = Systemclockfreq[count];      // Submitting system clock.
                data.SampleFrequency = Samplefreq[count];           // Submitting sample frequency.
                data.DataSet = Dataset[count];                      // Submitting data set.
                data.Measurement = Measurement[count];              // Submitting measurement.

                System.Threading.Thread.Sleep(1000);                // Wait for a second.

                zybo.EET321_Lab5_Tables.InsertOnSubmit(data);       
                zybo.SubmitChanges();                               // Submit to the SQL Server
            }

            // Clear Lists
            BoardID.Clear();
            Dataset.Clear();
            Systemclockfreq.Clear();
            Samplefreq.Clear();
            Measurement.Clear();
        }
        //*******************************************************************************************\\

        // Functions ********************************************************************************\\

        // This function checks what clock frequency and sample rate you are using, and what board 
        // is selected in the listbox. It then chooses the appropriate textbox to output the values 
        // to based on them. The first case is commented for reference. The only thing that changes
        // in the other cases are the specific textboxes that are being wrote to.
        void listswitch(string choice, int clock, int samplefreq)
        {
            switch (choice)
            {
                case "Board One":

                    // Is the clock 50MHz?
                    if (clock == 50)
                    {
                        // Is the sample frequency 8KHz?
                        if (samplefreq == 8)
                        { textBox508B1.Text = truefreq.ToString("F"); }    // Output truefreq to the 50MHz Clock, 8KHz Sample Frequency, Board One textbox.

                        // Is the sample frequency 16KHz?
                        else if (samplefreq == 16)
                        { textBox5016B1.Text = truefreq.ToString("F"); }   // Output truefreq to the 50MHz Clock, 16KHz Sample Frequency, Board One textbox.

                        // It must be a direct clock measurement.
                        else
                        { textBoxD50B1.Text = truefreq.ToString("F"); }    // Output truefreq to the Direct 50MHz textbox.
                    }


                    // The clock must be 125MHz.
                    else
                    {
                        // Is the sample frequency 8KHz?
                        if (samplefreq == 8)
                        { textBox1258B1.Text = truefreq.ToString("F"); }   // Output truefreq to the 125MHz Clock, 8KHz Sample Frequency, Board One textbox.

                        // Is the sample frequency 16KHz?
                        else if (samplefreq == 16)
                        { textBox12516B1.Text = truefreq.ToString("F"); }  // Output truefreq to the 125MHz Clock, 16KHz Sample Frequency, Board One textbox.

                        // It must be a direct clock measurement.
                        else
                        { textBoxD125B1.Text = truefreq.ToString("F"); }   // Output truefreq to the Direct 125MHz textbox.
                    }

                    // SQL data list additions.
                    BoardID.Add(textBoxB1Init.Text);    // Adding correct board ID from the board ID textbox onto the list.
                    Samplefreq.Add(samplefreq);         // Adding the sample frequency to the list.
                    if (samplefreq == 0)
                    {
                        Dataset.Add("Direct");          // If it is a direct measurement, add the Direct dataset
                    }
                    else
                        Dataset.Add("First");           // If it is not a direct measurement, add the correct dataset
                    Systemclockfreq.Add(clock);         // Add the correct system clock frequency to the list.
                    Measurement.Add(truefreq);          // Add the measurement to the list.
                    break;


                case "Board Two":
                    if (clock == 50)
                    {
                        if (samplefreq == 8)
                        { textBox508B2.Text = truefreq.ToString("F"); }

                        else if (samplefreq == 16)
                        { textBox5016B2.Text = truefreq.ToString("F"); }

                        else
                        { textBoxD50B2.Text = truefreq.ToString("F"); }
                    }

                    else
                    {
                        if (samplefreq == 8)
                        { textBox1258B2.Text = truefreq.ToString("F"); }

                        else if (samplefreq == 16)
                        { textBox12516B2.Text = truefreq.ToString("F"); }

                        else
                        { textBoxD125B2.Text = truefreq.ToString("F"); }
                    }

                    BoardID.Add(textBoxB2Init.Text);
                    Samplefreq.Add(samplefreq);
                    if (samplefreq == 0)
                    {
                        Dataset.Add("Direct");       
                    }
                    else
                        Dataset.Add("Second");           
                    Systemclockfreq.Add(clock);
                    Measurement.Add(truefreq);
                    break;


                case "Board One Check":
                    if (clock == 50)
                    {
                        if (samplefreq == 8)
                        { textBox508B1c.Text = truefreq.ToString("F"); }

                        else
                        { textBox5016B1c.Text = truefreq.ToString("F"); }

                    }

                    else
                    {
                        if (samplefreq == 8)
                        { textBox1258B1c.Text = truefreq.ToString("F"); }

                        else
                        { textBox12516B1c.Text = truefreq.ToString("F"); }
                    }

                    BoardID.Add(textBoxB1Init.Text);
                    Samplefreq.Add(samplefreq);
                    Dataset.Add("Check");
                    Systemclockfreq.Add(clock);
                    Measurement.Add(truefreq);
                    break;


                case "Board Two Check":
                    if (clock == 50)
                    {
                        if (samplefreq == 8)
                        { textBox508B2c.Text = truefreq.ToString("F"); }

                        else
                        { textBox5016B2c.Text = truefreq.ToString("F"); }

                    }

                    else
                    {
                        if (samplefreq == 8)
                        { textBox1258B2c.Text = truefreq.ToString("F"); }

                        else
                        { textBox12516B2c.Text = truefreq.ToString("F"); }
                    }

                    BoardID.Add(textBoxB2Init.Text);
                    Samplefreq.Add(samplefreq);
                    Dataset.Add("Check");
                    Systemclockfreq.Add(clock);
                    Measurement.Add(truefreq);
                    break;


                default:
                    break;
            }
        }
        //*******************************************************************************************\\ 
    }
}
