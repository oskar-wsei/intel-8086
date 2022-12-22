using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Application.Core.Controls;
using Application.Core.Misc;
using Assembler.Core.Assembly;
using Emulator.Core;

namespace Application.Core.Forms;

public partial class AppForm : Form
{
    private readonly List<IControlInteractive> _interactives = new();
    private Dictionary<int, int> _sourceMap = new();

    private readonly AssemblyCompiler _compiler;
    private readonly VirtualMachine _vm;
    private readonly MemoryView _memoryView;

    private Thread? _runThread;
    private bool _isRunning = false;
    private int _clockSelectedIndex = 2;

    public AppForm()
    {
        _compiler = new AssemblyCompiler();
        _vm = new VirtualMachine();
        _vm.Halt();
        _memoryView = new MemoryView((int)_vm.Memory.Size);

        Initialize();
        DisableRunStepButtons();
    }

    private void ButtonRun_Click(object sender, EventArgs e)
    {
        if (!_isRunning)
        {
            _isRunning = true;

            _runThread = new Thread(() =>
            {
                var timer = new Stopwatch();
                timer.Start();

                while (_isRunning)
                {
                    var timePerCycle = (long)((1.0f / GetCyclesPerSecondFromComboBox()) * 1000.0f);

                    if (timer.ElapsedMilliseconds >= timePerCycle)
                    {
                        Step();
                        timer.Restart();
                    }
                }

                timer.Stop();
            });

            _runThread.Start();
        }
        else
        {
            _isRunning = false;
            _runThread = null;
        }

        UpdateUI();
    }

    private void ButtonStep_Click(object sender, EventArgs e)
    {
        Step();
    }

    private void Step()
    {
        TryOrShowAlert(() => _vm.Step());

        if (_vm.Halted)
        {
            _isRunning = false;

            BeginInvoke(DisableRunStepButtons);
        }

        UpdateUI();
    }

    private void DisableRunStepButtons()
    {
        ButtonRun.Enabled = false;
        ButtonStep.Enabled = false;
    }

    private void EnableRunStepButtons()
    {
        ButtonRun.Enabled = true;
        ButtonStep.Enabled = true;
    }

    private void ButtonAssemble_Click(object sender, EventArgs e)
    {
        TryOrShowAlert(delegate ()
        {
            Reset();
            var bundle = _compiler.Compile(CodeEditor.Text);
            _vm.LoadProgram(bundle.Code);
            _sourceMap = bundle.SourceMap;
        });

        UpdateUI();
    }

    private void ButtomReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    private void Reset()
    {
        _vm.Reset();

        Invoke(EnableRunStepButtons);

        UpdateUI();
    }

    private void UpdateUI()
    {
        BeginInvoke(() =>
        {
            foreach (var box in _interactives)
            {
                box.UpdateValue();
            }

            var ip = _vm.Registers.InstructionPointer.Value;

            ButtonRun.Text = _isRunning ? "Stop" : "Run";

            if (_sourceMap.ContainsKey(ip))
            {
                CodeEditorSelectLine(_sourceMap[ip]);
            }

            UpdateMemoryPageBox();
        });
    }

    private void CodeEditorSelectLine(int line)
    {
        if (line < 0) return;
        var start = CodeEditor.GetFirstCharIndexFromLine(line - 1);
        if (start < 0) return;
        var stop = CodeEditor.Text.IndexOf(Environment.NewLine, start);
        if (stop < 0) stop = CodeEditor.Text.Length;

        CodeEditor.Focus();
        CodeEditor.Select(start, stop - start);
        CodeEditor.ScrollToCaret();
    }

    private static void TryOrShowAlert(Action action)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private void BtnMemoryPreviousPage_Click(object sender, EventArgs e)
    {
        _memoryView.PreviousPage();
        UpdateUI();
    }

    private void BtnMemoryNextPage_Click(object sender, EventArgs e)
    {
        _memoryView.NextPage();
        UpdateUI();
    }

    private int GetCyclesPerSecondFromComboBox()
    {
        return _clockSelectedIndex == 7 ? int.MaxValue : (int)Math.Pow(2, _clockSelectedIndex);
    }

    private void ClockComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        _clockSelectedIndex = ClockComboBox.SelectedIndex;
    }

    private void UpdateMemoryPageBox()
    {
        MemoryPageBox.Text = $"{_memoryView.Page + 1}/{_memoryView.Pages}";
    }
}
