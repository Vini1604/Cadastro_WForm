﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace Cadastro {
    public partial class Form1 : Form {
        string name;
        double salary;
        List<Person> people = new List<Person>();
        string[] lines;
        public Form1() {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void btnCadastrar_Click(object sender, EventArgs e) {
            if (txtNome.Text.Length != 0 && txtSalary.Text.Length != 0) {
                name = txtNome.Text;
                salary = double.Parse(txtSalary.Text, CultureInfo.InvariantCulture);
                people.Add(new Person(name, salary));
            }
            else {
                if (txtNome.Text.Length == 0) {
                    MessageBox.Show("Digite um nome!!");
                }
                if (txtSalary.Text.Length == 0) {
                    MessageBox.Show("Digite um salário");
                }
            }
        }

        private void importarToolStripMenuItem_Click(object sender, EventArgs e) {
            people.Clear();
            openFileDialog1.Filter = "Text files|*.txt";
            openFileDialog1.ShowDialog();
            var cadastro = openFileDialog1.OpenFile();
            using (StreamReader sr = new StreamReader(cadastro)) {
                string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                foreach (string line in lines) {
                    string[] fields = line.Split(',');
                    name = fields[0];
                    salary = double.Parse(fields[1], CultureInfo.InvariantCulture);
                    people.Add(new Person(name, salary));
                }
            }
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e) {
            saveFileDialog1.Filter = "Text files|*.txt";
            saveFileDialog1.ShowDialog();
            using (StreamWriter sw = File.AppendText(saveFileDialog1.FileName)) {
                foreach (Person p in people) {
                    sw.WriteLine(p.Name + "," + p.Salary.ToString("F2", CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
