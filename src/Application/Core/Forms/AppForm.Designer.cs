namespace Application.Core.Forms;

partial class AppForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CodeEditor = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ButtonRun = new System.Windows.Forms.Button();
            this.ButtonStep = new System.Windows.Forms.Button();
            this.ButtonAssemble = new System.Windows.Forms.Button();
            this.ButtomReset = new System.Windows.Forms.Button();
            this.RightFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LeftVerticalFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.RegistersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LabelRegisters = new System.Windows.Forms.Label();
            this.FlagsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.ClockComboBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.MemoryPanelContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.MemoryPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnMemoryPreviousPage = new System.Windows.Forms.Button();
            this.MemoryPageBox = new System.Windows.Forms.TextBox();
            this.BtnMemoryNextPage = new System.Windows.Forms.Button();
            this.OutputPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OutputLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.RightFlowPanel.SuspendLayout();
            this.LeftVerticalFlowPanel.SuspendLayout();
            this.RegistersPanel.SuspendLayout();
            this.FlagsPanel.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.MemoryPanelContainer.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.OutputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.CodeEditor);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.RightFlowPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 721);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 0;
            // 
            // CodeEditor
            // 
            this.CodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeEditor.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CodeEditor.Location = new System.Drawing.Point(0, 29);
            this.CodeEditor.Multiline = true;
            this.CodeEditor.Name = "CodeEditor";
            this.CodeEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CodeEditor.Size = new System.Drawing.Size(500, 692);
            this.CodeEditor.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.ButtonRun);
            this.flowLayoutPanel1.Controls.Add(this.ButtonStep);
            this.flowLayoutPanel1.Controls.Add(this.ButtonAssemble);
            this.flowLayoutPanel1.Controls.Add(this.ButtomReset);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(500, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ButtonRun
            // 
            this.ButtonRun.Location = new System.Drawing.Point(3, 3);
            this.ButtonRun.Name = "ButtonRun";
            this.ButtonRun.Size = new System.Drawing.Size(75, 23);
            this.ButtonRun.TabIndex = 0;
            this.ButtonRun.Text = "Run";
            this.ButtonRun.UseVisualStyleBackColor = true;
            this.ButtonRun.Click += new System.EventHandler(this.ButtonRun_Click);
            // 
            // ButtonStep
            // 
            this.ButtonStep.Location = new System.Drawing.Point(84, 3);
            this.ButtonStep.Name = "ButtonStep";
            this.ButtonStep.Size = new System.Drawing.Size(75, 23);
            this.ButtonStep.TabIndex = 1;
            this.ButtonStep.Text = "Step";
            this.ButtonStep.UseVisualStyleBackColor = true;
            this.ButtonStep.Click += new System.EventHandler(this.ButtonStep_Click);
            // 
            // ButtonAssemble
            // 
            this.ButtonAssemble.Location = new System.Drawing.Point(165, 3);
            this.ButtonAssemble.Name = "ButtonAssemble";
            this.ButtonAssemble.Size = new System.Drawing.Size(75, 23);
            this.ButtonAssemble.TabIndex = 2;
            this.ButtonAssemble.Text = "Assemble";
            this.ButtonAssemble.UseVisualStyleBackColor = true;
            this.ButtonAssemble.Click += new System.EventHandler(this.ButtonAssemble_Click);
            // 
            // ButtomReset
            // 
            this.ButtomReset.Location = new System.Drawing.Point(246, 3);
            this.ButtomReset.Name = "ButtomReset";
            this.ButtomReset.Size = new System.Drawing.Size(75, 23);
            this.ButtomReset.TabIndex = 3;
            this.ButtomReset.Text = "Reset";
            this.ButtomReset.UseVisualStyleBackColor = true;
            this.ButtomReset.Click += new System.EventHandler(this.ButtomReset_Click);
            // 
            // RightFlowPanel
            // 
            this.RightFlowPanel.Controls.Add(this.LeftVerticalFlowPanel);
            this.RightFlowPanel.Controls.Add(this.flowLayoutPanel2);
            this.RightFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightFlowPanel.Location = new System.Drawing.Point(0, 0);
            this.RightFlowPanel.Name = "RightFlowPanel";
            this.RightFlowPanel.Size = new System.Drawing.Size(760, 721);
            this.RightFlowPanel.TabIndex = 0;
            // 
            // LeftVerticalFlowPanel
            // 
            this.LeftVerticalFlowPanel.AutoSize = true;
            this.LeftVerticalFlowPanel.Controls.Add(this.RegistersPanel);
            this.LeftVerticalFlowPanel.Controls.Add(this.FlagsPanel);
            this.LeftVerticalFlowPanel.Controls.Add(this.flowLayoutPanel4);
            this.LeftVerticalFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.LeftVerticalFlowPanel.Location = new System.Drawing.Point(3, 3);
            this.LeftVerticalFlowPanel.MinimumSize = new System.Drawing.Size(100, 200);
            this.LeftVerticalFlowPanel.Name = "LeftVerticalFlowPanel";
            this.LeftVerticalFlowPanel.Size = new System.Drawing.Size(106, 266);
            this.LeftVerticalFlowPanel.TabIndex = 1;
            // 
            // RegistersPanel
            // 
            this.RegistersPanel.AutoSize = true;
            this.RegistersPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RegistersPanel.Controls.Add(this.LabelRegisters);
            this.RegistersPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.RegistersPanel.Location = new System.Drawing.Point(3, 3);
            this.RegistersPanel.MinimumSize = new System.Drawing.Size(100, 100);
            this.RegistersPanel.Name = "RegistersPanel";
            this.RegistersPanel.Size = new System.Drawing.Size(100, 100);
            this.RegistersPanel.TabIndex = 0;
            // 
            // LabelRegisters
            // 
            this.LabelRegisters.AutoSize = true;
            this.LabelRegisters.Location = new System.Drawing.Point(3, 0);
            this.LabelRegisters.Name = "LabelRegisters";
            this.LabelRegisters.Size = new System.Drawing.Size(54, 15);
            this.LabelRegisters.TabIndex = 0;
            this.LabelRegisters.Text = "Registers";
            // 
            // FlagsPanel
            // 
            this.FlagsPanel.AutoSize = true;
            this.FlagsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FlagsPanel.Controls.Add(this.label1);
            this.FlagsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlagsPanel.Location = new System.Drawing.Point(3, 109);
            this.FlagsPanel.MinimumSize = new System.Drawing.Size(100, 100);
            this.FlagsPanel.Name = "FlagsPanel";
            this.FlagsPanel.Size = new System.Drawing.Size(100, 100);
            this.FlagsPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Flags";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel4.Controls.Add(this.label3);
            this.flowLayoutPanel4.Controls.Add(this.ClockComboBox);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 215);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(100, 48);
            this.flowLayoutPanel4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Clock";
            // 
            // ClockComboBox
            // 
            this.ClockComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClockComboBox.FormattingEnabled = true;
            this.ClockComboBox.Location = new System.Drawing.Point(3, 18);
            this.ClockComboBox.Name = "ClockComboBox";
            this.ClockComboBox.Size = new System.Drawing.Size(90, 23);
            this.ClockComboBox.TabIndex = 1;
            this.ClockComboBox.SelectedIndexChanged += new System.EventHandler(this.ClockComboBox_SelectedIndexChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.MemoryPanelContainer);
            this.flowLayoutPanel2.Controls.Add(this.OutputPanel);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(115, 3);
            this.flowLayoutPanel2.MinimumSize = new System.Drawing.Size(100, 200);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(181, 224);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // MemoryPanelContainer
            // 
            this.MemoryPanelContainer.AutoSize = true;
            this.MemoryPanelContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MemoryPanelContainer.Controls.Add(this.label2);
            this.MemoryPanelContainer.Controls.Add(this.MemoryPanel);
            this.MemoryPanelContainer.Controls.Add(this.flowLayoutPanel3);
            this.MemoryPanelContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.MemoryPanelContainer.Location = new System.Drawing.Point(3, 3);
            this.MemoryPanelContainer.MinimumSize = new System.Drawing.Size(100, 100);
            this.MemoryPanelContainer.Name = "MemoryPanelContainer";
            this.MemoryPanelContainer.Size = new System.Drawing.Size(175, 112);
            this.MemoryPanelContainer.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Memory";
            // 
            // MemoryPanel
            // 
            this.MemoryPanel.AutoSize = true;
            this.MemoryPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.MemoryPanel.Location = new System.Drawing.Point(3, 18);
            this.MemoryPanel.MinimumSize = new System.Drawing.Size(80, 50);
            this.MemoryPanel.Name = "MemoryPanel";
            this.MemoryPanel.Size = new System.Drawing.Size(80, 50);
            this.MemoryPanel.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.BtnMemoryPreviousPage);
            this.flowLayoutPanel3.Controls.Add(this.MemoryPageBox);
            this.flowLayoutPanel3.Controls.Add(this.BtnMemoryNextPage);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 74);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(165, 31);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // BtnMemoryPreviousPage
            // 
            this.BtnMemoryPreviousPage.Location = new System.Drawing.Point(3, 3);
            this.BtnMemoryPreviousPage.Name = "BtnMemoryPreviousPage";
            this.BtnMemoryPreviousPage.Size = new System.Drawing.Size(42, 23);
            this.BtnMemoryPreviousPage.TabIndex = 0;
            this.BtnMemoryPreviousPage.Text = "<<";
            this.BtnMemoryPreviousPage.UseVisualStyleBackColor = true;
            this.BtnMemoryPreviousPage.Click += new System.EventHandler(this.BtnMemoryPreviousPage_Click);
            // 
            // MemoryPageBox
            // 
            this.MemoryPageBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MemoryPageBox.Location = new System.Drawing.Point(51, 3);
            this.MemoryPageBox.Name = "MemoryPageBox";
            this.MemoryPageBox.ReadOnly = true;
            this.MemoryPageBox.Size = new System.Drawing.Size(63, 25);
            this.MemoryPageBox.TabIndex = 1;
            this.MemoryPageBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BtnMemoryNextPage
            // 
            this.BtnMemoryNextPage.Location = new System.Drawing.Point(120, 3);
            this.BtnMemoryNextPage.Name = "BtnMemoryNextPage";
            this.BtnMemoryNextPage.Size = new System.Drawing.Size(42, 23);
            this.BtnMemoryNextPage.TabIndex = 2;
            this.BtnMemoryNextPage.Text = ">>";
            this.BtnMemoryNextPage.UseVisualStyleBackColor = true;
            this.BtnMemoryNextPage.Click += new System.EventHandler(this.BtnMemoryNextPage_Click);
            // 
            // OutputPanel
            // 
            this.OutputPanel.AutoSize = true;
            this.OutputPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OutputPanel.Controls.Add(this.OutputLabel);
            this.OutputPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.OutputPanel.Location = new System.Drawing.Point(3, 121);
            this.OutputPanel.MinimumSize = new System.Drawing.Size(100, 100);
            this.OutputPanel.Name = "OutputPanel";
            this.OutputPanel.Size = new System.Drawing.Size(133, 100);
            this.OutputPanel.TabIndex = 1;
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(3, 0);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(123, 15);
            this.OutputLabel.TabIndex = 0;
            this.OutputLabel.Text = "Output (8000h-805Fh)";
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 721);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "AppForm";
            this.Text = "Intel 8086";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.RightFlowPanel.ResumeLayout(false);
            this.RightFlowPanel.PerformLayout();
            this.LeftVerticalFlowPanel.ResumeLayout(false);
            this.LeftVerticalFlowPanel.PerformLayout();
            this.RegistersPanel.ResumeLayout(false);
            this.RegistersPanel.PerformLayout();
            this.FlagsPanel.ResumeLayout(false);
            this.FlagsPanel.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.MemoryPanelContainer.ResumeLayout(false);
            this.MemoryPanelContainer.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.OutputPanel.ResumeLayout(false);
            this.OutputPanel.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox CodeEditor;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button ButtonRun;
    private System.Windows.Forms.Button ButtonStep;
    private System.Windows.Forms.Button ButtonAssemble;
    private System.Windows.Forms.Button ButtomReset;
    private System.Windows.Forms.FlowLayoutPanel RightFlowPanel;
    private System.Windows.Forms.FlowLayoutPanel RegistersPanel;
    private System.Windows.Forms.Label LabelRegisters;
    private System.Windows.Forms.FlowLayoutPanel LeftVerticalFlowPanel;
    private System.Windows.Forms.FlowLayoutPanel FlagsPanel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.FlowLayoutPanel MemoryPanelContainer;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.FlowLayoutPanel OutputPanel;
    private System.Windows.Forms.Label OutputLabel;
    private System.Windows.Forms.FlowLayoutPanel MemoryPanel;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
    private System.Windows.Forms.Button BtnMemoryPreviousPage;
    private System.Windows.Forms.TextBox MemoryPageBox;
    private System.Windows.Forms.Button BtnMemoryNextPage;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox ClockComboBox;
}
