﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES
{
    public partial class baljoo : Form
    {
        MySqlConnection mySqlConnection = new MySqlConnection
      ("datasource=localhost;port=3307;Database=MES;username=root;password=123qwe");
        MySqlCommand mySqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        public baljoo()
        {
            InitializeComponent();
        }
        private void Displayshow()
        {
            string selectQuery = "select * from order_info_head;";
            mySqlCommand = new MySqlCommand(selectQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);

            oihscreen.DataSource = dataTable;

            oihscreen.Columns[0].HeaderText = "주문번호";
            oihscreen.Columns[1].HeaderText = "구분";
            oihscreen.Columns[2].HeaderText = "주문일자";
            oihscreen.Columns[3].HeaderText = "고객번호";
            oihscreen.Columns[4].HeaderText = "메모";
            oihscreen.Columns[5].HeaderText = "제작자";
            oihscreen.Columns[6].HeaderText = "제작일자";
            oihscreen.Columns[7].HeaderText = "갱신자";
            oihscreen.Columns[8].HeaderText = "갱신일자";
        }

        private void oihscreen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string selectString = oihscreen.CurrentRow.Cells[0].Value.ToString();
            string selectQuery = "select * from order_info_body where ORDER_NO = '" + selectString + "'";

            mySqlCommand = new MySqlCommand(selectQuery, mySqlConnection);
            mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);

            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);

            oibscreen.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baljoo_reg baljoo_Reg = new baljoo_reg();
            baljoo_Reg.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Displayshow();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xcelApp =
                           new Microsoft.Office.Interop.Excel.Application();
            xcelApp.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < oihscreen.Columns.Count + 1; i++)
            {
                xcelApp.Cells[1, i] = oihscreen.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < oihscreen.Rows.Count; i++)
            {
                for (int j = 0; j < oihscreen.Columns.Count; j++)
                {
                    xcelApp.Cells[i + 2, j + 1] = oihscreen.Rows[i].Cells[j].Value.ToString();
                }
            }

            xcelApp.Columns.AutoFit();
            xcelApp.Visible = true;
        }
    }
}
