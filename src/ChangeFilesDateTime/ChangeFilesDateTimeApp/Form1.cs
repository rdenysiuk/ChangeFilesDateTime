using CFDT.Primitives;
using ChangeFilesDateTimeApp.Config;
using ChangeFilesDateTimeApp.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChangeFilesDateTimeApp
{
    public partial class Form1 : Form
    {
        #region FIELDS
        DateTimeChange _dateTimeChanger;
        LogInfo _logInfo;
        DateTimeFromFileName _fileName;
        #endregion
        public Form1()
        {
            InitializeComponent();
            gridView.AutoGenerateColumns = false;

            _logInfo = new LogInfo(tsStatusText);
            _fileName = new DateTimeFromFileName();
        }

        List<FileData> _fileList;
        #region FORM EVETS
        private void Form1_Shown(object sender, EventArgs e)
        {
            _dateTimeChanger = new DateTimeChange(_logInfo, _fileName);
            LoadSettingsFromConfig();
        }
        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {
            setInitialParamForChanger(txtFolderPath.Text, cmb_filter.Text);
        }
        private void cmb_filter_TextChanged(object sender, EventArgs e)
        {
            setInitialParamForChanger(txtFolderPath.Text, cmb_filter.Text);
        }
        private void txtFolderPath_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog()
            {
                Description = "Choose folder",
                ShowNewFolderButton = false,
                SelectedPath = txtFolderPath.Text,
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            if (folderBrowser.ShowDialog() == DialogResult.OK)
                txtFolderPath.Text = folderBrowser.SelectedPath;
        }
        private void txtFolderPath_Leave(object sender, EventArgs e)
        {
            AppConfig.Add(AppConfig.DirectoryKey, txtFolderPath.Text);
        }
        private void cmb_filter_Leave(object sender, EventArgs e)
        {
            AppConfig.Add(AppConfig.ExtensionKey, cmb_filter.Text);
        }
        private void gridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = DeleteSelectedRow(gridView.SelectedRows[0]);
        }
        private void btnChangeDateTime_Click(object sender, EventArgs e)
        {
            var message = MessageBox.Show($"Do you really want to change last write datetime for {_fileList.Count} files", "",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (message == DialogResult.Yes)
            {
                var changedFiles = _dateTimeChanger.ChangeDateTime(_fileList);
                DeleteRowsChangedFiles(changedFiles);
            }
        }
        #endregion
        public void StatusLogText(string messageLog)
        {
            tsStatusText.Text = messageLog;
        }
        private void FillGridView()
        {
            gridView.Rows.Clear();
            foreach (FileData item in _fileList)
                gridView.Rows.Add(item.FileInfo, item.LastWriteTime, item.NewDateTime);
        }
        private void LoadSettingsFromConfig()
        {
            txtFolderPath.Text = AppConfig.Read(AppConfig.DirectoryKey);
            cmb_filter.Text = AppConfig.Read(AppConfig.ExtensionKey);
            _fileList = _dateTimeChanger.ListFile;
            FillGridView();
        }
        private void setInitialParamForChanger(string path, string filter)
        {
            if (!string.IsNullOrEmpty(path))
                _dateTimeChanger.SetPathFolder(path);

            _dateTimeChanger.Filter = filter;
            _fileList = _dateTimeChanger.ListFile;
            FillGridView();
        }
        /// <summary>
        /// Deleting row from GridView
        /// </summary>
        /// <param name="row"></param>
        /// <returns>False - to confirm deleting. True - to undo deleting</returns>
        private bool DeleteSelectedRow(DataGridViewRow row)
        {
            var fileName = row.Cells[0].Value.ToString();
            var message = MessageBox.Show($"Do you really want to delete {Environment.NewLine + fileName}", "",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (message == DialogResult.Yes)
            {
                _fileList.Remove(_fileList.FirstOrDefault(_ => _.FileInfo.Name == fileName));
                _logInfo.Log($"{_fileList.Count} files...");
                return false;
            }
            else
                return true;
        }
        private void DeleteRowsChangedFiles(IEnumerable<string> changedFiles)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (changedFiles.Any(_ => _ == row.Cells[0].Value.ToString()))
                    gridView.Rows.Remove(row);
            }
        }
    }
}
