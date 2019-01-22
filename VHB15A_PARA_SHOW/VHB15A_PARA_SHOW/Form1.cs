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

namespace VHB15A_PARA_SHOW
{
    public partial class Form1 : Form
    {
        private static bool m_b_serialPortOpened = false;
        private static bool m_b_sound_enable = false;
        private string[] m_oldSerialPortNames;
        private static QueryDevice m_queryDevice = new QueryDevice();

        private const string SOUND_ENABLE = "SOUND ENABLE";
        private const string SOUND_PAUSE = "SOUND PAUSE";

        private const string MODE_NONINVASIVE = "Noninvasive";   //无创模式
        private const string MODE_INVASIVE = "Invasive";         //有创模式
        private const string PARA_RECEIVE = "PARA_RECEIVE";      //接收参数
        private const string PARA_SEND = "PARA_SEND";           //下发参数

        private static PARA_INFO m_current_info = new PARA_INFO();
        private struct PARA_INFO
        {
            public byte PARA_MODE;
            public byte PARA_PATIENT_TEMP_SETPOINT_0;
            public byte PARA_PATIENT_TEMP_SETPOINT_1;
            public byte PARA_CHAMBER_OUTLET_TEMP_SETPOINT_0;
            public byte PARA_CHAMBER_OUTLET_TEMP_SETPOINT_1;
            public byte PARA_IN_EXP;
            public byte PARA_HEATER_WIRE_MODE;
        }

        public void serial_write(byte[] buffer)
        {
            if (m_b_serialPortOpened)
            {
                this.serialPort1.Write(buffer, 0, Convert.ToInt32(buffer.Length));
            }
        }

        /*--------------------------------------------------
         * Author: Hancy Wang
         * DateTime:2019.1018
         * Function: This class control sending queries to device(STM32XXX)
        ---------------------------------------------------*/
        private class QueryDevice
        {
            public delegate void queryHandler(byte[] bts);
            public event queryHandler query_handler;

            //串口传输协议定义:  HEAD0+HEAD1,LEN,CMDTYPE,FRAME_ID,DATA,CHECKSUM0+CHECKSUM1
            public const int HEAD0 = 0;   //协议头2个字节,下标为0,1
            public const int HEAD1 = 1;
            public const int LEN = 2;     //frame长度,1个字节,下标为2
            public const int CMDTYPE = 3;  //1个字节,下标为3
            public const int FRAME_ID = 4;  //1个字节,下标为4....后面的为data+checksum(2个字节)

            public const int HEAD0_VALUE = 0xAA;
            public const int HEAD1_VALUE = 0x55;

            ////请求内容ID
            //public const int QUERY_ID_PATIENT_SIDE_TEMP = 0;
            //public const int QUERY_ID_HUMIDITY = 1;
            //public const int QUERY_ID_CHAMBER_OUTLET_TEMP = 2;
            //public const int QUERY_ID_RUNNING_TIME = 3;

            //CMDTYPE_ID
            public const byte CMDTYPE_ID_PC_2_DEVICE = 0x01;   //表示PC机发送到设备
            public const byte CMDTYPE_ID_DEVICE_2_PC = 0x00;   //表示设备发送到PC机

            //FRAME_ID query
            public const byte FRAME_ID_QUERY_PATIENT_SIDE_TEMP = 0x01;                  //请求获取患者端温度
            public const byte FRAME_ID_QUERY_HUMIDITY = 0x02;                            //请求获取患者端湿度
            public const byte FRAME_ID_QUERY_OUTLET_TEMP = 0x03;                         //请求呼气口温度
            public const byte FRAME_ID_QUERY_PARAMETERS = 0x04;                          //请求获取参数
            public const byte FRAME_ID_QUERY_SETTING_PARAMETERS = 0x05;                 //请求设置参数
            public const byte FRAME_ID_QUERY_ALARM_INFO = 0x06;                         //请求报警信息
            public const byte FRAME_ID_QUERY_PAUSE_ALARM_SOUND = 0x07;                   //请求暂停报警声音
            public const byte FRAME_ID_QUERY_RUNNING_TIME = 0x08;                       //请求获取时间
            public const byte FRAME_ID_QUERY_SETTING_SPECIAL_DISPLAY_MODE = 0x09;       //请求设置特殊显示模式

            //FRAME_LEN of query
            public const int FRAME_LEN_OF_QUERY_PATIENT_SIDE_TEMP = 7;
            public const int FRAME_LEN_OF_QUERY_HUMIDITY = 7;
            public const int FRAME_LEN_OF_QUERY_OUTLET_TEMP = 7;
            public const int FRAME_LEN_OF_QUERY_PARAMETERS = 7;
            public const int FRAME_LEN_OF_QUERY_SETTTING_PARAMETERS = 14;  
            public const int FRAME_LEN_OF_QUERY_ALARM_INFO = 7;
            public const int FRAME_LEN_OF_QUERY_PAUSE_ALARM_SOUND = 8;
            public const int FRAME_LEN_OF_QUERY_RUNNING_TIME = 7;
            public const int FRAME_LEN_OF_QUERY_SETTING_SPECIAL_DISPLAY_MODE = 8;

            //FRAME_ID_GET receive
            public const byte FRAME_ID_RECEIVE_PATIENT_SIDE_TEMP = 0x81;                //接收患者端温度
            public const byte FRAME_ID_RECEIVE_HUMIDITY = 0x82;                         //接收患者端湿度
            public const byte FRAME_ID_RECEIVE_OUTLET_TEMP = 0x83;                      //接收出气口温度
            public const byte FRAME_ID_RECEIVE_PARAMETERS = 0x84;                       //接收参数
            public const byte FRAME_ID_RECEIVE_ALARM_INFO = 0x86;                       //接收报警信息
            public const byte FRAME_ID_RECEIVE_RUNNING_TIME = 0x88;                     //接收运行时间

            //FRAM_LEN of receive
            public const int FRAME_LEN_OF_RECEIVE_PATIENT_SIDE_TEMP = 9;
            public const int FRAME_LEN_OF_RECEIVE_HUMIDITY = 9;
            public const int FRAME_LEN_OF_RECEIVE_OUTLET_TEMP = 9;
            public const int FRAME_LEN_OF_RECEIVE_PARAMETERS = 14;   
            public const int FRAME_LEN_OF_RECEIVE_ALARM_INFO = 9;
            public const int FRAME_LEN_OF_RECEIVE_RUNNING_TIME = 10;

            //用来保存当前的请求内容编号,采用轮询的方式200ms发送一个请求
            public static byte m_query_queue_No = 0x04;  //循环query 0,1,2,3..0,1,2,3...  
            public static byte m_prev_query_queue_No = 0x01;
            

            //用来保存串口发送或接收的数据
            public static List<byte> m_buffer = new List<byte>();

            public static void query_by_ID(int ID)
            {
                if (!m_b_serialPortOpened)
                {
                    return;
                }

                byte[] buffer = null;
                switch (ID)
                {
                    case FRAME_ID_QUERY_PATIENT_SIDE_TEMP:                                          //请求患者端温度
                        buffer = new byte[FRAME_LEN_OF_QUERY_PATIENT_SIDE_TEMP];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_PATIENT_SIDE_TEMP;
                        break;
                    case FRAME_ID_QUERY_HUMIDITY:                                                     //请求患者端湿度
                        buffer = new byte[FRAME_LEN_OF_QUERY_HUMIDITY];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_HUMIDITY;
                        break;
                    case FRAME_ID_QUERY_OUTLET_TEMP:                                                  //请求出气口温度
                        buffer = new byte[FRAME_LEN_OF_QUERY_OUTLET_TEMP];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_OUTLET_TEMP;
                        break;
                    case FRAME_ID_QUERY_PARAMETERS:                                                   //请求参数
                        buffer = new byte[FRAME_LEN_OF_QUERY_PARAMETERS];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_PARAMETERS;
                        break;
                    case FRAME_ID_QUERY_SETTING_PARAMETERS:                                          //请求设置参数
                        buffer = new byte[FRAME_LEN_OF_QUERY_SETTTING_PARAMETERS];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_SETTING_PARAMETERS;
                        buffer[QueryDevice.FRAME_ID + 1] = m_current_info.PARA_MODE;
                        buffer[QueryDevice.FRAME_ID + 2] = m_current_info.PARA_PATIENT_TEMP_SETPOINT_0;
                        buffer[QueryDevice.FRAME_ID + 3] = m_current_info.PARA_PATIENT_TEMP_SETPOINT_1;
                        buffer[QueryDevice.FRAME_ID + 4] = m_current_info.PARA_CHAMBER_OUTLET_TEMP_SETPOINT_0;
                        buffer[QueryDevice.FRAME_ID + 5] = m_current_info.PARA_CHAMBER_OUTLET_TEMP_SETPOINT_1;
                        buffer[QueryDevice.FRAME_ID + 6] = m_current_info.PARA_IN_EXP;
                        buffer[QueryDevice.FRAME_ID + 7] = m_current_info.PARA_HEATER_WIRE_MODE;
                        //SendParameters();
                        break;
                    case FRAME_ID_QUERY_ALARM_INFO:                                                   //请求报警信息
                        buffer = new byte[FRAME_LEN_OF_QUERY_ALARM_INFO];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_ALARM_INFO;
                        break;
                    case FRAME_ID_QUERY_PAUSE_ALARM_SOUND:                                              //请求暂停报警声音
                        buffer = new byte[FRAME_LEN_OF_QUERY_PAUSE_ALARM_SOUND];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_PAUSE_ALARM_SOUND;
                        buffer[QueryDevice.FRAME_ID+1] = m_b_sound_enable==true?Convert.ToByte(0x00):Convert.ToByte(0x01);  //0x00-打开声音；0x01-暂停声音
                        break;
                    case FRAME_ID_QUERY_RUNNING_TIME:                                                   //请求运行时间
                        buffer = new byte[FRAME_LEN_OF_QUERY_RUNNING_TIME];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_RUNNING_TIME;
                        break;
                    case FRAME_ID_QUERY_SETTING_SPECIAL_DISPLAY_MODE:                                   //请求设置特殊显示模式
                        buffer = new byte[FRAME_LEN_OF_QUERY_SETTING_SPECIAL_DISPLAY_MODE];
                        buffer[QueryDevice.FRAME_ID] = QueryDevice.FRAME_ID_QUERY_SETTING_SPECIAL_DISPLAY_MODE;
                        break;
                    default:
                        break;
                }

                buffer[QueryDevice.HEAD0] = QueryDevice.HEAD0_VALUE;
                buffer[QueryDevice.HEAD1] = QueryDevice.HEAD1_VALUE;

                buffer[QueryDevice.LEN] = Convert.ToByte(buffer.Length - 2);  //长度不包含头,所以去掉头的2个字节
                buffer[QueryDevice.CMDTYPE] = QueryDevice.CMDTYPE_ID_PC_2_DEVICE;

                //计算checksum
                int sum = 0;
                for (int i = LEN; i < Convert.ToInt32(buffer.Length); i++)
                {
                    sum += buffer[i];
                }
                buffer[Convert.ToInt32(buffer[QueryDevice.LEN])] = Convert.ToByte(sum / 256);   //checksum1
                buffer[Convert.ToInt32(buffer[QueryDevice.LEN]) + 1] = Convert.ToByte(sum % 256); //checksum2

                m_queryDevice.query_handler(buffer);          //运行函数,将参数buffer传给serial_write,并运行
            }
        }

        private enum PARA_ENUM
        {
            PARA_ENUM_MODE,
            PARA_ENUM_PATIENT_TEMP_SETPOINT,
            PARA_ENUM_OULET_TEMP_SETPOINT,
            PARA_ENUM_IN_EXP,
            PARA_HEATER_WIRE_MODE
        }

        private bool ConvertParameters2PARAINFO(ref PARA_INFO para_info)
        {
            PARA_INFO res = new PARA_INFO();

            //如果有combox的内容为空，弹出提示
            if (this.comboBox_para_mode.Text == "")
            {
                MessageBox.Show("Mode is emtpy");
                return false;
            }
            if (this.comboBox_para_patient_side_temp_setpoint.Text == "")
            {
                MessageBox.Show("Patient side temp setpoint is empty");
                return false;
            }
            if (this.comboBox_para_chamber_outlet_temp_setpoint.Text == "")
            {
                MessageBox.Show("Chamber outlet temp setpoint is emtpy");
                return false;
            }
            if (this.comboBox_para_in_exp.Text == "")
            {
                MessageBox.Show("IN/EXP is empty");
                return false;
            }
            if (this.comboBox_heater_wire_mode.Text == "")
            {
                MessageBox.Show("Heater wire mode is emtpy");
                return false;
            }

            //先获取患者端温度和出气口温度，然后做判断
            string tmp = this.comboBox_para_patient_side_temp_setpoint.Text;
            UInt16 patient_temp_setpoint = Convert.ToUInt16((Convert.ToUInt16(Convert.ToUInt16(tmp.Substring(0, tmp.Length - tmp.IndexOf('.'))) * 10)) 
                                                                                         + (Convert.ToUInt16(tmp.Substring(tmp.IndexOf(".") + 1, 1))));
           

            tmp = this.comboBox_para_chamber_outlet_temp_setpoint.Text;
            UInt16 outlet_temp_setpoint = Convert.ToUInt16((Convert.ToUInt16(Convert.ToUInt16(tmp.Substring(0, tmp.Length - tmp.IndexOf('.'))) * 10)) 
                                                                                          + (Convert.ToUInt16(tmp.Substring(tmp.IndexOf(".")+1, 1))));


            //模式，无创还是有创
            if (this.comboBox_para_mode.Text == MODE_NONINVASIVE)
            {
                res.PARA_MODE = 0x00;

                //对患者端温度的设置和出气口温度的设置进行判断
                if (patient_temp_setpoint < 310 || patient_temp_setpoint > 370)                         //检测患者端温度的设置
                {
                    MessageBox.Show("Patient side temp. setpoint should in the range [31.0,37.0]");
                    return false;
                }

                if (outlet_temp_setpoint < 300 || outlet_temp_setpoint > 320)                           //检测出气口温度的设置
                {
                    MessageBox.Show("Patient outlet temp. setpoint should in the range [30.0,32.0]");
                    return false;
                }

                if (outlet_temp_setpoint < patient_temp_setpoint - 40 || outlet_temp_setpoint > patient_temp_setpoint + 30)  //检测 Tp-40 <= Tc <= Tp+30
                {
                    MessageBox.Show("Tp: Patient side Temp. setpoint\n\rTc: Chamber outlet Temp. setpoint\n\rRange: Tp -4 <= Tc <=Tp+ 3 ", "Out of range");
                    return false;
                }
            }
            else if (this.comboBox_para_mode.Text == MODE_INVASIVE)
            {
                res.PARA_MODE = 0x01;

                //对患者端温度的设置和出气口温度的设置进行判断
                if (patient_temp_setpoint < 350 || patient_temp_setpoint > 400)
                {
                    MessageBox.Show("Patient side temp. setpoint should in the range [35.0,40.0]");
                    return false;
                }

                if (outlet_temp_setpoint < 340 || outlet_temp_setpoint > 430)
                {
                    MessageBox.Show("Patient outlet temp. setpoint should in the range [34.0,43.0]");
                    return false;
                }

                if (outlet_temp_setpoint < patient_temp_setpoint - 40 || outlet_temp_setpoint > patient_temp_setpoint + 30)
                {
                    MessageBox.Show("Tp: Patient side Temp. setpoint\n\rTc: Chamber outlet Temp. setpoint\n\rRange: Tp -4 <= Tc <=Tp+ 3 ", "Out of range");
                    return false;
                }
            }
            else
            {   
                //容错处理，如果模式没有设置，弹出提示
                MessageBox.Show("Wrong mode");
                return false;
            }

            //病人温度设置
            res.PARA_PATIENT_TEMP_SETPOINT_0 = Convert.ToByte(patient_temp_setpoint % 256);
            res.PARA_PATIENT_TEMP_SETPOINT_1 = Convert.ToByte(patient_temp_setpoint / 256);

            //出气口温度
            res.PARA_CHAMBER_OUTLET_TEMP_SETPOINT_0 = Convert.ToByte(outlet_temp_setpoint % 256);
            res.PARA_CHAMBER_OUTLET_TEMP_SETPOINT_1 = Convert.ToByte(outlet_temp_setpoint / 256);

            // IN/EXP
            if (this.comboBox_para_in_exp.Text == "1:1.0")
            {
                res.PARA_IN_EXP = 0x01;
            }
            else if (this.comboBox_para_in_exp.Text == "1:1.1")
            {
                res.PARA_IN_EXP = 0x02;
            }
            else if (this.comboBox_para_in_exp.Text == "1:1.2")
            {
                res.PARA_IN_EXP = 0x03;
            }
            else if (this.comboBox_para_in_exp.Text == "1:1.3")
            {
                res.PARA_IN_EXP = 0x04;
            }
            else if (this.comboBox_para_in_exp.Text == "1:1.4")
            {
                res.PARA_IN_EXP = 0x05;
            }
            else if (this.comboBox_para_in_exp.Text == "1:1.5")
            {
                res.PARA_IN_EXP = 0x06;
            }
            else
            {
                //do nothing
            }

            //heater wire mode
            if (this.comboBox_heater_wire_mode.Text == "Double Heater Wire")
            {
                res.PARA_HEATER_WIRE_MODE = 0x01;
            }
            else if (this.comboBox_heater_wire_mode.Text == "Insp Only")
            {
                res.PARA_HEATER_WIRE_MODE = 0x02;
            }
            else if (this.comboBox_heater_wire_mode.Text == "None")
            {
                res.PARA_HEATER_WIRE_MODE = 0x03;
            }
            para_info = res;
            return true;
        }
  

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitApp();
        }

        private void InitApp()
        {
            //初始化串口设置
            InitSerialPort();

            //加载图片
            LoadPicture();

            //初始化QueryDevice类
            InitQueryDevice();

            //初始化报警声音相关控件
            show_alarm_sound_status(false);

            //初始化参数的comboBox
            InitParameters();
        }

        private void InitParameters()
        {
            this.comboBox_para_mode.Text = @"";
            this.comboBox_para_patient_side_temp_setpoint.Text = @"";
            this.comboBox_para_chamber_outlet_temp_setpoint.Text = @"";
            this.comboBox_para_in_exp.Text = @"";
            this.comboBox_heater_wire_mode.Text = @"";
        }

        //将app上的参数下发到下位机上
        private void SendParameters()
        {

        }

        private void InitQueryDevice()
        {
            //注册要运行的函数;
            m_queryDevice.query_handler += serial_write;
        }

        private void InitSerialPort()
        {
            string[] str_portNames=SerialPort.GetPortNames();  //获取电脑的serial port
            if (str_portNames.Length != 0)  //如果有serial port
            {
                Array.Sort(str_portNames, (a, b) => Convert.ToInt32(((string)a).Substring(3)).CompareTo(Convert.ToInt32(((string)b).Substring(3)))); //闭包，类似Lamda表达式
                m_oldSerialPortNames = str_portNames;
                this.comboBox_serial_port_name.Items.AddRange(str_portNames);
                this.comboBox_serial_port_name.SelectedIndex = 0;
            }

            this.comboBox_serial_port_baut_rate.Text = "19200";
            this.comboBox_serial_port_data_bits.Text = "8";
            this.comboBox_serial_port_stop_bits.Text = "1";
            this.comboBox_serial_port_parity.Text = "None";
            this.comboBox_serial_port_flow_control.Text = "None";
        }

        private void LoadPicture()
        {
            if (!m_b_serialPortOpened)
            {
                this.pictureBox_serial_port_connecting.Load(Environment.CurrentDirectory + @"\" + "red.png");
            }
            else
            {
                this.pictureBox_serial_port_connecting.Load(Environment.CurrentDirectory + @"\" + "green.png");
            }
        }

        private void timer_serial_port_checking_Tick(object sender, EventArgs e)
        {
            string[] names = SerialPort.GetPortNames();   //获取当前serial port端口名称

            if (names.Length == 0 || m_oldSerialPortNames == null)  //如果一个端口都没有，返回
            {
                return;
            }

            //将当前获取的端口进行排序
            Array.Sort(names, (a, b) => Convert.ToInt32(((string)a).Substring(3)).CompareTo(Convert.ToInt32(((string)b).Substring(3))));
            int nCount = 0;

            if (names.Length == m_oldSerialPortNames.Length) //不能进行names==m_oldSerialPortNames的判断
            {
                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i] == m_oldSerialPortNames[i])  //将每一项进行比较
                    {
                        nCount++;                          //存在一种可能：length相同，但是具体的端口不一样
                    }
                }
                if (nCount == names.Length)  //如果每个都相同
                {
                    return;
                }
                else               //如果存在length一样，但是具体端口不一样         
                {
                    m_oldSerialPortNames = names;  //如果不匹配，将新的值赋给旧的值
                }
            }
            else
            {
                m_oldSerialPortNames = names;
            }

            this.comboBox_serial_port_name.Items.Clear();
            Array.Sort(names, (a, b) => Convert.ToInt32(((string)a).Substring(3)).CompareTo(Convert.ToInt32(((string)b).Substring(3))));
            this.comboBox_serial_port_name.Items.AddRange(names);
            this.comboBox_serial_port_name.SelectedIndex = 0;
        }

        private void button_serial_port_connect_Click(object sender, EventArgs e)
        {
            m_b_serialPortOpened =! m_b_serialPortOpened;
            if (m_b_serialPortOpened)
            {
                try
                {
                    this.serialPort1.Open();
                }
                catch (Exception ex)
                {
                    m_b_serialPortOpened = false;
                    MessageBox.Show(ex.Message);
                    return;
                }
                this.button_serial_port_connect.Text = "DISCONNECT";

                m_b_serialPortOpened = true;
                this.comboBox_serial_port_name.Enabled = false;
                LoadPicture();
            }
            else
            {
                this.button_serial_port_connect.Text = "CONNECT";
                this.serialPort1.Close();
                m_b_serialPortOpened = false;
                LoadPicture();
                this.comboBox_serial_port_name.Enabled = true;
            }
        }

        private void timer_query_Tick(object sender, EventArgs e)
        {
            //这句代码是必要的，开串口是需要时间的，如果不加，query_by_ID会直接退出几次，然而m_query_queue_No会自己走到下一个NO，
            //等待串口准备OK之后，发送的query就不是从0x01开始了，因为m_query_queue_No已经走到0x03,或者0x08去了
            if (!m_b_serialPortOpened)   
            {
                return;
            }

            QueryDevice.query_by_ID(QueryDevice.m_query_queue_No);

            //使用m_query_queue_No和m_prev_query_queue_No控制query
            switch (QueryDevice.m_query_queue_No)
            {
                case QueryDevice.FRAME_ID_QUERY_PATIENT_SIDE_TEMP:                                          //请求患者端温度，循环请求之一，不用按钮触发
                    QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_HUMIDITY;
                    QueryDevice.m_prev_query_queue_No = QueryDevice.m_query_queue_No;
                    break;
                case QueryDevice.FRAME_ID_QUERY_HUMIDITY:                                                     //请求患者端湿度，循环请求之一，不用按钮触发
                    QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_OUTLET_TEMP;
                    QueryDevice.m_prev_query_queue_No = QueryDevice.m_query_queue_No;
                    break;
                case QueryDevice.FRAME_ID_QUERY_OUTLET_TEMP:                                                  //请求出气口温度，循环请求之一，不用按钮触发
                    QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_ALARM_INFO;
                    QueryDevice.m_prev_query_queue_No = QueryDevice.m_query_queue_No;
                    break;
                case QueryDevice.FRAME_ID_QUERY_PARAMETERS:                                                   //请求参数
                    QueryDevice.m_query_queue_No = QueryDevice.m_prev_query_queue_No;                         
                    break;
                case QueryDevice.FRAME_ID_QUERY_SETTING_PARAMETERS:                                          //请求设置参数
                    QueryDevice.m_query_queue_No = QueryDevice.m_prev_query_queue_No;
                    break;
                case QueryDevice.FRAME_ID_QUERY_ALARM_INFO:                                                   //请求报警信息，循环请求之一，不用按钮触发
                    QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_RUNNING_TIME;
                    QueryDevice.m_prev_query_queue_No = QueryDevice.m_query_queue_No;
                    break;
                case QueryDevice.FRAME_ID_QUERY_PAUSE_ALARM_SOUND:                                              //请求暂停报警声音
                    QueryDevice.m_query_queue_No = QueryDevice.m_prev_query_queue_No;
                    break;
                case QueryDevice.FRAME_ID_QUERY_RUNNING_TIME:                                                   //请求运行时间，循环请求之一，不用按钮触发
                    QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_PATIENT_SIDE_TEMP;
                    QueryDevice.m_prev_query_queue_No = QueryDevice.m_query_queue_No;
                    break;
                case QueryDevice.FRAME_ID_QUERY_SETTING_SPECIAL_DISPLAY_MODE:                                   //请求设置特殊显示模式
                    QueryDevice.m_query_queue_No = QueryDevice.m_prev_query_queue_No;
                    break;
                default:
                    break;
            }
            

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var nPendingRead = this.serialPort1.BytesToRead;   //有多少字节需要读取
            byte[] tmp = new byte[nPendingRead];
            this.serialPort1.Read(tmp, 0, nPendingRead);       //读取数据到tmp中

            //m_bRcvParamtersCompleted = false;
            lock (QueryDevice.m_buffer)
            {
                QueryDevice.m_buffer.AddRange(tmp);   //将tmp中的数据装入m_buffer中
                #region
                while (QueryDevice.m_buffer.Count >= 5)
                {
                    if (QueryDevice.m_buffer[QueryDevice.HEAD0] == 0xAA&& QueryDevice.m_buffer[QueryDevice.HEAD1] ==0x55) //帧头为0xAA55
                    {
                        int len = Convert.ToInt32(QueryDevice.m_buffer[QueryDevice.LEN]); // 获取帧长度(不包含checksum1和checksum2)
                        if (QueryDevice.m_buffer.Count < len + 2)  //数据没有接收完全，继续接收
                        {
                            break;
                        }
                        int checksum = 256 * Convert.ToInt32(QueryDevice.m_buffer[len]) + Convert.ToInt32(QueryDevice.m_buffer[len + 1]);
                        int sum = 0;
                        for (int i = Convert.ToInt32(QueryDevice.LEN); i < len; i++) //校验和不包含包头,从2(LEN)开始算和
                        {
                            sum += Convert.ToInt32(QueryDevice.m_buffer[i]);
                        }
                        //MessageBox.Show(sum.ToString());
                        if (checksum == sum)
                        {
                            //解析数据
                            ParseData2Lists();
                        }
                        else
                        {
                            //校验之后发现数据不对,清除该帧数据
                            QueryDevice.m_buffer.RemoveRange(0, len + 2);
                            continue;
                        }
                        QueryDevice.m_buffer.RemoveRange(0, len + 2);
                    }
                    else
                    {
                        QueryDevice.m_buffer.RemoveAt(0); //清除帧头的0xAA55
                        QueryDevice.m_buffer.RemoveAt(0);
                    }
                }
                #endregion
            }
        }

        private void ParseData2Lists()
        {
            if (QueryDevice.m_buffer[QueryDevice.CMDTYPE] != 0x00)  
            {
                return;
            }

            //根据FRAME_ID来数据解析
            ParseBy(QueryDevice.m_buffer[QueryDevice.FRAME_ID]);
        }

        private void ParseBy(byte ID)
        {
            List<byte> list = new List<byte>();
            for (int i = QueryDevice.FRAME_ID + 1; i < QueryDevice.m_buffer[QueryDevice.LEN]; i++)
            {
                list.Add(QueryDevice.m_buffer[i]);
            }

            switch (ID)
            {
                case QueryDevice.FRAME_ID_RECEIVE_PATIENT_SIDE_TEMP:
                    if (list.Count == 2)
                    {
                        if (list[0] == 0xFF && list[1] == 0xFF)   //接收到错误数据
                        {
                            label_patient_side_temp_value.Text = @"/";
                        }
                        else
                        {
                            UInt16 result = Convert.ToUInt16(list[0] + list[1] * 256);
                            if (result <= 800)    //病人温度的值不能超过80度(800),否则就不更新数据
                            {
                                label_patient_side_temp_value.Text = String.Format("{0:N1}", (float)result / 10);
                            }
                        }
                    }
                    break;
                case QueryDevice.FRAME_ID_RECEIVE_HUMIDITY:
                    if (list.Count == 2)
                    {
                        if (list[0] == 0xFF && list[1] == 0xFF)   //接收到错误数据
                        {
                            label_Humidity_value.Text = @"/";
                        }
                        else
                        {
                            UInt16 result = Convert.ToUInt16(list[0] + list[1] * 256);
                            if (result <= 1000)    //湿度的值不能超过1000,否则就不更新数据
                            {
                                label_Humidity_value.Text = String.Format("{0:N1}", (float)result / 10);
                            }
                        }
                    }
                    break;
                case QueryDevice.FRAME_ID_RECEIVE_OUTLET_TEMP:
                    if (list.Count == 2)
                    {
                        if (list[0] == 0xFF && list[1] == 0xFF)   //接收到错误数据
                        {
                            label_chamber_outlet_temp_value.Text = @"/";
                        }
                        else
                        {
                            UInt16 result = Convert.ToUInt16(list[0] + list[1] * 256);
                            if (result <= 800)    //出气口温度的值不能超过80度(800),否则就不更新数据
                            {
                                label_chamber_outlet_temp_value.Text = String.Format("{0:N1}", (float)result/10);
                            }
                        }
                    }
                    break;
                case QueryDevice.FRAME_ID_RECEIVE_PARAMETERS:
                    if (list.Count == 7)
                    {
                        SetParametersby(list[0] == 0x00 ? MODE_NONINVASIVE : MODE_INVASIVE);
                    }
                    break;
                case QueryDevice.FRAME_ID_RECEIVE_ALARM_INFO:
                    if (list.Count == 2)
                    {
                        #region
                        byte alarm_info = list[0];                  //报警信息
                        byte alarm_sound_status = list[1];          //报警声音状态

                        if (alarm_info == 0x00)                     //无报警
                        {
                            textBox_alarm_info.Text = "No alarm";
                            show_alarm_sound_status(false);
                        }
                        else
                        {                                           //有报警
                            show_alarm_sound_status(true);
                            #region
                            if (alarm_info == 0x01)
                            {
                                textBox_alarm_info.Text = "Chamber is not install";
                            }
                            else if (alarm_info == 0x02)
                            {
                                textBox_alarm_info.Text = "Chamber has no water";
                            }
                            else if (alarm_info == 0x03)
                            {
                                textBox_alarm_info.Text = "Temperature-humidity data cable is unplugged";
                            }
                            else if (alarm_info == 0x04)
                            {
                                textBox_alarm_info.Text = "Patient side temperature is too high";
                            }
                            else if (alarm_info == 0x05)
                            {
                                textBox_alarm_info.Text = "Patient side temperature is too low";
                            }
                            else if (alarm_info == 0x06)
                            {
                                textBox_alarm_info.Text = "Chamber outlet temperature is too low";
                            }
                            else if (alarm_info == 0x07)
                            {
                                textBox_alarm_info.Text = "Patient side humidity is too low";
                            }
                            else if (alarm_info == 0x08)
                            {
                                textBox_alarm_info.Text = "Heater wire unselected or error";
                            }
                            else
                            {
                                //do nothing
                            }
                            #endregion
                            string picture_path_of_enable = Environment.CurrentDirectory + @"\" + "enable.png";
                            string picture_path_of_pause = Environment.CurrentDirectory + @"\" + "pause.png";
                            if (alarm_sound_status == 0x00)
                            {
                                if (this.pictureBox_alarm_sound_state.ImageLocation != picture_path_of_enable)
                                {
                                    //报警声音打开，加载enable图片
                                    this.pictureBox_alarm_sound_state.Load(picture_path_of_enable);
                                    this.button_alarm_sound_enable_disable.Text = SOUND_PAUSE;
                                }
                            }
                            else if (alarm_sound_status == 0x01)
                            {
                                if (this.pictureBox_alarm_sound_state.ImageLocation != picture_path_of_pause)
                                {
                                    //报警声音关闭，加载pause图片
                                    this.pictureBox_alarm_sound_state.Load(picture_path_of_pause);
                                    this.button_alarm_sound_enable_disable.Text = SOUND_ENABLE;
                                }   
                            }
                        }
                        #endregion
                    }
                    break;
                case QueryDevice.FRAME_ID_RECEIVE_RUNNING_TIME:
                    if (list.Count == 3)
                    {
                        label_running_time_value.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(list[0])) + @":"
                                            +Convert.ToString(Convert.ToInt32(list[1]))+@":"+Convert.ToString(Convert.ToInt32(list[2])));
                    }
                    break;
            }
        }

        private void show_alarm_sound_status(bool status)
        {
            button_alarm_sound_enable_disable.Visible = status;   //隐藏按钮
            pictureBox_alarm_sound_state.Visible = status;        //隐藏图片
            label_alarm_sound_status.Visible = status;            //隐藏label_alarm_sound_status
        }

        private void comboBox_serial_port_name_SelectedValueChanged(object sender, EventArgs e)
        {
            this.serialPort1.PortName = this.comboBox_serial_port_name.Text;
        }

        private void button_get_parameters_Click(object sender, EventArgs e)
        {
            if (m_b_serialPortOpened)
            {
                QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_PARAMETERS;
            }
            else
            {
                MessageBox.Show("Connect serial port first!");
            }
        }

        private void button_set_parameters_Click(object sender, EventArgs e)
        {
            if (m_b_serialPortOpened)
            {
                if (ConvertParameters2PARAINFO(ref m_current_info))
                {
                    QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_SETTING_PARAMETERS;
                }
            }
            else
            {
                MessageBox.Show("Connect serial port first!");
            }  
        }

        private void button_alarm_sound_enable_disable_Click(object sender, EventArgs e)
        {
            QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_PAUSE_ALARM_SOUND;
            if (this.button_alarm_sound_enable_disable.Text == SOUND_PAUSE)
            {
                this.button_alarm_sound_enable_disable.Text = SOUND_ENABLE;
                m_b_sound_enable = true;
                this.pictureBox_alarm_sound_state.Load(Environment.CurrentDirectory + @"\" + "pause.png");
            }
            else if (this.button_alarm_sound_enable_disable.Text == SOUND_ENABLE)
            {
                this.button_alarm_sound_enable_disable.Text = SOUND_PAUSE;
                m_b_sound_enable = false;
                this.pictureBox_alarm_sound_state.Load(Environment.CurrentDirectory + @"\" + "enable.png");
            }
        }

        private void button_set_special_display_mode_Click(object sender, EventArgs e)
        {
            QueryDevice.m_query_queue_No = QueryDevice.FRAME_ID_QUERY_SETTING_SPECIAL_DISPLAY_MODE;
        }

        private void comboBox_para_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置患者端温度combox的范围
            SetRangebyMode(((ComboBox)sender).Text);
        }

        private void SetParametersby(string para_mode)
        {
            //根据模式设置范围
            SetRangebyMode(para_mode);

            //判断患者端温度Tp和出气口温度Tc
            UInt16 Tp = Convert.ToUInt16(QueryDevice.m_buffer[QueryDevice.FRAME_ID + 2]        //获取Tp
                + QueryDevice.m_buffer[QueryDevice.FRAME_ID + 3] * 256);

            UInt16 Tc = Convert.ToUInt16(QueryDevice.m_buffer[QueryDevice.FRAME_ID + 4]        //获取Tc
                + QueryDevice.m_buffer[QueryDevice.FRAME_ID + 5] * 256);

            if (Tp == Convert.ToUInt16(0xFFFF))   //如果Tp为0xFFFFF,接收到错误的温度设置信息，显示"/"
            {
                comboBox_para_patient_side_temp_setpoint.Items.Clear();
                comboBox_para_patient_side_temp_setpoint.Text = @"/";
            }
            else if (Tc == Convert.ToUInt16(0xFFFF))  //如果Tc为0xFFFF,接收到错误的出气口温度设置信息，显示"/"
            {
                comboBox_para_chamber_outlet_temp_setpoint.Items.Clear();
                comboBox_para_chamber_outlet_temp_setpoint.Text = @"/";
            }
            else if (Tc >= Tp - 40 && Tc <= Tp + 30)     //如果 Tp-40 <= Tc <= Tp+30才进行设置，否则就认为接收的数据出错，不用管
            {
                //设置模式                                                                              //同时将模式信息放入m_current_info中
                if (QueryDevice.m_buffer[QueryDevice.FRAME_ID + 1] == 0x00)
                {
                    this.comboBox_para_mode.Text = MODE_NONINVASIVE;
                    m_current_info.PARA_MODE = 0x00;
                }
                else if (QueryDevice.m_buffer[QueryDevice.FRAME_ID + 1] == 0x01)
                {
                    this.comboBox_para_mode.Text = MODE_INVASIVE;
                    m_current_info.PARA_MODE = 0x01;
                }
                else
                {
                    //do nothing
                    this.comboBox_para_mode.Text = @"/";
                }

                //设置患者端温度和出气口温度，同时放入m_current_info中
                comboBox_para_patient_side_temp_setpoint.Text = String.Format("{0:N1}", (float)Tp / 10);            //设置Tp
                comboBox_para_chamber_outlet_temp_setpoint.Text = String.Format("{0:N1}", (float)Tc / 10);        //设置Tc

                m_current_info.PARA_PATIENT_TEMP_SETPOINT_0 = Convert.ToByte(Tp % 256);                                 //将Tp放入m_current_info中
                m_current_info.PARA_PATIENT_TEMP_SETPOINT_1 = Convert.ToByte(Tp / 256);

                m_current_info.PARA_CHAMBER_OUTLET_TEMP_SETPOINT_0 = Convert.ToByte(Tc % 256);                          //将Tc放入m_current_info中
                m_current_info.PARA_CHAMBER_OUTLET_TEMP_SETPOINT_1 = Convert.ToByte(Tc / 256);

                //设置IN/EXP
                byte in_exp = QueryDevice.m_buffer[QueryDevice.FRAME_ID + 6];                                        //设置IN/EXP
                if (in_exp == 0x01)
                {
                    comboBox_para_in_exp.Text = "1:1.0";
                }
                else if (in_exp == 0x02)
                {
                    comboBox_para_in_exp.Text = "1:1.1";
                }
                else if (in_exp == 0x03)
                {
                    comboBox_para_in_exp.Text = "1:1.2";
                }
                else if (in_exp == 0x04)
                {
                    comboBox_para_in_exp.Text = "1:1.3";
                }
                else if (in_exp == 0x05)
                {
                    comboBox_para_in_exp.Text = "1:1.4";
                }
                else if (in_exp == 0x06)
                {
                    comboBox_para_in_exp.Text = "1:1.5";
                }
                else
                {
                    comboBox_para_in_exp.Text = @"/";
                }

                m_current_info.PARA_IN_EXP = QueryDevice.m_buffer[QueryDevice.FRAME_ID + 6];                             //将IN/EXP信息放入m_current_info中

                if (QueryDevice.m_buffer[QueryDevice.FRAME_ID + 7] == 0x01)                                             //将heater wire mode信息放入m_current_info中
                {
                    comboBox_heater_wire_mode.Text = "Double Heater Wire";
                    m_current_info.PARA_HEATER_WIRE_MODE = 0x01;
                }
                else if (QueryDevice.m_buffer[QueryDevice.FRAME_ID + 7] == 0x02)
                {
                    comboBox_heater_wire_mode.Text = "Insp Only";
                    m_current_info.PARA_HEATER_WIRE_MODE = 0x02;
                }
                else if (QueryDevice.m_buffer[QueryDevice.FRAME_ID + 7] == 0x03)
                {
                    comboBox_heater_wire_mode.Text = "None";
                    m_current_info.PARA_HEATER_WIRE_MODE = 0x03;
                }
                else
                {
                    //do nothing
                }
            }
           
        }

        private void SetRangebyMode(string para_mode)
        {
            int start_patient_temp = 0;
            int end_patient_temp = 0;
            int start_outlet_temp = 0;
            int end_outlet_temp = 0;

            //发送参数的时候，根据用户选择无创或有创来调整范围
            if (para_mode == MODE_NONINVASIVE)
            {
                start_patient_temp = 310;
                end_patient_temp = 370;

                start_outlet_temp = 300;
                end_outlet_temp = 320;
            }
            else if (para_mode == MODE_INVASIVE)
            {
                start_patient_temp = 350;
                end_patient_temp = 400;

                start_outlet_temp = 340;
                end_outlet_temp = 430;
            }
            else
            {
                return;
            }

            //根据有创和无创，重新设置患者端温度的范围
            //添加患者端温度，步长为5
            comboBox_para_patient_side_temp_setpoint.Items.Clear();
            for (int i = start_patient_temp; i <= end_patient_temp;)
            {
                comboBox_para_patient_side_temp_setpoint.Items.Add(String.Format("{0:N1}", (float)i / 10));
                i += 5;
            }

            //根据有创和无创，重新设置出气口温度的范围
            //添加出气口温度，步长为5
            comboBox_para_chamber_outlet_temp_setpoint.Items.Clear();
            for (int i = start_outlet_temp; i <= end_outlet_temp;)
            {
                comboBox_para_chamber_outlet_temp_setpoint.Items.Add(String.Format("{0:N1}", (float)i / 10));
                i += 5;
            }

            //设置IN/EXP的比值Items.Clear();
            comboBox_para_in_exp.Items.Clear();
            for (int i = 0; i <= 5; i++)
            {
                comboBox_para_in_exp.Items.Add("1:1." + Convert.ToString(i));
            }

            //设置heater wire mode
            comboBox_heater_wire_mode.Items.Clear();
            comboBox_heater_wire_mode.Items.Add("Double Heater Wire");
            comboBox_heater_wire_mode.Items.Add("Insp Only");
            comboBox_heater_wire_mode.Items.Add("None");
        }
    }
}
