using Application.Core.Controls;
using Application.Core.Misc;
using Application.Properties;
using Emulator.Core.Components.Registers;
using Emulator.Core.Utils;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace Application.Core.Forms;

public partial class AppForm
{
    private void Initialize()
    {
        InitializeComponent();
        InitializeRegisters();
        InitializeFlags();
        InitializeMemory();
        InitializeOutputPanel();
        InitializeExampleCode();
        InitializeClockBox();
        UpdateMemoryPageBox();
    }

    private void InitializeRegisters()
    {
        RegistersPanel.SuspendLayout();

        var registers = new Dictionary<string, WordRegisterComponent>()
        {
            { "AX", _vm.Registers.GeneralA },
            { "BX", _vm.Registers.GeneralB },
            { "CX", _vm.Registers.GeneralC },
            { "DX", _vm.Registers.GeneralD },
            { "IP", _vm.Registers.InstructionPointer },
            { "SP", _vm.Registers.StackPointer },
            { "BP", _vm.Registers.BasePointer },
            { "DI", _vm.Registers.DestinationIndex },
            { "SI", _vm.Registers.SourceIndex },
            { "CS", _vm.Registers.CodeSegment },
            { "DS", _vm.Registers.DataSegment },
            { "SS", _vm.Registers.StackSegment },
            { "ES", _vm.Registers.ExtraSegment },
        };

        foreach (var reg in registers)
        {
            var label = new Label()
            {
                Text = reg.Key,
                Size = new(20, 20),
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var text = new TextBoxInteractive(() => FormatUtils.ToHex(reg.Value.Value))
            {
                ReadOnly = true,
                Font = Fonts.ConsolasNormal,
                Size = new(50, 20),
            };

            _interactives.Add(text);

            var container = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
            };

            container.Controls.Add(label);
            container.Controls.Add(text);

            RegistersPanel.Controls.Add(container);
        }

        RegistersPanel.ResumeLayout(false);
    }

    private void InitializeFlags()
    {
        FlagsPanel.SuspendLayout();

        var flags = new Dictionary<string, Func<bool>>()
        {
            { "CF", () => _vm.Registers.Flags.Carry },
            { "OF", () => _vm.Registers.Flags.Overflow },
            { "ZF", () => _vm.Registers.Flags.Zero },
            { "SF", () => _vm.Registers.Flags.Sign },
            { "PF", () => _vm.Registers.Flags.Parity },
            { "DF", () => _vm.Registers.Flags.Direction },
        };

        foreach (var flag in flags)
        {
            var label = new Label()
            {
                Text = flag.Key,
                Size = new(20, 20),
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var text = new TextBoxInteractive(() => flag.Value() ? "TRUE" : "FALSE")
            {
                ReadOnly = true,
                Font = Fonts.ConsolasNormal,
                Size = new(50, 20),
            };

            _interactives.Add(text);

            var container = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
            };

            container.Controls.Add(label);
            container.Controls.Add(text);

            FlagsPanel.Controls.Add(container);
        }

        FlagsPanel.ResumeLayout(false);
    }

    private void InitializeMemory()
    {
        MemoryPanel.SuspendLayout();

        var container = new FlowLayoutPanel()
        {
            FlowDirection = FlowDirection.TopDown,
            AutoSize = true,
        };

        {
            var row = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Margin = new(0),
            };

            var rowLabel = new Label()
            {
                Size = new(40, 20),
                Text = "",
            };
            row.Controls.Add(rowLabel);

            for (int x = 0; x < _memoryView.BytesPerRow; x++)
            {
                var text = new Label()
                {
                    Text = FormatUtils.ToHex((uint)x, 1),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new(19, 20),
                };

                row.Controls.Add(text);
            }

            container.Controls.Add(row);
        }

        for (int y = 0; y < _memoryView.Rows; y++)
        {
            int scopedY = y;

            var row = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Margin = new(0),
            };

            var rowLabel = new LabelInteractive(() => FormatUtils.ToHex((byte)_memoryView.Page) + FormatUtils.ToHex((byte)scopedY, 1) + "...")
            {
                Size = new(40, 20),
            };
            row.Controls.Add(rowLabel);
            _interactives.Add(rowLabel);

            for (int x = 0; x < _memoryView.BytesPerRow; x++)
            {
                int scopedX = x;

                var text = new TextBoxInteractive(() => FormatUtils.ToHex(_vm.Memory.GetByte((ushort)(_memoryView.Page * _memoryView.BytesPerPage + scopedY * _memoryView.BytesPerRow + scopedX))))
                {
                    ReadOnly = true,
                    Font = Fonts.ConsolasNormal,
                    Size = new(25, 20),
                    TextAlign = HorizontalAlignment.Center,
                    Margin = new(0),
                };

                _interactives.Add(text);

                row.Controls.Add(text);
            }

            container.Controls.Add(row);
        }

        MemoryPanel.Controls.Add(container);
        MemoryPanel.ResumeLayout(false);
    }

    private void InitializeOutputPanel()
    {
        OutputPanel.SuspendLayout();

        var container = new FlowLayoutPanel()
        {
            FlowDirection = FlowDirection.TopDown,
            AutoSize = true,
        };

        for (int y = 0; y < 6; y++)
        {
            var row = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Margin = new(0),
            };

            for (int x = 0; x < 16; x++)
            {
                int scopedX = x;
                int scopedY = y;

                var text = new TextBoxInteractive(() => ((char)(_vm.Memory.GetByte((ushort)(0x8000 + scopedY * 16 + scopedX)))).ToString())
                {
                    ReadOnly = true,
                    Font = Fonts.ConsolasNormal,
                    Size = new(20, 20),
                    TextAlign = HorizontalAlignment.Center,
                    Margin = new(0),
                };

                _interactives.Add(text);

                row.Controls.Add(text);
            }

            container.Controls.Add(row);
        }

        OutputPanel.Controls.Add(container);
        OutputPanel.ResumeLayout(false);
    }

    private void InitializeExampleCode()
    {
        CodeEditor.Text = Resources.ExampleHello;
    }

    private void InitializeClockBox()
    {
        List<KeyValuePair<int, string>> options = new()
        {
            new(0, "1 Hz"),
            new(1, "2 Hz"),
            new(2, "4 Hz"),
            new(3, "8 Hz"),
            new(4, "16 Hz"),
            new(5, "32 Hz"),
            new(6, "64 Hz"),
            new(7, "Unlimited"),
        };

        ClockComboBox.DataSource = options;
        ClockComboBox.DisplayMember = "Value";
        ClockComboBox.ValueMember = "Key";
        ClockComboBox.SelectedIndex = 6;
    }
}
