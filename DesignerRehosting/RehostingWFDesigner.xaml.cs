//-------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved
//-------------------------------------------------------------------

using System;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.IO;
using System.Windows;
using DesignerRehosting.Properties;
using System.Activities.Presentation.Services;
using SimpleActivities;
using System.Collections.Generic;

namespace DesignerRehosting
{
    public partial class RehostingWfDesigner : Window
    {
        Activity _currentWorkflow = null;
        WorkflowDesigner _wd = null;
        private string _currentProgramFilePath = null;
        private bool _isCurrentProgramStored = false;
        private string _runningWorkflowTemporaryFileName;

        public RehostingWfDesigner()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            // register metadata
            (new DesignerMetadata()).Register();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var lastProgramName = Settings.Default.LastProgramName;
            if (String.IsNullOrEmpty(lastProgramName))
            {
                NewWorkflow();
            }
            else
            {
                try
                {
                    var lastProgramPath = Path.Combine(GetProgramDirPath(), lastProgramName);
                    if (File.Exists(lastProgramPath))
                    {
                        OpenProgram(lastProgramPath);
                    }
                    else
                    {
                        NewWorkflow();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Не удалось загрузить программу: " + lastProgramName);
                    NewWorkflow();
                }
            }

            WorkflowExecutionLog.WorkflowOutputEvent += (senderActivity, text) =>
            {
                InOutTextBlock.Text += (text + Environment.NewLine);
            };
        }

        private void NewWorkflow()
        {
            // create the workflow designer
            _currentWorkflow = new Flowchart();
            _currentWorkflow.DisplayName = "Блок-схема";
            RecreateWorkflowDesigner(_currentWorkflow);
            _currentProgramFilePath = null;
            _isCurrentProgramStored = true;
            //System.Activities.Statements.
        }

        private void RecreateWorkflowDesigner(Activity workflow)
        {
            _wd = new WorkflowDesigner();
            DesignerBorder.Child = _wd.View;
            PropertyBorder.Child = _wd.PropertyInspectorView;
            _wd.Load(workflow);
            _wd.TextChanged += _wd_TextChanged;
            _wd.ModelChanged += _wd_ModelChanged;

            var modelService = _wd.Context.Services.GetService<ModelService>();
            if (modelService != null)
            {
                modelService.ModelChanged += new EventHandler<ModelChangedEventArgs>(RehostingWfDesigner_ModelChanged);
            }
        }

        void _wd_ModelChanged(object sender, EventArgs e)
        {
            _isCurrentProgramStored = false;
        }

        void RehostingWfDesigner_ModelChanged(object sender, ModelChangedEventArgs e)
        {
            _isCurrentProgramStored = false;

            var localaizedDisplayNames = new Dictionary<Type, string>
            {
                {typeof(ExpressionMessage), "Сообщение - выражение"},
                {typeof(SimpleMessage), "Простое сообщение"},
                {typeof(RandomNumber),  "Случайное число"},
                {typeof(InputNumber),  "Ввести число"},
                {typeof(Assign),  "Присвоить"},
                {typeof(InputExpressionNumber),  "Ввести число"},
            };

            if (e != null && e.ItemsAdded != null)
            {
                foreach (System.Activities.Presentation.Model.ModelItem item in e.ItemsAdded)
                {
                    // Локализация стандартных активити при вставке
                    if (item.ItemType == typeof(FlowDecision))
                    {
                        item.Properties["DisplayName"].SetValue("Условие");
                        item.Properties["TrueLabel"].SetValue("Да");
                        item.Properties["FalseLabel"].SetValue("Нет");
                    }
                    else if (item.ItemType == typeof(FlowStep))
                    {
                        if (item.Content != null)
                        {
                            string newDisplayName = null;
                            localaizedDisplayNames.TryGetValue(item.Content.ComputedValue.GetType(), out newDisplayName);
                            if (!string.IsNullOrEmpty(newDisplayName))
                            {
                                (item.Content.ComputedValue as Activity).DisplayName = newDisplayName;
                            }
                        }
                    }
                }
            }
        }

        void _wd_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _isCurrentProgramStored = false;
        }

        #region Методы работы с файлами и каталогами
        string GetProgramDirPath()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var path = System.IO.Path.Combine(currentPath, "Программы");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        string GetOpenFileName()
        {
            var path = GetProgramDirPath();

            var openFileDialog =
                new System.Windows.Forms.OpenFileDialog
                {
                    Title = "Укажите файл с программой",
                    InitialDirectory = path,
                    Filter = "|*.xaml",
                    DefaultExt = "*.xaml"
                };

            var openFileDialogResult = openFileDialog.ShowDialog();
            if (openFileDialogResult == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }

        string GetSaveFileName()
        {
            var path = GetProgramDirPath();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var saveFileDialog =
                new System.Windows.Forms.SaveFileDialog
                {
                    Title = "Укажите файл для сохранения программы",
                    InitialDirectory = path,
                    Filter = "|*.xaml", 
                    OverwritePrompt = false,
                    DefaultExt = "*.xaml"
                };

            var openFileDialogResult = saveFileDialog.ShowDialog();
            if (openFileDialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!File.Exists(saveFileDialog.FileName))
                {
                    return saveFileDialog.FileName;
                }
                else
                {
                    var shortFileName = Path.GetFileName(saveFileDialog.FileName);
                    var mbResult =
                        MessageBox.Show(
                            string.Format("Вы действительно хотите записать свою новую программу в файл '{0}'", shortFileName),
                            "Внимание!",
                            MessageBoxButton.YesNoCancel);

                    switch (mbResult)
                    {
                        case MessageBoxResult.No:
                            // Вызовем себя еще раз, чтобы переспросить имя файла
                            return GetSaveFileName();

                        case MessageBoxResult.Yes:
                            return saveFileDialog.FileName;

                        case MessageBoxResult.None:
                        case MessageBoxResult.Cancel:
                        default:
                            return null;
                    }
                }
            }

            return null;
        }

        void OpenProgram(string filePath)
        {
            _currentWorkflow = ActivityXamlServices.Load(filePath);
            RecreateWorkflowDesigner(_currentWorkflow);
            _currentProgramFilePath = filePath;
            _isCurrentProgramStored = true;
            StoreCurrentProgramName();
        }

        private void SaveProgram()
        {
            if (!string.IsNullOrEmpty(_currentProgramFilePath))
            {
                _wd.Save(_currentProgramFilePath);
                _isCurrentProgramStored = true;
                StoreCurrentProgramName();
            }
            else
            {
                SaveProgramAs();
            }
        }

        private void StoreCurrentProgramName()
        {
            var programDirPath = GetProgramDirPath();
            if (_currentProgramFilePath.StartsWith(programDirPath))
            {
                var currentProgramFileName = Path.GetFileName(_currentProgramFilePath);
                Settings.Default.LastProgramName = currentProgramFileName;
                Settings.Default.Save();
            }
        }

        private void SaveProgramAs()
        {
            _currentProgramFilePath = GetSaveFileName();
            if (!string.IsNullOrEmpty(_currentProgramFilePath))
            {
                SaveProgram();
            }
        }
        #endregion


        #region Обработчики команд
        private void CommandNew_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isCurrentProgramStored;
        }

        private void CommandNew_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            NewWorkflow();
        }

        private void CommandOpen_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var filePath = GetOpenFileName();
            if (string.IsNullOrEmpty(filePath)) return;
            OpenProgram(filePath);
        }

        //private void CommandSave_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = !string.IsNullOrEmpty(_currentProgramFilePath);
        //}

        private void CommandSave_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveProgram();
        }

        private void CommandSaveAs_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveProgramAs();
        }

        private void CommandRun_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (_wd != null && !_wd.IsInErrorState());
        }

        private void CommandRun_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            InOutTextBlock.Text = string.Empty;

            // Для того чтобы текущее Workflow безглючно запускалось, нужно сохранить его в файл,
            // загрузить файл в другое Workflow и уже запускать 
            if (String.IsNullOrEmpty(_runningWorkflowTemporaryFileName))
            {
                var currentPath = Directory.GetCurrentDirectory();
                _runningWorkflowTemporaryFileName = System.IO.Path.Combine(currentPath, "RunningWorkflow.xaml");
            }
            _wd.Save(_runningWorkflowTemporaryFileName);
            var runningWorkflow = ActivityXamlServices.Load(_runningWorkflowTemporaryFileName);

            using (var textWriter = new StringWriter())
            {
                Console.SetOut(textWriter);

                var wi = new WorkflowInvoker(runningWorkflow);
                wi.Invoke();

                var textOutput = textWriter.ToString();
                if (!string.IsNullOrEmpty(textOutput))
                {
                    MessageBox.Show(textOutput, "Вывод программы:");
                }
            }
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FlowDecision f;
            e.Cancel = false;

            if (!_isCurrentProgramStored)
            {
                var mbResult = MessageBox.Show("Программа не сохранена. Сохранить?", "Внимание", MessageBoxButton.YesNoCancel);
                switch (mbResult)
                {
                    case MessageBoxResult.Yes:
                        if (String.IsNullOrEmpty(_currentProgramFilePath))
                        {
                            SaveProgramAs();
                        }
                        else
                        {
                            SaveProgram();
                        }
                        break;

                    case MessageBoxResult.No:
                        break;

                    case MessageBoxResult.Cancel:
                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
