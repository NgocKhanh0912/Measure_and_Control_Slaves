using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace GUI_Control_and_Display
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] Baudrate = { "1200", "2400", "3600", "4800", "9600", "115200" };
            cboBaudrate.Items.AddRange(Baudrate);
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboComPort.DataSource = SerialPort.GetPortNames();
            cboBaudrate.Text = "9600";
        }

        private void butConnect_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                butConnect.Text = "COM Port Connected";
                serCOM.PortName = cboComPort.Text;
                serCOM.BaudRate = Convert.ToInt32(cboBaudrate.Text);

                serCOM.Open();
            }
            else
            {
                butConnect.Text = "COM Port Disconnected";
                serCOM.Close();
            }
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butOnLED_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                MessageBox.Show("Serial COM hasn't been connected");
            }
            else
                serCOM.Write("@R2ON#");
        }

        private void butOffLED_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                MessageBox.Show("Serial COM hasn't been connected");
            }
            else
                serCOM.Write("@R2OF#");
        }

        private void btnSetDate_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                MessageBox.Show("Serial COM hasn't been connected");
            }
            else
            {
                string write_Date = "D" + txtSetDate.Text + "E";
                serCOM.Write(write_Date);
            }
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            if (!serCOM.IsOpen)
            {
                MessageBox.Show("Serial COM hasn't been connected");
            }
            else
            {
                string write_Time = "T" + txtSetTime.Text + "M";
                serCOM.Write(write_Time);
            }
        }

        private StringBuilder receivedData = new StringBuilder();

        private void serCOM_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                int startIndex = 0;
                int endIndex = 0;

                // Đọc dữ liệu từ SerialPort
                string newData = serCOM.ReadExisting();

                // Thêm dữ liệu vào bộ nhớ đệm
                receivedData.Append(newData); 

                // Xử lý chuỗi dữ liệu điều khiển LED được Respond từ STM32
                if (receivedData.ToString().Contains("@") && receivedData.ToString().Contains("#"))
                {
                    // Lấy chỉ số của ký tự @ đầu tiên
                    startIndex = receivedData.ToString().IndexOf("@");

                    // Lấy chỉ số của ký tự # cuối cùng
                    endIndex = receivedData.ToString().LastIndexOf("#");

                    // Trích xuất chuỗi dữ liệu từ vị trí @ đầu tiên đến vị trí # cuối cùng
                    string completeData = receivedData.ToString().Substring(startIndex, endIndex - startIndex + 1);

                    // Gửi dữ liệu đến UI trong một phương thức an toàn với thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        // Cập nhật TextBox với dữ liệu đầy đủ
                        txtLEDDataFrame.Text = completeData;

                        // Kiểm tra trạng thái LED và cập nhật vào TextBox mới
                        if (completeData.Contains("ON"))
                        {
                            txtLEDStatus.Text = "LED is ON";
                        }
                        else if (completeData.Contains("OF"))
                        {
                            txtLEDStatus.Text = "LED is OFF";
                        }
                        else
                        {
                            txtLEDStatus.Text = "Unknown";
                        }
                    });

                    // Xóa bộ nhớ đệm sau khi đã hiển thị
                    receivedData.Clear();
                }

                //  Xử lý chuỗi dữ liệu nhiệt độ và độ ẩm từ DHT11
                if (receivedData.ToString().Contains("T") && receivedData.ToString().Contains("H"))
                {

                    // Lấy chỉ số của ký tự T đầu tiên
                    startIndex = receivedData.ToString().IndexOf("T");

                    // Lấy chỉ số của ký tự H cuối cùng
                    endIndex = receivedData.ToString().LastIndexOf("H");

                    // Trích xuất các chuỗi dữ liệu
                    string DHT11_Data_Frame = receivedData.ToString().Substring(startIndex, endIndex - startIndex + 1);
                    string DHT11 = receivedData.ToString().Substring(startIndex + 1, endIndex - startIndex - 1);

                    // Tách giá trị dựa trên ký tự '&'
                    string[] parts = DHT11.Split('&');
                    string Temperature = parts[0];
                    string Humidity = parts[1];

                    // Gửi dữ liệu đến UI trong một phương thức an toàn với thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtDHT11DataFrame.Text = DHT11_Data_Frame;
                        txtTemp.Text = Temperature;
                        txtHu.Text = Humidity;
                        txtDHT11Status.Text = "No Error";
                    });

                    // Xóa bộ nhớ đệm sau khi đã xử lý
                    receivedData.Clear();
                }

                //  Xử lý chuỗi dữ liệu lỗi của DHT11
                if (receivedData.ToString().Contains("E") && receivedData.ToString().Contains("!"))
                {
                    // Lấy chỉ số của ký tự E đầu tiên
                    startIndex = receivedData.ToString().IndexOf("E");

                    // Lấy chỉ số của ký tự ! cuối cùng
                    endIndex = receivedData.ToString().LastIndexOf("!");

                    // Trích xuất chuỗi dữ liệu
                    string DHT11_Status = receivedData.ToString().Substring(startIndex, endIndex - startIndex + 1);

                    // Gửi dữ liệu đến UI trong một phương thức an toàn với thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (DHT11_Status.Contains("Respond Level 0"))
                        {
                            txtDHT11Status.Text = "Error: Respond Level 0 Timeout";
                        }
                        else if (DHT11_Status.Contains("Respond Level 0"))
                        {
                            txtDHT11Status.Text = "Error: Respond Level 1 Timeout";
                        }
                        else if (DHT11_Status.Contains("Data Level 0"))
                        {
                            txtDHT11Status.Text = "Error: Data Level 0 Timeout";
                        }
                        else
                        {
                            txtDHT11Status.Text = "Error: CRC Error";
                        }
                    });

                    // Xóa bộ nhớ đệm sau khi đã hiển thị
                    receivedData.Clear();
                }

                // Xử lý chuỗi dữ liệu giờ, phút, giây từ RTC DS3231
                if (receivedData.ToString().Contains("T") && receivedData.ToString().Contains("M"))
                {

                    // Lấy chỉ số của ký tự T đầu tiên
                    startIndex = receivedData.ToString().IndexOf("T");

                    // Lấy chỉ số của ký tự M cuối cùng
                    endIndex = receivedData.ToString().LastIndexOf("M");

                    // Trích xuất các chuỗi dữ liệu
                    string Time_Data_Frame = receivedData.ToString().Substring(startIndex + 1, endIndex - startIndex - 1);

                    // Gửi dữ liệu đến UI trong một phương thức an toàn với thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtTime.Text = Time_Data_Frame;
                    });

                    // Xóa bộ nhớ đệm sau khi đã xử lý
                    receivedData.Clear();
                }

                // Xử lý chuỗi dữ liệu thứ, ngày, tháng, năm từ RTC DS3231
                if (receivedData.ToString().Contains("D") && receivedData.ToString().Contains("E"))
                {

                    // Lấy chỉ số của ký tự D đầu tiên
                    startIndex = receivedData.ToString().IndexOf("D");

                    // Lấy chỉ số của ký tự E cuối cùng
                    endIndex = receivedData.ToString().LastIndexOf("E");

                    // Trích xuất các chuỗi dữ liệu
                    string Date_Data_Frame = receivedData.ToString().Substring(startIndex + 1, endIndex - startIndex - 1);

                    // Gửi dữ liệu đến UI trong một phương thức an toàn với thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtDate.Text = Date_Data_Frame;
                    });

                    // Xóa bộ nhớ đệm sau khi đã xử lý
                    receivedData.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while reading data from SerialPort: " + ex.Message);
            }
        }
    }
}
