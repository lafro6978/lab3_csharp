using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Series;
using wpflab3.Common;

namespace wpflab3
{
    public partial class MainWindow : Window
    {
        private UndoRedoManager _undoRedoManager = new UndoRedoManager();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.Undo();
        }

        private void BtnRedo_Click(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.Redo();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string oldText = TxtInputData.Text;
                    PlotModel oldModel = MainPlotView.Model;

                    string filePath = openFileDialog.FileName;
                    InputData inputData = InputDataLoader.Load(filePath);

                    string newText = $"Name = {filePath}\n" +
                                     $"Mass = {inputData.Mass},\n" +
                                     $"Stiffness = {inputData.Stiffness},\n" +
                                     $"Damping = {inputData.Damping},\n" +
                                     $"X0 = {inputData.X0},\n" +
                                     $"V0 = {inputData.V0}";

                    Solver solver = new Solver();
                    var result = solver.Solve(inputData);

                    var plotModel = new PlotModel { Title = "Результат расчета" };
                    var series = new LineSeries { Title = result.Title };

                    for (int i = 0; i < result.XValues.Length; i++)
                    {
                        series.Points.Add(new DataPoint(result.XValues[i], result.YValues[i]));
                    }
                    plotModel.Series.Add(series); // [cite: 28]

                    Action applyChanges = () =>
                    {
                        TxtInputData.Text = newText;
                        MainPlotView.Model = plotModel;
                    };

                    Action revertChanges = () =>
                    {
                        TxtInputData.Text = oldText;
                        MainPlotView.Model = oldModel;
                    };

                    applyChanges();
                    _undoRedoManager.AddUndoRedo(revertChanges, applyChanges);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}