using System;
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
        string salarytxt;
        public Form1() {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void btnCadastrar_Click(object sender, EventArgs e) {
            if (txtNome.Text.Length != 0 && salarytxt != null) {
                name = txtNome.Text;
                salary = double.Parse(salarytxt, CultureInfo.InvariantCulture);
                people.Add(new Person(name, salary));
                MessageBox.Show("Usuário cadastrado!!", "Cadastro OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                calculaMin(salary, people);
                calculaMax(salary, people);
            }
            else {
                if (txtNome.Text.Length == 0) {
                    MessageBox.Show("Digite um nome!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (salarytxt == null) {
                    MessageBox.Show("Digite um Salário", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            txtNome.Text = "";
            maskedTextBox1.Text = "";
        }

        private void importarToolStripMenuItem_Click(object sender, EventArgs e) {
            importPeople();
            Person p = people.First();
            calculaMax(p.Salary, people);
            calculaMin(p.Salary, people);

        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e) {
            exportPeople();
        }
        private void importPeople() {
            people.Clear();
            openFileDialog1.Filter = "Text files|*.txt|csv files|*.csv";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                var cadastro = openFileDialog1.OpenFile();
                using (StreamReader sr = new StreamReader(cadastro)) {
                    string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                    
                    for (int i = 1; i < lines.Length; i++) {
                        string[] fields = lines[i].Split(',');
                        name = fields[0];
                        salary = double.Parse(fields[1].Substring(2), CultureInfo.InvariantCulture);
                        people.Add(new Person(name, salary));
                    }
                }
            }
        }
        private void exportPeople() {
            saveFileDialog1.Filter = "Text files|*.txt|csv files|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                File.WriteAllText(saveFileDialog1.FileName, String.Empty);
                using (StreamWriter sw = File.AppendText(saveFileDialog1.FileName)) {
                    sw.WriteLine("Nome,Salario");
                    foreach (Person p in people) {
                        sw.WriteLine(p.Name + ",R$" + p.Salary.ToString("F2", CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void maskedTextBox1_Leave(object sender, EventArgs e) {
            if (maskedTextBox1.MaskCompleted==false) {
                MessageBox.Show("Erro!! Digite um salário válido!!!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
               salarytxt = maskedTextBox1.Text.Substring(2).ToString();

            }
        }

        private void estatísticasToolStripMenuItem_Click(object sender, EventArgs e) {
            panel1.Visible = true;
        }
        private void calculaMax(double salary, List<Person> people) {
            double max = salary;
            foreach (Person person in people) {
                if (person.Salary > max) {
                    max = person.Salary;
                }
            }
            lblMaxValue.Text = "R$" + (max.ToString("F2", CultureInfo.InvariantCulture));
        }
        private void calculaMin(double salary, List<Person> people) {
            double min = salary;
            foreach (Person person in people) {
                if (person.Salary < min) {
                    min = person.Salary;
                }
            }
            lblMinValue.Text = "R$" + (min.ToString("F2", CultureInfo.InvariantCulture));
        }

    }
}
