using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EMBEDDED_LAB3
{
    public partial class Form1 : Form
    {
        private int _pointIndex = 0;

        private Label labelLightValue;
        private Label labelMotionStatus;
        private Label labelConnectionStatus;

        public Form1()
        {
            InitializeComponent();

            SetupSerialPort();
            SetupCharts();
            SetupReadoutLabels();
            SetupBaudDropdown();
            RefreshPortList();

            button2.Enabled = false;
        }

        // =====================================================
        // BAUD DROPDOWN
        // =====================================================
        private void SetupBaudDropdown()
        {
            comboBox2.Items.Clear();

            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("115200");

            // Default for this test
            comboBox2.SelectedItem = "115200";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Selected baud is applied when CONNECT is clicked.
        }

        // =====================================================
        // LABELS
        // =====================================================
        private void SetupReadoutLabels()
        {
            labelLightValue = new Label
            {
                Location = new Point(
                    button1.Left,
                    button1.Bottom + 30
                ),

                AutoSize = true,

                Font = new Font(
                    Font.FontFamily,
                    10,
                    FontStyle.Bold
                ),

                Text = "Light: --"
            };

            Controls.Add(labelLightValue);

            labelMotionStatus = new Label
            {
                Location = new Point(
                    labelLightValue.Left,
                    labelLightValue.Bottom + 15
                ),

                AutoSize = true,

                Font = new Font(
                    Font.FontFamily,
                    10,
                    FontStyle.Bold
                ),

                Text = "Motion: --",

                ForeColor = Color.Black
            };

            Controls.Add(labelMotionStatus);

            labelConnectionStatus = new Label
            {
                Location = new Point(
                    labelMotionStatus.Left,
                    labelMotionStatus.Bottom + 15
                ),

                AutoSize = true,

                Font = new Font(
                    Font.FontFamily,
                    9,
                    FontStyle.Bold
                ),

                Text = "Status: Disconnected",

                ForeColor = Color.Red
            };

            Controls.Add(labelConnectionStatus);
        }

        // =====================================================
        // SERIAL PORT SETUP
        // =====================================================
        private void SetupSerialPort()
        {
            serialPort1.DataReceived -= SerialPort1_DataReceived;
            serialPort1.DataReceived += SerialPort1_DataReceived;

            serialPort1.NewLine = "\n";
            serialPort1.ReadTimeout = 2000;
        }

        // =====================================================
        // CHARTS
        // =====================================================
        private void SetupCharts()
        {
            // LIGHT CHART
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();

            ChartArea lightArea =
                new ChartArea("LightArea");

            lightArea.AxisY.Minimum = 0;
            lightArea.AxisY.Maximum = 100;

            lightArea.AxisY.Title = "Light (%)";
            lightArea.AxisX.Title = "Samples";

            chart1.ChartAreas.Add(lightArea);

            Series lightSeries =
                new Series("LIGHT");

            lightSeries.ChartType =
                SeriesChartType.Line;

            lightSeries.ChartArea =
                "LightArea";

            lightSeries.BorderWidth = 2;

            chart1.Series.Add(lightSeries);

            // MOTION CHART
            chart2.ChartAreas.Clear();
            chart2.Series.Clear();

            ChartArea motionArea =
                new ChartArea("MotionArea");

            motionArea.AxisY.Minimum = 0;
            motionArea.AxisY.Maximum = 100;

            motionArea.AxisY.Title = "Motion";
            motionArea.AxisX.Title = "Samples";

            chart2.ChartAreas.Add(motionArea);

            Series motionSeries =
                new Series("MOTION");

            motionSeries.ChartType =
                SeriesChartType.Line;

            motionSeries.ChartArea =
                "MotionArea";

            motionSeries.BorderWidth = 2;

            chart2.Series.Add(motionSeries);
        }

        // =====================================================
        // COM PORT LIST
        // =====================================================
        private void RefreshPortList()
        {
            comboBox1.Items.Clear();

            string[] ports =
                SerialPort.GetPortNames();

            comboBox1.Items.AddRange(ports);

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void PORT1_Click(object sender, EventArgs e)
        {
            RefreshPortList();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
        }

        // =====================================================
        // CONNECT BUTTON
        // =====================================================
        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                return;

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a COM port.");
                return;
            }

            try
            {
                serialPort1.PortName =
                    comboBox1.SelectedItem.ToString();

                // FIXED AT 115200
                serialPort1.BaudRate = 115200;

                serialPort1.DataBits = 8;
                serialPort1.Parity = Parity.None;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Handshake = Handshake.None;

                serialPort1.NewLine = "\n";
                serialPort1.ReadTimeout = 2000;

                serialPort1.Open();

                _pointIndex = 0;

                chart1.Series["LIGHT"]
                    .Points.Clear();

                chart2.Series["MOTION"]
                    .Points.Clear();

                labelLightValue.Text =
                    "Light: --";

                labelMotionStatus.Text =
                    "Motion: --";

                labelMotionStatus.ForeColor =
                    Color.Black;

                labelConnectionStatus.Text =
                    $"Status: Connected {serialPort1.PortName} @ 115200 baud";

                labelConnectionStatus.ForeColor =
                    Color.Green;

                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                labelConnectionStatus.Text =
                    "Status: Connection Error";

                labelConnectionStatus.ForeColor =
                    Color.Red;

                MessageBox.Show(
                    "Unable to connect.\n\n" + ex.Message,
                    "Serial Port Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // DISCONNECT BUTTON
        // =====================================================
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }

                labelConnectionStatus.Text =
                    "Status: Disconnected";

                labelConnectionStatus.ForeColor =
                    Color.Red;

                button1.Enabled = true;
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Disconnect error:\n" + ex.Message
                );
            }
        }

        // =====================================================
        // SERIAL DATA RECEIVED
        // =====================================================
        private void SerialPort1_DataReceived(
            object sender,
            SerialDataReceivedEventArgs e)
        {
            try
            {
                string line =
                    serialPort1.ReadLine().Trim();

                string[] parts =
                    line.Split(',');

                if (parts.Length != 2)
                    return;

                if (!int.TryParse(
                    parts[0],
                    out int light))
                {
                    return;
                }

                if (!int.TryParse(
                    parts[1],
                    out int motion))
                {
                    return;
                }

                if (light < 0 ||
                    light > 1023)
                {
                    return;
                }

                if (motion != 0 &&
                    motion != 1)
                {
                    return;
                }

                int lightScaled =
                    (int)(
                        (light / 1023.0)
                        * 100
                    );

                int motionScaled =
                    motion == 1
                    ? 100
                    : 0;

                BeginInvoke(
                    (MethodInvoker)(() =>
                    {
                        this.Text =
                            $"Received: {light},{motion}";

                        labelLightValue.Text =
                            $"Light: {light} / 1023";

                        if (motion == 1)
                        {
                            labelMotionStatus.Text =
                                "Motion: DETECTED";

                            labelMotionStatus.ForeColor =
                                Color.Red;
                        }
                        else
                        {
                            labelMotionStatus.Text =
                                "Motion: none";

                            labelMotionStatus.ForeColor =
                                Color.Black;
                        }

                        chart1.Series["LIGHT"]
                            .Points.AddXY(
                                _pointIndex,
                                lightScaled
                            );

                        chart2.Series["MOTION"]
                            .Points.AddXY(
                                _pointIndex,
                                motionScaled
                            );

                        _pointIndex++;

                        while (
                            chart1.Series["LIGHT"]
                                .Points.Count > 20)
                        {
                            chart1.Series["LIGHT"]
                                .Points.RemoveAt(0);
                        }

                        while (
                            chart2.Series["MOTION"]
                                .Points.Count > 20)
                        {
                            chart2.Series["MOTION"]
                                .Points.RemoveAt(0);
                        }
                    }));
            }
            catch (TimeoutException)
            {
            }
            catch (InvalidOperationException)
            {
            }
            catch
            {
            }
        }

        // =====================================================
        // CLOSE PORT WHEN FORM CLOSES
        // =====================================================
        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
            }
            catch
            {
            }

            base.OnFormClosing(e);
        }
    }
}