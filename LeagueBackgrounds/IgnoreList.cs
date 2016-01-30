using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBackgrounds
{
    public partial class IgnoreList : Form
    {
        private readonly DataTable _dataTable = new DataTable();
        private readonly DataGridView _dataGridView = new DataGridView();

        public IgnoreList()
        {
            InitializeComponent();

            _dataTable.Columns.Add("Ignored Splash Arts", typeof(string));
            _dataGridView.AllowUserToAddRows = true;
            _dataGridView.AllowUserToDeleteRows = true;
            _dataGridView.AllowUserToOrderColumns = false;
            _dataGridView.AllowUserToResizeColumns = false;
            _dataGridView.AllowUserToResizeRows = false;
            _dataGridView.AutoSize = true;
            _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Dock = DockStyle.Fill;
            _dataGridView.Margin = new Padding(0);
            _dataGridView.Name = "_DataGridView";
            _dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _dataGridView.ScrollBars = ScrollBars.Both;
            _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView.DataSource = _dataTable;

            TableLayoutPanel.Controls.Add(_dataGridView, 0, 0);
            TableLayoutPanel.SetColumnSpan(_dataGridView, 2);

            foreach (var c in Static.GetIgnoreList())
                _dataTable.Rows.Add(c);
            _dataGridView.Update();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            var tmp = new List<string>();
            for (var i = 0; i < _dataTable.Rows.Count; i++)
            {
                tmp.Add(_dataTable.Rows[i][0].ToString());
            }
            var ignore = tmp.Where(r => !string.IsNullOrEmpty(r)).Aggregate(string.Empty, (current, r) => current + (r + "\r\n"));
            ignore = ignore.Trim();
            if (ignore.Length == 0) ignore = null;
            Properties.Settings.Default.IgnoreList = ignore;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
