using System;
using System.IO.Ports;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EMBEDDED_LAB4
{
    public partial class Form1 : Form
    {
        // Stores the time when ON/OFF command was sent
        private DateTime _commandStart;

        public Form1()
        {
            InitializeComponent();


            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(SerialPort.GetPortNames());

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;


            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.None;
            serialPort1.NewLine = "\n";


            serialPort1.DataReceived += SerialPort1_DataReceived;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        // COM PORT SELECTION
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Close current port before changing
                if (serialPort1.IsOpen)
                    serialPort1.Close();

                if (comboBox1.SelectedItem == null)
                    return;

                serialPort1.PortName =
                    comboBox1.SelectedItem.ToString();

                serialPort1.BaudRate = 9600;

                serialPort1.Open();

                this.Text =
                    $"Connected: {serialPort1.PortName} @ 9600";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Connection error:\n" + ex.Message
                );
            }
        }


        // ON BUTTON
        private void btnOn_Click_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Serial port is not connected.");
                    return;
                }

                // Record when command was sent
                _commandStart = DateTime.Now;

                // Send ON command
                serialPort1.Write("1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Send error:\n" + ex.Message
                );
            }
        }

        // OFF BUTTON
        private void btnOff_Click_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    MessageBox.Show("Serial port is not connected.");
                    return;
                }

                // Record when command was sent
                _commandStart = DateTime.Now;

                // Send OFF command
                serialPort1.Write("0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Send error:\n" + ex.Message
                );
            }
        }

        // RECEIVE ACK FROM ARDUINO

        private void SerialPort1_DataReceived(
            object sender,
            SerialDataReceivedEventArgs e)
        {
            try
            {
                string line =
                    serialPort1.ReadLine().Trim();

                if (line == "ACK")
                {
                    double ms =
                        (DateTime.Now - _commandStart)
                        .TotalMilliseconds;

                    BeginInvoke((MethodInvoker)(() =>
                    {
                        this.Text =
                            $"ACK received - Latency: {ms:F2} ms";
                    }));
                }
            }
            catch
            {
                // Ignore incomplete serial data
            }
        }


        // CLOSE SERIAL PORT SAFELY
        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();
            }
            catch
            {
            }

            base.OnFormClosing(e);
        }
    }
}