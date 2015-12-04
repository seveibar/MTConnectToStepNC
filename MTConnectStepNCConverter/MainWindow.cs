﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTConnectAnalyzer.Analysis;
using System.Threading;
using System.Windows.Threading;

namespace MTConnectStepNCConverter
{
    public partial class MainWindow : Form
    {
        String inputPath;
        String writeLocationPath;
        Analysis analysis;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void browseForMTConnectFile(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            inputPath = openFileDialog1.FileName;
            input_field.Text = openFileDialog1.FileName;
        }

        private void browseForSteoNCOutput(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            writeLocationPath = saveFileDialog1.FileName;
            output_field.Text = saveFileDialog1.FileName;
        }

        private void analyzeMTConnect(object sender, EventArgs e)
        {
            analysisWorker.RunWorkerAsync();
        }

        private void analysisWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MTConnectAnalyzer.Log.outputTo = MTConnectAnalyzer.Log.OutputMode.LogWriter;
            MTConnectAnalyzer.Log.logwriter = new TextBoxLogWriter(sender as BackgroundWorker);
            analysis = new Analysis(inputPath);
        }

        private void analysisWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            info.AppendText(Environment.NewLine + e.UserState);
        }

        private void analysisWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }


    internal class TextBoxLogWriter : MTConnectAnalyzer.LogWriter
    {
        BackgroundWorker worker;
        internal TextBoxLogWriter(BackgroundWorker w)
        {
            this.worker = w;
        }

        public override void Write(object str)
        {
            worker.ReportProgress(0, str.ToString());
        }
    }
}
