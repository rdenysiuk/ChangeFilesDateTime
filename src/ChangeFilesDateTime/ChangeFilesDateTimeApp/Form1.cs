using ChangeFilesDateTimeApp.Config;
using ChangeFilesDateTimeApp.Logger;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChangeFilesDateTimeApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gridView.AutoGenerateColumns = false;
        }

        DateTimeChange _dateTimeChanger;
        LogInfo _logInfo;
        List<FileData> _fileList;

        public void StatusLogText(string messageLog)
        {
            tsStatusText.Text = messageLog;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            _logInfo = new LogInfo(tsStatusText);
            _dateTimeChanger = new DateTimeChange(_logInfo);

            LoadSettingsFromConfig();
        }

        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {
            setInitialParamForChanger(txtFolderPath.Text, cmb_filter.Text);
        }

        private void FillGridView()
        {
            gridView.DataSource = _fileList;
        }

        private void cmb_filter_TextChanged(object sender, EventArgs e)
        {
            setInitialParamForChanger(txtFolderPath.Text, cmb_filter.Text);
        }

        private void setInitialParamForChanger(string path, string filter)
        {
            if (!string.IsNullOrEmpty(path))
                _dateTimeChanger.SetPathFolder(path);

            _dateTimeChanger.Filter = filter;
            _fileList = _dateTimeChanger.ListFile;
            FillGridView();
        }

        private void txtFolderPath_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
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

        private void LoadSettingsFromConfig()
        {
            txtFolderPath.Text = AppConfig.Read(AppConfig.DirectoryKey);
            cmb_filter.Text = AppConfig.Read(AppConfig.ExtensionKey);
            _fileList = _dateTimeChanger.ListFile;
            FillGridView();
        }

    }
}
