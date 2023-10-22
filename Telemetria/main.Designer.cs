namespace Telemetria
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dg_acumulado = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dg_hora = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dg_dia = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_id = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_rata = new System.Windows.Forms.ComboBox();
            this.tb_timeout = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dg_modbus = new System.Windows.Forms.DataGridView();
            this.bt_guardar = new System.Windows.Forms.Button();
            this.cb_escala = new System.Windows.Forms.ComboBox();
            this.tb_frecuencia = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_puertos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_puntos = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tb_test = new System.Windows.Forms.TextBox();
            this.list_logs = new System.Windows.Forms.ListBox();
            this.puertoserial = new System.IO.Ports.SerialPort(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.temporizador = new System.Windows.Forms.Timer(this.components);
            this.estado = new System.Windows.Forms.StatusStrip();
            this.status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.estado1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.estado2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.estado3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cb_intentos = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_acumulado)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_hora)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_dia)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_modbus)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.estado.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(4, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(935, 437);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(927, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Datos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(676, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 311;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dg_acumulado);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.groupBox3.Location = new System.Drawing.Point(3, 248);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(913, 115);
            this.groupBox3.TabIndex = 310;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Acumulado";
            // 
            // dg_acumulado
            // 
            this.dg_acumulado.AllowUserToAddRows = false;
            this.dg_acumulado.AllowUserToDeleteRows = false;
            this.dg_acumulado.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_acumulado.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_acumulado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dg_acumulado.BackgroundColor = System.Drawing.Color.White;
            this.dg_acumulado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dg_acumulado.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_acumulado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dg_acumulado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_acumulado.DefaultCellStyle = dataGridViewCellStyle3;
            this.dg_acumulado.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dg_acumulado.Location = new System.Drawing.Point(6, 26);
            this.dg_acumulado.Name = "dg_acumulado";
            this.dg_acumulado.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dg_acumulado.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_acumulado.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dg_acumulado.RowTemplate.Height = 24;
            this.dg_acumulado.Size = new System.Drawing.Size(897, 83);
            this.dg_acumulado.TabIndex = 310;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dg_hora);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.groupBox2.Location = new System.Drawing.Point(6, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(913, 115);
            this.groupBox2.TabIndex = 310;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hora";
            // 
            // dg_hora
            // 
            this.dg_hora.AllowUserToAddRows = false;
            this.dg_hora.AllowUserToDeleteRows = false;
            this.dg_hora.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_hora.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dg_hora.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dg_hora.BackgroundColor = System.Drawing.Color.White;
            this.dg_hora.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dg_hora.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_hora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dg_hora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_hora.DefaultCellStyle = dataGridViewCellStyle7;
            this.dg_hora.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dg_hora.Location = new System.Drawing.Point(6, 26);
            this.dg_hora.Name = "dg_hora";
            this.dg_hora.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dg_hora.RowHeadersVisible = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_hora.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dg_hora.RowTemplate.Height = 24;
            this.dg_hora.Size = new System.Drawing.Size(897, 83);
            this.dg_hora.TabIndex = 309;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(785, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 309;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dg_dia);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(913, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Día";
            // 
            // dg_dia
            // 
            this.dg_dia.AllowUserToAddRows = false;
            this.dg_dia.AllowUserToDeleteRows = false;
            this.dg_dia.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_dia.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dg_dia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dg_dia.BackgroundColor = System.Drawing.Color.White;
            this.dg_dia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dg_dia.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_dia.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dg_dia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_dia.DefaultCellStyle = dataGridViewCellStyle11;
            this.dg_dia.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dg_dia.Location = new System.Drawing.Point(6, 26);
            this.dg_dia.Name = "dg_dia";
            this.dg_dia.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dg_dia.RowHeadersVisible = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_dia.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dg_dia.RowTemplate.Height = 24;
            this.dg_dia.Size = new System.Drawing.Size(897, 83);
            this.dg_dia.TabIndex = 308;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cb_intentos);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.tb_id);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.cb_rata);
            this.tabPage2.Controls.Add(this.tb_timeout);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.dg_modbus);
            this.tabPage2.Controls.Add(this.bt_guardar);
            this.tabPage2.Controls.Add(this.cb_escala);
            this.tabPage2.Controls.Add(this.tb_frecuencia);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cb_puertos);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cb_puntos);
            this.tabPage2.Controls.Add(this.label46);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(927, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuración";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(323, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 23);
            this.label7.TabIndex = 318;
            this.label7.Text = "Id Modbus";
            // 
            // tb_id
            // 
            this.tb_id.Location = new System.Drawing.Point(426, 91);
            this.tb_id.Name = "tb_id";
            this.tb_id.Size = new System.Drawing.Size(43, 20);
            this.tb_id.TabIndex = 317;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(475, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 19);
            this.label6.TabIndex = 316;
            this.label6.Text = "ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(323, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 23);
            this.label4.TabIndex = 315;
            this.label4.Text = "Timeout";
            // 
            // cb_rata
            // 
            this.cb_rata.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_rata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_rata.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_rata.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400"});
            this.cb_rata.Location = new System.Drawing.Point(426, 37);
            this.cb_rata.Name = "cb_rata";
            this.cb_rata.Size = new System.Drawing.Size(78, 21);
            this.cb_rata.TabIndex = 314;
            // 
            // tb_timeout
            // 
            this.tb_timeout.Location = new System.Drawing.Point(426, 64);
            this.tb_timeout.Name = "tb_timeout";
            this.tb_timeout.Size = new System.Drawing.Size(43, 20);
            this.tb_timeout.TabIndex = 313;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(323, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 23);
            this.label3.TabIndex = 312;
            this.label3.Text = "Rata puerto";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(9, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 23);
            this.label5.TabIndex = 311;
            this.label5.Text = "Configuración Modbus";
            // 
            // dg_modbus
            // 
            this.dg_modbus.AllowUserToDeleteRows = false;
            this.dg_modbus.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_modbus.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dg_modbus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dg_modbus.BackgroundColor = System.Drawing.Color.White;
            this.dg_modbus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dg_modbus.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_modbus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dg_modbus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_modbus.DefaultCellStyle = dataGridViewCellStyle15;
            this.dg_modbus.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dg_modbus.Location = new System.Drawing.Point(13, 175);
            this.dg_modbus.Name = "dg_modbus";
            this.dg_modbus.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_modbus.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dg_modbus.RowHeadersVisible = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_modbus.RowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dg_modbus.RowTemplate.Height = 24;
            this.dg_modbus.Size = new System.Drawing.Size(900, 86);
            this.dg_modbus.TabIndex = 307;
            this.dg_modbus.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dg_modbus_KeyUp);
            // 
            // bt_guardar
            // 
            this.bt_guardar.BackColor = System.Drawing.Color.SteelBlue;
            this.bt_guardar.CausesValidation = false;
            this.bt_guardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_guardar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_guardar.ForeColor = System.Drawing.Color.White;
            this.bt_guardar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bt_guardar.Location = new System.Drawing.Point(60, 279);
            this.bt_guardar.Margin = new System.Windows.Forms.Padding(4);
            this.bt_guardar.Name = "bt_guardar";
            this.bt_guardar.Size = new System.Drawing.Size(98, 49);
            this.bt_guardar.TabIndex = 306;
            this.bt_guardar.Text = "Guardar cambios";
            this.bt_guardar.UseVisualStyleBackColor = false;
            this.bt_guardar.Click += new System.EventHandler(this.bt_guardar_Click);
            // 
            // cb_escala
            // 
            this.cb_escala.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_escala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_escala.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_escala.Items.AddRange(new object[] {
            "Minutos",
            "Horas",
            "Dias"});
            this.cb_escala.Location = new System.Drawing.Point(208, 87);
            this.cb_escala.Name = "cb_escala";
            this.cb_escala.Size = new System.Drawing.Size(78, 21);
            this.cb_escala.TabIndex = 293;
            // 
            // tb_frecuencia
            // 
            this.tb_frecuencia.Location = new System.Drawing.Point(115, 87);
            this.tb_frecuencia.Name = "tb_frecuencia";
            this.tb_frecuencia.Size = new System.Drawing.Size(43, 20);
            this.tb_frecuencia.TabIndex = 292;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 291;
            this.label2.Text = "Puerto";
            // 
            // cb_puertos
            // 
            this.cb_puertos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_puertos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_puertos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_puertos.Location = new System.Drawing.Point(115, 60);
            this.cb_puertos.Name = "cb_puertos";
            this.cb_puertos.Size = new System.Drawing.Size(171, 21);
            this.cb_puertos.TabIndex = 290;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(9, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 23);
            this.label1.TabIndex = 289;
            this.label1.Text = "Punto";
            // 
            // cb_puntos
            // 
            this.cb_puntos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_puntos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_puntos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_puntos.Location = new System.Drawing.Point(115, 33);
            this.cb_puntos.Name = "cb_puntos";
            this.cb_puntos.Size = new System.Drawing.Size(171, 21);
            this.cb_puntos.TabIndex = 288;
            this.cb_puntos.SelectedIndexChanged += new System.EventHandler(this.cb_puntos_SelectedIndexChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.DarkBlue;
            this.label46.Location = new System.Drawing.Point(9, 86);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(96, 23);
            this.label46.TabIndex = 287;
            this.label46.Text = "Frecuencia";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tb_test);
            this.tabPage3.Controls.Add(this.list_logs);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(927, 411);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Logs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tb_test
            // 
            this.tb_test.Location = new System.Drawing.Point(6, 273);
            this.tb_test.Multiline = true;
            this.tb_test.Name = "tb_test";
            this.tb_test.Size = new System.Drawing.Size(751, 93);
            this.tb_test.TabIndex = 1;
            // 
            // list_logs
            // 
            this.list_logs.FormattingEnabled = true;
            this.list_logs.Location = new System.Drawing.Point(6, 3);
            this.list_logs.Name = "list_logs";
            this.list_logs.Size = new System.Drawing.Size(783, 264);
            this.list_logs.TabIndex = 0;
            // 
            // puertoserial
            // 
            this.puertoserial.PortName = "COM2";
            this.puertoserial.ReadBufferSize = 8096;
            this.puertoserial.ReadTimeout = 2000;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // temporizador
            // 
            this.temporizador.Tick += new System.EventHandler(this.temporizador_Tick);
            // 
            // estado
            // 
            this.estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status1,
            this.toolStripStatusLabel1,
            this.estado1,
            this.estado2,
            this.estado3});
            this.estado.Location = new System.Drawing.Point(0, 473);
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(944, 22);
            this.estado.TabIndex = 319;
            this.estado.Text = "statusStrip1";
            // 
            // status1
            // 
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // estado1
            // 
            this.estado1.Name = "estado1";
            this.estado1.Size = new System.Drawing.Size(118, 17);
            this.estado1.Text = "toolStripStatusLabel2";
            // 
            // estado2
            // 
            this.estado2.Name = "estado2";
            this.estado2.Size = new System.Drawing.Size(118, 17);
            this.estado2.Text = "toolStripStatusLabel2";
            // 
            // estado3
            // 
            this.estado3.Name = "estado3";
            this.estado3.Size = new System.Drawing.Size(118, 17);
            this.estado3.Text = "toolStripStatusLabel2";
            // 
            // cb_intentos
            // 
            this.cb_intentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_intentos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_intentos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_intentos.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cb_intentos.Location = new System.Drawing.Point(631, 37);
            this.cb_intentos.Name = "cb_intentos";
            this.cb_intentos.Size = new System.Drawing.Size(78, 21);
            this.cb_intentos.TabIndex = 320;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkBlue;
            this.label8.Location = new System.Drawing.Point(528, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 23);
            this.label8.TabIndex = 319;
            this.label8.Text = "Intentos";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(944, 495);
            this.Controls.Add(this.estado);
            this.Controls.Add(this.tabControl1);
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Telemetria";
            this.Load += new System.EventHandler(this.main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_acumulado)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_hora)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_dia)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_modbus)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.estado.ResumeLayout(false);
            this.estado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_puertos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_puntos;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.ComboBox cb_escala;
        private System.Windows.Forms.TextBox tb_frecuencia;
        private System.Windows.Forms.Button bt_guardar;
        private System.IO.Ports.SerialPort puertoserial;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.DataGridView dg_modbus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cb_rata;
        private System.Windows.Forms.TextBox tb_timeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_id;
        private System.Windows.Forms.StatusStrip estado;
        private System.Windows.Forms.ToolStripStatusLabel status1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridView dg_acumulado;
        private System.Windows.Forms.DataGridView dg_hora;
        private System.Windows.Forms.DataGridView dg_dia;
        private System.Windows.Forms.ToolStripStatusLabel estado1;
        private System.Windows.Forms.ToolStripStatusLabel estado2;
        private System.Windows.Forms.ToolStripStatusLabel estado3;
        public System.Windows.Forms.Timer temporizador;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox list_logs;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_test;
        private System.Windows.Forms.ComboBox cb_intentos;
        private System.Windows.Forms.Label label8;
    }
}

