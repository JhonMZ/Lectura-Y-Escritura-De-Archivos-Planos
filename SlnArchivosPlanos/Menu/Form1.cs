using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Biblioteca de clases para utilizar StramReader y StreamWriter
using System.IO;

namespace Menu
{
    public partial class Form1 : Form
    {
        string dirarchivo = null;//variable para obtener la direccion del archivo

        public Form1()
        {
            InitializeComponent();
        }

        private void btncrear_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text File | *.txt"; //filtro de archivos txt 
            saveFileDialog1.DefaultExt = "txt"; //extencion por defecto txt

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nombrearchivo = saveFileDialog1.FileName; //obtiene nombre del archivo
                StreamWriter sw = File.CreateText(nombrearchivo); //crea el archivo con el nombre que se le ponga

                sw.Close();
            }
        }

        private void btncargar_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear(); //Limpia todos los items del listview

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dirarchivo = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(dirarchivo);

                while(!sr.EndOfStream)//Ciclo hasta terminar las lineas o filas del archivo
                {
                    string strlinea = sr.ReadLine();//lee y asigna el texto de la linea en la variable 

                    if (strlinea.Length > 0)//condicion para saber si la linea o fila contiene texto
                    {
                        int intpos = strlinea.IndexOf(",");//obtiene la posicion de la coma 
                        string nombre = strlinea.Substring(0, intpos);//obtiene una cadena desde la posicion 0 hasta la coma

                        strlinea = strlinea.Substring(intpos + 1);//corta la cadena despues de la coma

                        intpos = strlinea.IndexOf(",");
                        int edad = Convert.ToInt32(strlinea.Substring(0, intpos));

                        strlinea = strlinea.Substring(intpos + 1);

                        intpos = strlinea.IndexOf(",");
                        int ficha = Convert.ToInt32(strlinea);

                        ListViewItem item = new ListViewItem();//objeto de items para agregar en el listview

                        item.Text = nombre;//texto del item o columna 1

                        item.SubItems.Add(edad.ToString());//columna 2
                        item.SubItems.Add(ficha.ToString());//columna 3

                        listView1.Items.Add(item);//agregamos el item al listview
                    }
                }
                sr.Close();
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            StreamWriter sw = File.AppendText(dirarchivo);//Se abre el archivo y se posiciona al final

            string[] lineas = textBox1.Lines;//se obtiene todo el contenido del texbox

            for (int i = 0; i < lineas.Length; i++)//ciclo para escribir en el archivo
            {
                sw.WriteLine(lineas[i]);//escribe el contenido del vector linea por linea o fila por fila en el archivo
            }

            sw.Close();
        }
    }
}
