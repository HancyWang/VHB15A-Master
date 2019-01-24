namespace VHB15A_PARA_SHOW
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer_serial_port_checking = new System.Windows.Forms.Timer(this.components);
            this.timer_query = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_running_time_value = new System.Windows.Forms.Label();
            this.label_running_time = new System.Windows.Forms.Label();
            this.label_chamber_outlet_temp_value = new System.Windows.Forms.Label();
            this.label_chamber_outlet_temp = new System.Windows.Forms.Label();
            this.label_Humidity_value = new System.Windows.Forms.Label();
            this.label_humidity = new System.Windows.Forms.Label();
            this.label_patient_side_temp_value = new System.Windows.Forms.Label();
            this.label_patient_side_temp = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox_alarm_sound_state = new System.Windows.Forms.PictureBox();
            this.label_alarm_sound_status = new System.Windows.Forms.Label();
            this.button_alarm_sound_enable_disable = new System.Windows.Forms.Button();
            this.textBox_alarm_info = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox_para_mode = new System.Windows.Forms.ComboBox();
            this.comboBox_heater_wire_mode = new System.Windows.Forms.ComboBox();
            this.comboBox_para_in_exp = new System.Windows.Forms.ComboBox();
            this.comboBox_para_chamber_outlet_temp_setpoint = new System.Windows.Forms.ComboBox();
            this.comboBox_para_patient_side_temp_setpoint = new System.Windows.Forms.ComboBox();
            this.button_set_special_display_mode = new System.Windows.Forms.Button();
            this.button_set_parameters = new System.Windows.Forms.Button();
            this.button_get_parameters = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button_serial_port_connect = new System.Windows.Forms.Button();
            this.pictureBox_serial_port_connecting = new System.Windows.Forms.PictureBox();
            this.comboBox_serial_port_flow_control = new System.Windows.Forms.ComboBox();
            this.comboBox_serial_port_parity = new System.Windows.Forms.ComboBox();
            this.comboBox_serial_port_stop_bits = new System.Windows.Forms.ComboBox();
            this.comboBox_serial_port_data_bits = new System.Windows.Forms.ComboBox();
            this.comboBox_serial_port_baut_rate = new System.Windows.Forms.ComboBox();
            this.comboBox_serial_port_name = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_alarm_sound_state)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_serial_port_connecting)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer_serial_port_checking
            // 
            this.timer_serial_port_checking.Enabled = true;
            this.timer_serial_port_checking.Interval = 200;
            this.timer_serial_port_checking.Tick += new System.EventHandler(this.timer_serial_port_checking_Tick);
            // 
            // timer_query
            // 
            this.timer_query.Enabled = true;
            this.timer_query.Interval = 200;
            this.timer_query.Tick += new System.EventHandler(this.timer_query_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label_running_time_value);
            this.groupBox2.Controls.Add(this.label_running_time);
            this.groupBox2.Controls.Add(this.label_chamber_outlet_temp_value);
            this.groupBox2.Controls.Add(this.label_chamber_outlet_temp);
            this.groupBox2.Controls.Add(this.label_Humidity_value);
            this.groupBox2.Controls.Add(this.label_humidity);
            this.groupBox2.Controls.Add(this.label_patient_side_temp_value);
            this.groupBox2.Controls.Add(this.label_patient_side_temp);
            this.groupBox2.Location = new System.Drawing.Point(332, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1046, 494);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 15);
            this.label4.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 404);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "(degree centigrade)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "(degree centigrade)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "(D:H:M)";
            // 
            // label_running_time_value
            // 
            this.label_running_time_value.AutoSize = true;
            this.label_running_time_value.Font = new System.Drawing.Font("宋体", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_running_time_value.Location = new System.Drawing.Point(460, 15);
            this.label_running_time_value.Name = "label_running_time_value";
            this.label_running_time_value.Size = new System.Drawing.Size(45, 48);
            this.label_running_time_value.TabIndex = 8;
            this.label_running_time_value.Text = "0";
            // 
            // label_running_time
            // 
            this.label_running_time.AutoSize = true;
            this.label_running_time.Location = new System.Drawing.Point(14, 34);
            this.label_running_time.Name = "label_running_time";
            this.label_running_time.Size = new System.Drawing.Size(119, 15);
            this.label_running_time.TabIndex = 7;
            this.label_running_time.Text = "Running Time :";
            // 
            // label_chamber_outlet_temp_value
            // 
            this.label_chamber_outlet_temp_value.AutoSize = true;
            this.label_chamber_outlet_temp_value.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_chamber_outlet_temp_value.Location = new System.Drawing.Point(433, 333);
            this.label_chamber_outlet_temp_value.Name = "label_chamber_outlet_temp_value";
            this.label_chamber_outlet_temp_value.Size = new System.Drawing.Size(111, 120);
            this.label_chamber_outlet_temp_value.TabIndex = 6;
            this.label_chamber_outlet_temp_value.Text = "0";
            // 
            // label_chamber_outlet_temp
            // 
            this.label_chamber_outlet_temp.AutoSize = true;
            this.label_chamber_outlet_temp.Location = new System.Drawing.Point(12, 380);
            this.label_chamber_outlet_temp.Name = "label_chamber_outlet_temp";
            this.label_chamber_outlet_temp.Size = new System.Drawing.Size(183, 15);
            this.label_chamber_outlet_temp.TabIndex = 5;
            this.label_chamber_outlet_temp.Text = "Chamber Outlet Temp. :";
            // 
            // label_Humidity_value
            // 
            this.label_Humidity_value.AutoSize = true;
            this.label_Humidity_value.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Humidity_value.Location = new System.Drawing.Point(433, 203);
            this.label_Humidity_value.Name = "label_Humidity_value";
            this.label_Humidity_value.Size = new System.Drawing.Size(111, 120);
            this.label_Humidity_value.TabIndex = 4;
            this.label_Humidity_value.Text = "0";
            // 
            // label_humidity
            // 
            this.label_humidity.AutoSize = true;
            this.label_humidity.Location = new System.Drawing.Point(14, 265);
            this.label_humidity.Name = "label_humidity";
            this.label_humidity.Size = new System.Drawing.Size(119, 15);
            this.label_humidity.TabIndex = 3;
            this.label_humidity.Text = "Humidity(%RH):";
            // 
            // label_patient_side_temp_value
            // 
            this.label_patient_side_temp_value.AutoSize = true;
            this.label_patient_side_temp_value.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_patient_side_temp_value.Location = new System.Drawing.Point(433, 71);
            this.label_patient_side_temp_value.Name = "label_patient_side_temp_value";
            this.label_patient_side_temp_value.Size = new System.Drawing.Size(111, 120);
            this.label_patient_side_temp_value.TabIndex = 2;
            this.label_patient_side_temp_value.Text = "0";
            this.label_patient_side_temp_value.Click += new System.EventHandler(this.label_patient_side_temp_value_Click);
            // 
            // label_patient_side_temp
            // 
            this.label_patient_side_temp.AutoSize = true;
            this.label_patient_side_temp.Location = new System.Drawing.Point(14, 131);
            this.label_patient_side_temp.Name = "label_patient_side_temp";
            this.label_patient_side_temp.Size = new System.Drawing.Size(167, 15);
            this.label_patient_side_temp.TabIndex = 1;
            this.label_patient_side_temp.Text = "Patient Side Temp. :";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pictureBox_alarm_sound_state);
            this.groupBox1.Controls.Add(this.label_alarm_sound_status);
            this.groupBox1.Controls.Add(this.button_alarm_sound_enable_disable);
            this.groupBox1.Controls.Add(this.textBox_alarm_info);
            this.groupBox1.Location = new System.Drawing.Point(332, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1046, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alarm Info.";
            // 
            // pictureBox_alarm_sound_state
            // 
            this.pictureBox_alarm_sound_state.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_alarm_sound_state.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_alarm_sound_state.Image")));
            this.pictureBox_alarm_sound_state.Location = new System.Drawing.Point(569, 48);
            this.pictureBox_alarm_sound_state.Name = "pictureBox_alarm_sound_state";
            this.pictureBox_alarm_sound_state.Size = new System.Drawing.Size(206, 47);
            this.pictureBox_alarm_sound_state.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_alarm_sound_state.TabIndex = 3;
            this.pictureBox_alarm_sound_state.TabStop = false;
            // 
            // label_alarm_sound_status
            // 
            this.label_alarm_sound_status.AutoSize = true;
            this.label_alarm_sound_status.Location = new System.Drawing.Point(573, 23);
            this.label_alarm_sound_status.Name = "label_alarm_sound_status";
            this.label_alarm_sound_status.Size = new System.Drawing.Size(159, 15);
            this.label_alarm_sound_status.TabIndex = 2;
            this.label_alarm_sound_status.Text = "Alarm Sound Status:";
            // 
            // button_alarm_sound_enable_disable
            // 
            this.button_alarm_sound_enable_disable.Location = new System.Drawing.Point(814, 48);
            this.button_alarm_sound_enable_disable.Name = "button_alarm_sound_enable_disable";
            this.button_alarm_sound_enable_disable.Size = new System.Drawing.Size(120, 44);
            this.button_alarm_sound_enable_disable.TabIndex = 1;
            this.button_alarm_sound_enable_disable.Text = "SOUND ENABLE ";
            this.button_alarm_sound_enable_disable.UseVisualStyleBackColor = true;
            this.button_alarm_sound_enable_disable.Click += new System.EventHandler(this.button_alarm_sound_enable_disable_Click);
            // 
            // textBox_alarm_info
            // 
            this.textBox_alarm_info.Location = new System.Drawing.Point(6, 45);
            this.textBox_alarm_info.Name = "textBox_alarm_info";
            this.textBox_alarm_info.ReadOnly = true;
            this.textBox_alarm_info.Size = new System.Drawing.Size(449, 25);
            this.textBox_alarm_info.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.comboBox_para_mode);
            this.groupBox3.Controls.Add(this.comboBox_heater_wire_mode);
            this.groupBox3.Controls.Add(this.comboBox_para_in_exp);
            this.groupBox3.Controls.Add(this.comboBox_para_chamber_outlet_temp_setpoint);
            this.groupBox3.Controls.Add(this.comboBox_para_patient_side_temp_setpoint);
            this.groupBox3.Controls.Add(this.button_set_special_display_mode);
            this.groupBox3.Controls.Add(this.button_set_parameters);
            this.groupBox3.Controls.Add(this.button_get_parameters);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(332, 629);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1046, 353);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parameters";
            // 
            // comboBox_para_mode
            // 
            this.comboBox_para_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_para_mode.FormattingEnabled = true;
            this.comboBox_para_mode.Items.AddRange(new object[] {
            "Noninvasive",
            "Invasive"});
            this.comboBox_para_mode.Location = new System.Drawing.Point(453, 88);
            this.comboBox_para_mode.Name = "comboBox_para_mode";
            this.comboBox_para_mode.Size = new System.Drawing.Size(184, 23);
            this.comboBox_para_mode.TabIndex = 17;
            this.comboBox_para_mode.SelectedIndexChanged += new System.EventHandler(this.comboBox_para_mode_SelectedIndexChanged);
            // 
            // comboBox_heater_wire_mode
            // 
            this.comboBox_heater_wire_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_heater_wire_mode.FormattingEnabled = true;
            this.comboBox_heater_wire_mode.Location = new System.Drawing.Point(453, 279);
            this.comboBox_heater_wire_mode.Name = "comboBox_heater_wire_mode";
            this.comboBox_heater_wire_mode.Size = new System.Drawing.Size(184, 23);
            this.comboBox_heater_wire_mode.TabIndex = 16;
            // 
            // comboBox_para_in_exp
            // 
            this.comboBox_para_in_exp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_para_in_exp.FormattingEnabled = true;
            this.comboBox_para_in_exp.Location = new System.Drawing.Point(453, 230);
            this.comboBox_para_in_exp.Name = "comboBox_para_in_exp";
            this.comboBox_para_in_exp.Size = new System.Drawing.Size(184, 23);
            this.comboBox_para_in_exp.TabIndex = 15;
            // 
            // comboBox_para_chamber_outlet_temp_setpoint
            // 
            this.comboBox_para_chamber_outlet_temp_setpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_para_chamber_outlet_temp_setpoint.FormattingEnabled = true;
            this.comboBox_para_chamber_outlet_temp_setpoint.Location = new System.Drawing.Point(453, 183);
            this.comboBox_para_chamber_outlet_temp_setpoint.Name = "comboBox_para_chamber_outlet_temp_setpoint";
            this.comboBox_para_chamber_outlet_temp_setpoint.Size = new System.Drawing.Size(184, 23);
            this.comboBox_para_chamber_outlet_temp_setpoint.TabIndex = 14;
            // 
            // comboBox_para_patient_side_temp_setpoint
            // 
            this.comboBox_para_patient_side_temp_setpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_para_patient_side_temp_setpoint.FormattingEnabled = true;
            this.comboBox_para_patient_side_temp_setpoint.Location = new System.Drawing.Point(453, 134);
            this.comboBox_para_patient_side_temp_setpoint.Name = "comboBox_para_patient_side_temp_setpoint";
            this.comboBox_para_patient_side_temp_setpoint.Size = new System.Drawing.Size(184, 23);
            this.comboBox_para_patient_side_temp_setpoint.TabIndex = 13;
            this.comboBox_para_patient_side_temp_setpoint.SelectedIndexChanged += new System.EventHandler(this.comboBox_para_patient_side_temp_setpoint_SelectedIndexChanged);
            // 
            // button_set_special_display_mode
            // 
            this.button_set_special_display_mode.Location = new System.Drawing.Point(908, 32);
            this.button_set_special_display_mode.Name = "button_set_special_display_mode";
            this.button_set_special_display_mode.Size = new System.Drawing.Size(35, 20);
            this.button_set_special_display_mode.TabIndex = 12;
            this.button_set_special_display_mode.Text = "SET SPECIAL DISPALY MODE";
            this.button_set_special_display_mode.UseVisualStyleBackColor = true;
            this.button_set_special_display_mode.Visible = false;
            this.button_set_special_display_mode.Click += new System.EventHandler(this.button_set_special_display_mode_Click);
            // 
            // button_set_parameters
            // 
            this.button_set_parameters.Location = new System.Drawing.Point(184, 32);
            this.button_set_parameters.Name = "button_set_parameters";
            this.button_set_parameters.Size = new System.Drawing.Size(92, 40);
            this.button_set_parameters.TabIndex = 11;
            this.button_set_parameters.Text = "SET";
            this.button_set_parameters.UseVisualStyleBackColor = true;
            this.button_set_parameters.Click += new System.EventHandler(this.button_set_parameters_Click);
            // 
            // button_get_parameters
            // 
            this.button_get_parameters.Location = new System.Drawing.Point(16, 32);
            this.button_get_parameters.Name = "button_get_parameters";
            this.button_get_parameters.Size = new System.Drawing.Size(92, 40);
            this.button_get_parameters.TabIndex = 10;
            this.button_get_parameters.Text = "GET";
            this.button_get_parameters.UseVisualStyleBackColor = true;
            this.button_get_parameters.Click += new System.EventHandler(this.button_get_parameters_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 287);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 15);
            this.label10.TabIndex = 4;
            this.label10.Text = "Heater Wire Mode : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 238);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "IN/EXP :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(255, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Chamber Outlet Temp. Setpoint :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(239, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "Patient Side Temp. Setpoint :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Mode:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button_serial_port_connect);
            this.groupBox4.Controls.Add(this.pictureBox_serial_port_connecting);
            this.groupBox4.Controls.Add(this.comboBox_serial_port_flow_control);
            this.groupBox4.Controls.Add(this.comboBox_serial_port_parity);
            this.groupBox4.Controls.Add(this.comboBox_serial_port_stop_bits);
            this.groupBox4.Controls.Add(this.comboBox_serial_port_data_bits);
            this.groupBox4.Controls.Add(this.comboBox_serial_port_baut_rate);
            this.groupBox4.Controls.Add(this.comboBox_serial_port_name);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(314, 970);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Serial Port ";
            // 
            // button_serial_port_connect
            // 
            this.button_serial_port_connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button_serial_port_connect.Location = new System.Drawing.Point(130, 389);
            this.button_serial_port_connect.Name = "button_serial_port_connect";
            this.button_serial_port_connect.Size = new System.Drawing.Size(99, 49);
            this.button_serial_port_connect.TabIndex = 13;
            this.button_serial_port_connect.Text = "CONNECT";
            this.button_serial_port_connect.UseVisualStyleBackColor = false;
            this.button_serial_port_connect.Click += new System.EventHandler(this.button_serial_port_connect_Click);
            // 
            // pictureBox_serial_port_connecting
            // 
            this.pictureBox_serial_port_connecting.Location = new System.Drawing.Point(10, 389);
            this.pictureBox_serial_port_connecting.Name = "pictureBox_serial_port_connecting";
            this.pictureBox_serial_port_connecting.Size = new System.Drawing.Size(54, 49);
            this.pictureBox_serial_port_connecting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_serial_port_connecting.TabIndex = 12;
            this.pictureBox_serial_port_connecting.TabStop = false;
            // 
            // comboBox_serial_port_flow_control
            // 
            this.comboBox_serial_port_flow_control.FormattingEnabled = true;
            this.comboBox_serial_port_flow_control.Location = new System.Drawing.Point(130, 328);
            this.comboBox_serial_port_flow_control.Name = "comboBox_serial_port_flow_control";
            this.comboBox_serial_port_flow_control.Size = new System.Drawing.Size(121, 23);
            this.comboBox_serial_port_flow_control.TabIndex = 11;
            // 
            // comboBox_serial_port_parity
            // 
            this.comboBox_serial_port_parity.FormattingEnabled = true;
            this.comboBox_serial_port_parity.Location = new System.Drawing.Point(130, 275);
            this.comboBox_serial_port_parity.Name = "comboBox_serial_port_parity";
            this.comboBox_serial_port_parity.Size = new System.Drawing.Size(121, 23);
            this.comboBox_serial_port_parity.TabIndex = 10;
            // 
            // comboBox_serial_port_stop_bits
            // 
            this.comboBox_serial_port_stop_bits.FormattingEnabled = true;
            this.comboBox_serial_port_stop_bits.Location = new System.Drawing.Point(130, 215);
            this.comboBox_serial_port_stop_bits.Name = "comboBox_serial_port_stop_bits";
            this.comboBox_serial_port_stop_bits.Size = new System.Drawing.Size(121, 23);
            this.comboBox_serial_port_stop_bits.TabIndex = 9;
            // 
            // comboBox_serial_port_data_bits
            // 
            this.comboBox_serial_port_data_bits.FormattingEnabled = true;
            this.comboBox_serial_port_data_bits.Location = new System.Drawing.Point(130, 159);
            this.comboBox_serial_port_data_bits.Name = "comboBox_serial_port_data_bits";
            this.comboBox_serial_port_data_bits.Size = new System.Drawing.Size(121, 23);
            this.comboBox_serial_port_data_bits.TabIndex = 8;
            // 
            // comboBox_serial_port_baut_rate
            // 
            this.comboBox_serial_port_baut_rate.FormattingEnabled = true;
            this.comboBox_serial_port_baut_rate.Location = new System.Drawing.Point(130, 99);
            this.comboBox_serial_port_baut_rate.Name = "comboBox_serial_port_baut_rate";
            this.comboBox_serial_port_baut_rate.Size = new System.Drawing.Size(121, 23);
            this.comboBox_serial_port_baut_rate.TabIndex = 7;
            // 
            // comboBox_serial_port_name
            // 
            this.comboBox_serial_port_name.FormattingEnabled = true;
            this.comboBox_serial_port_name.Location = new System.Drawing.Point(130, 43);
            this.comboBox_serial_port_name.Name = "comboBox_serial_port_name";
            this.comboBox_serial_port_name.Size = new System.Drawing.Size(121, 23);
            this.comboBox_serial_port_name.TabIndex = 6;
            this.comboBox_serial_port_name.SelectedValueChanged += new System.EventHandler(this.comboBox_serial_port_name_SelectedValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 331);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 15);
            this.label16.TabIndex = 5;
            this.label16.Text = "Flow Control:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 278);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 15);
            this.label15.TabIndex = 4;
            this.label15.Text = "Parity:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 218);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 15);
            this.label14.TabIndex = 3;
            this.label14.Text = "Stop Bits:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 161);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 15);
            this.label13.TabIndex = 2;
            this.label13.Text = "Data Bits:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 15);
            this.label12.TabIndex = 1;
            this.label12.Text = "Baut Rate:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "Port Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1374, 997);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VHB15A Data Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_alarm_sound_state)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_serial_port_connecting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer_serial_port_checking;
        private System.Windows.Forms.Timer timer_query;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_running_time_value;
        private System.Windows.Forms.Label label_running_time;
        private System.Windows.Forms.Label label_chamber_outlet_temp_value;
        private System.Windows.Forms.Label label_chamber_outlet_temp;
        private System.Windows.Forms.Label label_Humidity_value;
        private System.Windows.Forms.Label label_humidity;
        private System.Windows.Forms.Label label_patient_side_temp_value;
        private System.Windows.Forms.Label label_patient_side_temp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox_alarm_sound_state;
        private System.Windows.Forms.Label label_alarm_sound_status;
        private System.Windows.Forms.Button button_alarm_sound_enable_disable;
        private System.Windows.Forms.TextBox textBox_alarm_info;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_serial_port_connect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox_para_mode;
        private System.Windows.Forms.ComboBox comboBox_heater_wire_mode;
        private System.Windows.Forms.ComboBox comboBox_para_in_exp;
        private System.Windows.Forms.ComboBox comboBox_para_chamber_outlet_temp_setpoint;
        private System.Windows.Forms.ComboBox comboBox_para_patient_side_temp_setpoint;
        private System.Windows.Forms.Button button_set_special_display_mode;
        private System.Windows.Forms.Button button_set_parameters;
        private System.Windows.Forms.Button button_get_parameters;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox_serial_port_connecting;
        private System.Windows.Forms.ComboBox comboBox_serial_port_flow_control;
        private System.Windows.Forms.ComboBox comboBox_serial_port_parity;
        private System.Windows.Forms.ComboBox comboBox_serial_port_stop_bits;
        private System.Windows.Forms.ComboBox comboBox_serial_port_data_bits;
        private System.Windows.Forms.ComboBox comboBox_serial_port_baut_rate;
        private System.Windows.Forms.ComboBox comboBox_serial_port_name;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}

